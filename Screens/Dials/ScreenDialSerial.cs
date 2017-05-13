using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Moggle.Screens.Dials
{
	/// <summary>
	/// Un objeto que puede presentar una respuesta de tipo genérico como un evento 
	/// </summary>
	/// <seealso cref="IEventoRespuesta{T}"/>
	public interface IEventoRespuesta<T>
	{
		/// <summary>
		/// Ocurre cuando hay una respuesta, regresa la respuesta como argumento
		/// </summary>
		event EventHandler<T> HayRespuesta;
	}

	/// <summary>
	/// Un objeto que puede presentar una respuesta como un evento 
	/// </summary>
	/// <seealso cref="IEventoRespuesta{T}"/>
	public interface IEventoRespuesta
	{
		/// <summary>
		/// Ocurre cuando hay una respuesta, regresa la respuesta como argumento
		/// </summary>
		event EventHandler HayRespuesta;
	}

	[Serializable]
	public sealed class IntEventArgs : EventArgs
	{
		public readonly int Response;
		public IntEventArgs (int resp)
		{
			Response = resp;
		}

		public static implicit operator IntEventArgs (int p)
		{
			return new IntEventArgs (p);
		}
	}
	/// <summary>
	/// Un Screen que puede presentar una respuesta
	/// </summary>
	public interface IRespScreen : IScreen, IEventoRespuesta<IntEventArgs>
	{
	}

	/// <summary>
	/// Provee métodos para invocar una serie de diálogos y presentar una respuesta al final
	/// </summary>
	public class ScreenDialSerial
	{
		/// <summary>
		/// La lista de <see cref="IScreen"/> de suscripción
		/// </summary>
		protected List<IRespScreen> InvocationList;

		/// <summary>
		/// Agrega un <see cref="IScreen"/> a la lista de diálogos
		/// </summary>
		public void AddRequest (IRespScreen inputScreen)
		{
			InvocationList.Add (inputScreen);
		}

		int _currentScr;

		/// <summary>
		/// Ejecuta la serie de invocaciones
		/// </summary>
		/// <param name="useThread">El hilo de screens a usar</param>
		public void Executar (ScreenThread useThread)
		{
			var collectedData = new object [InvocationList.Count];

			// suscrpciones
			for (int i = InvocationList.Count - 1; i >= 0; i--)
			{
				// Preparar screen i
				var scr = InvocationList [i];
				scr.Initialize ();
				scr.LoadAllContent ();

				useThread.Stack (
					scr,
					new ScreenThread.ScreenStackOptions
					{
						DibujaBase = false,
						ActualizaBase = false
					});

				scr.HayRespuesta +=
						delegate (object sender, IntEventArgs e)
				{
					collectedData [_currentScr--] = e.Response;
					useThread.TerminateLast ();
				};

				if (i == InvocationList.Count - 1)
				{
					scr.HayRespuesta += delegate
					{
						Debug.WriteLine (i);
						HayRespuesta?.Invoke (this, collectedData);
					};
				}
			}
			_currentScr = InvocationList.Count - 1;
		}

		/// <summary>
		/// Ocurre cuando se genera una respuesta (como sucesión) a todas los diálogos.
		/// El argumento del evento es la sucesión de las respuestas parciales de cada invocación
		/// </summary>
		public event EventHandler<object []> HayRespuesta;

		/// <summary>
		/// Initializes a new instance of the <see cref="Moggle.Screens.Dials.ScreenDialSerial"/> class.
		/// </summary>
		public ScreenDialSerial ()
		{
			InvocationList = new List<IRespScreen> ();
		}
	}
}
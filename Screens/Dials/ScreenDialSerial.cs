using System;
using System.Collections.Generic;

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

	/// <summary>
	/// Un Screen que puede presentar una respuesta
	/// </summary>
	public interface IRespScreen : IScreen, IEventoRespuesta<object>
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

		/// <summary>
		/// Ejecuta la serie de invocaciones
		/// </summary>
		/// <param name="useThread">El hilo de screens a usar</param>
		public void Executar (ScreenThread useThread)
		{
			var collectedData = new object[InvocationList.Count];

			// suscrpciones
			for (int i = InvocationList.Count - 1; i >= 0; i--)
			{
				// Preparar screen i
				var scr = InvocationList [i];
				scr.Initialize ();
				scr.AddContent ();
				scr.Content.Load ();
				scr.InitializeContent ();

				useThread.Stack (scr);

				scr.HayRespuesta += delegate(object sender, object e)
				{
					collectedData [i] = e;
					useThread.TerminateLast ();
				};

				if (i == 0)
				{
					scr.HayRespuesta += delegate
					{
						HayRespuesta?.Invoke (this, collectedData);
					};
				}
			}
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
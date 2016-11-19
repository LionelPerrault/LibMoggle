using System;
using System.Collections.Generic;

namespace Moggle.Screens.Dials
{
	public interface IEventoRespuesta<T>
	{
		/// <summary>
		/// Ocurre cuando hay una respuesta, regresa la respuesta como argumento
		/// </summary>
		event EventHandler<T> HayRespuesta;
	}

	public interface IEventoRespuesta
	{
		event EventHandler HayRespuesta;
	}

	public interface IRespScreen : IScreen, IEventoRespuesta<object>
	{
	}

	public class ScreenDialSerial
	{
		protected List<IRespScreen> InvocationList;

		public void AddRequest (IRespScreen inputScreen)
		{
			InvocationList.Add (inputScreen);
		}

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

		public event EventHandler<object []> HayRespuesta;

		public ScreenDialSerial ()
		{
			InvocationList = new List<IRespScreen> ();
		}
	}
}
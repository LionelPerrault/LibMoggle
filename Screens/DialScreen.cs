using System;
using Microsoft.Xna.Framework;

namespace Moggle.Screens
{
	public abstract class DialScreen : Screen
	{
		protected IScreen ScreenBase { get; }

		protected DialScreen (Game juego)
			: this (juego, juego.CurrentScreen)
		{
		}

		protected DialScreen (Game juego, IScreen baseScreen)
			: base (juego)
		{
			ScreenBase = baseScreen;
		}

		public override void Dibujar (GameTime gameTime)
		{
			if (DibujarBase)
				ScreenBase.Dibujar (gameTime);
			base.Dibujar (gameTime);
		}

		public override void Ejecutar ()
		{
			#if DEBUG
			System.Diagnostics.Debug.WriteLine ("\n\nEntrando a " + this);
			#endif
			Juego.CurrentScreen.Escuchando = false;
			Inicializar ();
			LoadContent ();
			Juego.CurrentScreen = this;
			Escuchando = true;
		}

		public virtual void Salir ()
		{
			#if DEBUG
			System.Diagnostics.Debug.WriteLine ("\n\nEntrando a " + ScreenBase);
			#endif
			Escuchando = false;
			Juego.CurrentScreen = ScreenBase;
			ScreenBase.Escuchando = true;
			AlTerminar?.Invoke ();

			UnloadContent ();
		}

		public override string ToString ()
		{
			return string.Format ("[{0}]\nAnterior: {1}", GetType (), ScreenBase);
		}

		public override Color BgColor
		{
			get
			{
				return ScreenBase.BgColor;
			}
		}

		public abstract bool DibujarBase { get; }

		public event Action AlTerminar;
	}
}


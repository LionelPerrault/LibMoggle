using System;
using Microsoft.Xna.Framework;

namespace Moggle.Screens
{
	/// <summary>
	/// Un Screen que actúal como un diálogo.
	/// </summary>
	public abstract class DialScreen : Screen
	{
		#region Screen

		/// <summary>
		/// Pantalla base.
		/// </summary>
		protected IScreen ScreenBase { get; }

		#endregion

		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="juego">Juego.</param>
		protected DialScreen (Game juego)
			: this (juego, juego.CurrentScreen)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="juego">Juego.</param>
		/// <param name="baseScreen">Base screen.</param>
		protected DialScreen (Game juego, IScreen baseScreen)
			: base (juego)
		{
			ScreenBase = baseScreen;
		}

		#endregion

		#region Dibujo

		/// <summary>
		/// Dibuja la pantalla
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Draw (GameTime gameTime)
		{
			if (DibujarBase)
				ScreenBase.Draw (gameTime);
			base.Draw (gameTime);
		}

		#endregion

		#region Comportamiento

		/// <summary>
		/// Ejecuta la pantalla.
		/// </summary>
		public override void Ejecutar ()
		{
			Initialize ();
			LoadContent ();
			Juego.CurrentScreen = this;
		}

		/// <summary>
		/// Se sale de este diálogo.
		/// Libera todo los recursos usados.
		/// </summary>
		public virtual void Salir ()
		{
			Juego.CurrentScreen = ScreenBase;
			AlTerminar?.Invoke (this, EventArgs.Empty);

			UnloadContent ();
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Moggle.Screens.DialScreen"/>.
		/// </summary>
		public override string ToString ()
		{
			return string.Format ("[{0}]\nAnterior: {1}", GetType (), ScreenBase);
		}

		/// <summary>
		/// Devuelve el color de fondo
		/// </summary>
		public override Color BgColor
		{
			get
			{
				return ScreenBase.BgColor;
			}
		}

		/// <summary>
		/// Determina si se debe dibujar la pantala madre.
		/// </summary>
		public abstract bool DibujarBase { get; }

		#endregion

		#region Eventos

		/// <summary>
		/// Ocurre al terminar
		/// </summary>
		public event EventHandler AlTerminar;

		#endregion
	}
}
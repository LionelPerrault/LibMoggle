using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using Moggle.Shape;
using Inputs = MonoGame.Extended.InputListeners;
using MonoGame.Extended.InputListeners;

namespace Moggle.Controles
{
	/// <summary>
	/// Single buffered control
	/// </summary>
	public abstract class SBC : DrawableGameComponent, IComponent
	{
		/// <summary>
		/// Pantalla del control
		/// </summary>
		/// <value>The screen.</value>
		public IScreen Screen { get; }

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		protected SBC (IScreen screen)
			: base (screen.Juego)
		{
			Screen = screen;
		}

		/// <summary>
		/// </summary>
		/// <param name="game">Game</param>
		protected SBC (Game game)
			: base (game)
		{
			Screen = null;
		}

		void IComponent.LoadContent ()
		{
			LoadContent ();
		}

		void IComponent.UnloadContent ()
		{
			UnloadContent ();
		}

		/// <summary>
		/// Prioridad de dibujo;
		/// Mayor prioridad se dibuja en la cima
		/// </summary>
		[Obsolete]
		public int Prioridad { get; set; }

		/// <summary>
		/// Gets the game.
		/// </summary>
		public new Game Game
		{
			get
			{
				return (Game)(base.Game);
			}
		}

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores
		/// </summary>
		public override void Initialize ()
		{
			base.Initialize ();
			LoadContent ();
			Game.MouseListener.MouseClicked += check_click;
			Game.MouseListener.MouseDoubleClicked += check_2click;
		}

		void check_2click (object sender, MouseEventArgs e)
		{
			if (GetBounds ().Contains (e.Position))
				OnDoubleClick (e);
		}

		void check_click (object sender, MouseEventArgs e)
		{
			if (GetBounds ().Contains (e.Position))
				OnClick (e);
		}

		/// <summary>
		/// This control was clicked.
		/// </summary>
		protected virtual void OnClick (MouseEventArgs args)
		{
		}

		/// <summary>
		/// This control was double clicked.
		/// </summary>
		protected virtual void OnDoubleClick (MouseEventArgs args)
		{
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		public abstract IShape GetBounds ();

		/// <summary>
		/// Determina si el apuntador del ratón está sobre este control.
		/// </summary>
		[Obsolete]
		public bool MouseOver
		{
			get
			{
				var state = Microsoft.Xna.Framework.Input.Mouse.GetState ();
				return GetBounds ().Contains (state.Position);
			}
		}

		/// <summary>
		/// Devuelve el tiempo en el que el apuntador ha estado sobre este control.
		/// </summary>
		public TimeSpan TiempoMouseOver { get; private set; }

		/// <summary>
		/// Shuts down the component.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose (bool disposing)
		{
			Game.MouseListener.MouseClicked -= check_click;
			Game.MouseListener.MouseDoubleClicked -= check_2click;
			base.Dispose (disposing);
		}
	}
}
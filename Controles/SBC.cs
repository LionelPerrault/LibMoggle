using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using OpenTK.Input;
using Moggle.IO;

namespace Moggle.Controles
{
	/// <summary>
	/// Single buffered control
	/// </summary>
	public abstract class SBC : IControl
	{
		public IScreen Screen { get; }

		protected SBC(IScreen screen)
		{
			Screen = screen;
		}

		public int Prioridad { get; set; }

		public virtual void Include ()
		{
			Screen.Controles.Add(this);
		}

		public virtual void Exclude ()
		{
			Screen.Controles.Remove(this);
		}

		public virtual void Inicializar ()
		{
		}

		/// <summary>
		/// Dibuja el control
		/// </summary>
		public abstract void Dibujar (GameTime gameTime);

		/// <summary>
		/// Loads the content.
		/// </summary>
		public abstract void LoadContent ();

		public virtual void Update (GameTime gameTime)
		{
			CheckMouseState(gameTime.ElapsedGameTime);
		}

		public abstract Rectangle GetBounds ();

		/// <summary>
		/// Se ejecuta cada llamada a game.Update 
		/// </summary>
		public virtual void CheckMouseState (TimeSpan time)
		{
			if (MouseOver)
			{
				if (InputManager.FuePresionado(MouseButton.Left))
					AlClick?.Invoke();

				if (InputManager.FuePresionado(MouseButton.Right))
					AlClickDerecho?.Invoke();

				TiempoMouseOver += time;
			}
			else
			{
				TiempoMouseOver = TimeSpan.Zero;
			}
		}

		public bool MouseOver
		{
			get
			{
				var state = Microsoft.Xna.Framework.Input.Mouse.GetState();
				return GetBounds().Contains(state.Position);
			}
		}

		public TimeSpan TiempoMouseOver { get; private set; }

		void IDisposable.Dispose ()
		{
			Dispose();
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.SBC"/> object.
		/// En particular libera las texturas cargadas
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Moggle.Controles.SBC"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Moggle.Controles.SBC"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="Moggle.Controles.SBC"/> so the garbage
		/// collector can reclaim the memory that the <see cref="Moggle.Controles.SBC"/> was occupying.</remarks>
		protected virtual void Dispose ()
		{
			Exclude();
		}

		public virtual void CatchKey (Key key)
		{
		}

		public event Action AlClick;
		public event Action AlClickDerecho;
	}
}
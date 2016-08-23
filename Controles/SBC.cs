using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using OpenTK.Input;
using Moggle.Controles;
using Moggle.Shape;
using MonoGame.Extended.InputListeners;

namespace Moggle.Controles
{
	/// <summary>
	/// Single buffered control
	/// </summary>
	public abstract class SBC : IControl
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
		{
			Screen = screen;
		}

		/// <summary>
		/// Prioridad de dibujo;
		/// Mayor prioridad se dibuja en la cima
		/// </summary>
		public int Prioridad { get; set; }

		/// <summary>
		/// Incluir este control en su pantalla
		/// </summary>
		public virtual void Include ()
		{
			Screen.Controles.Add (this);
		}

		/// <summary>
		/// Excluir este control de su pantalla
		/// </summary>
		public virtual void Exclude ()
		{
			Screen.Controles.Remove (this);
		}

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores
		/// </summary>
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

		/// <summary>
		/// Ciclo de la lógica
		/// </summary>
		public virtual void Update (GameTime gameTime)
		{
			CheckMouseState (gameTime.ElapsedGameTime);
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		public abstract IShape GetBounds ();

		/// <summary>
		/// Se ejecuta cada llamada a game.Update 
		/// </summary>
		public virtual void CheckMouseState (TimeSpan time)
		{
			if (MouseOver)
			{
				//if (InputManager.FuePresionado (MouseButton.Left))
				AlClick?.Invoke (this, EventArgs.Empty);

				//if (InputManager.FuePresionado (MouseButton.Right))
				AlClickDerecho?.Invoke (this, EventArgs.Empty);

				TiempoMouseOver += time;
			}
			else
			{
				TiempoMouseOver = TimeSpan.Zero;
			}
		}

		/// <summary>
		/// Determina si el apuntador del ratón está sobre este control.
		/// </summary>
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

		void IDisposable.Dispose ()
		{
			Dispose ();
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
			Exclude ();
		}

		/// <summary>
		/// </summary>
		public virtual void CatchKey (KeyboardEventArgs key)
		{
		}

		/// <summary>
		/// Ocurre cuando se hace click en este control.
		/// </summary>
		public event EventHandler AlClick;
		/// <summary>
		/// Ucurre cuando se hace click derecho en este control
		/// </summary>
		public event EventHandler AlClickDerecho;
	}
}
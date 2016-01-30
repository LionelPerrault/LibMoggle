using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Controles;
using Moggle.IO;
using System.Collections.Generic;

namespace Moggle
{
	public class Game : Microsoft.Xna.Framework.Game
	,IScreen // Para poder tener controles globales (cursor)
	{
		protected readonly Ratón Mouse;

		#if FPS
		readonly Label fpsLabel;
		#endif

		public bool Escuchando
		{
			set{ }
		}

		public ListaControl ControlesUniversales { get; }

		public IScreen CurrentScreen;

		readonly GraphicsDeviceManager graphics;

		public SpriteBatch Batch { get; private set; }

		public Game ()
		{
			ControlesUniversales = new ListaControl ();
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = true;
			Mouse = new Ratón (this);
			Mouse.Include ();

			TargetElapsedTime = TimeSpan.FromMilliseconds (7);
			IsFixedTimeStep = false;

		}

		/// <summary>
		/// Carga el contenido del juego, incluyendo los controles universales.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			Batch = new SpriteBatch (GraphicsDevice);
			//spriteBatch.DrawString(new SpriteFont)

			CurrentScreen?.LoadContent ();
			foreach (var x in ControlesUniversales)
			{
				x.LoadContent ();
			}
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			CurrentScreen.Update (gameTime);
			(this as IScreen).Update (gameTime);

			InputManager.Update (gameTime.ElapsedGameTime);
		}

		protected override void OnExiting (object sender, EventArgs args)
		{
			base.OnExiting (sender, args);
			((IScreen)this).UnloadContent ();
		}

		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (BackgroundColor);

			Batch.Begin ();

			CurrentScreen.Dibujar (gameTime);

			foreach (var x in ControlesUniversales)
			{
				x.Dibujar (gameTime);
			}

			//mouse.Dibujar (gameTime);
			Batch.End ();

		}

		public Color BackgroundColor
		{
			get
			{
				return CurrentScreen.BgColor;
			}
		}

		#region IScreen

		Color IScreen.BgColor
		{
			get
			{
				return BackgroundColor;
			}
		}

		public SpriteBatch GetNewBatch ()
		{
			return new SpriteBatch (GraphicsDevice);
		}

		public GraphicsDevice Device
		{
			get
			{
				return GraphicsDevice;
			}
		}

		void IScreen.Dibujar (GameTime gameTime)
		{
			Draw (gameTime);
		}

		ListaControl IScreen.Controles
		{
			get
			{
				return ControlesUniversales;
			}
		}

		/// <summary>
		/// Carga contenido de controles universales
		/// </summary>
		void IScreen.LoadContent ()
		{
			foreach (var cu in ControlesUniversales)
			{
				cu.LoadContent ();
			}
		}

		void IScreen.Update (GameTime gametime)
		{
			foreach (var cu in new List<IControl> (ControlesUniversales))
			{
				cu.Update (gametime);
			}
		}

		void IScreen.UnloadContent ()
		{
			foreach (var cu in new List<IControl> (ControlesUniversales))
			{
				cu.Dispose ();
			}
			CurrentScreen.UnloadContent ();
		}

		public DisplayMode GetDisplayMode
		{
			get
			{
				return GraphicsDevice.Adapter.CurrentDisplayMode;
			}
		}

		void IScreen.Inicializar ()
		{
			Initialize ();
		}

		#endregion
	}
}


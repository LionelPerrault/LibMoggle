using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Controles;
using System.Collections.Generic;
using MonoGame.Extended.InputListeners;

namespace Moggle
{
	/// <summary>
	/// Clase global de un juego.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	,IScreen // Para poder tener controles globales (cursor)
	{
		/// <summary>
		/// Devuelve el control del puntero del ratón.
		/// </summary>
		protected readonly Ratón Mouse;

		#if FPS
		readonly Label fpsLabel;
		#endif

		/// <summary>
		/// Devuelve la lista de controles univesales
		/// </summary>
		/// <value>The controles universales.</value>
		public ListaControl ControlesUniversales { get; }

		/// <summary>
		/// La pantalla mostrada actualmente
		/// </summary>
		public IScreen CurrentScreen;

		/// <summary>
		/// The graphics.
		/// </summary>
		public readonly GraphicsDeviceManager Graphics;

		/// <summary>
		/// Gets the input manager.
		/// </summary>
		public readonly InputListenerManager InputManager = new InputListenerManager ();

		/// <summary>
		/// Gets the keyboard listener
		/// </summary>
		public KeyboardListener KeyListener { get; protected set; }

		/// <summary>
		/// Gets the mouse listener
		/// </summary>
		/// <value>The mouse listener.</value>
		public MouseListener MouseListener { get; protected set; }

		/// <summary>
		/// Batch de dibujo
		/// </summary>
		public SpriteBatch Batch { get; private set; }

		/// <summary>
		/// </summary>
		public Game ()
		{
			ControlesUniversales = new ListaControl ();
			Graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			Mouse = new Ratón (this);

			TargetElapsedTime = TimeSpan.FromMilliseconds (7);
			IsFixedTimeStep = false;

		}

		/// <summary>
		/// </summary>
		protected override void Initialize ()
		{
			base.Initialize ();

			// Listeners
			MouseListener = InputManager.AddListener<MouseListener> ();
			KeyListener = InputManager.AddListener<KeyboardListener> ();

			KeyListener.KeyPressed += keyPressed;
		}

		void keyPressed (object sender, KeyboardEventArgs e)
		{
			TeclaPresionada (e);
		}

		/// <summary>
		/// Manda señal de tecla presionada.
		/// </summary>
		/// <param name="key">Tecla señal</param>
		public void TeclaPresionada (KeyboardEventArgs key)
		{
			CurrentScreen?.TeclaPresionada (key);
		}

		/// <summary>
		/// Carga el contenido del juego, incluyendo los controles universales.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			Batch = new SpriteBatch (GraphicsDevice);
			//spriteBatch.DrawString(new SpriteFont)

			if (Mouse.Habilitado)
				Mouse.Include ();

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
			updateControls (gameTime);

			InputManager.Update (gameTime);
		}

		/// <summary>
		/// Raises the exiting event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		protected override void OnExiting (object sender, EventArgs args)
		{
			base.OnExiting (sender, args);
			((IScreen)this).UnloadContent ();
		}

		/// <summary>
		/// Draw the game
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		protected override void Draw (GameTime gameTime)
		{
			Graphics.GraphicsDevice.Clear (BackgroundColor);

			Batch.Begin ();

			CurrentScreen.Dibujar (gameTime);

			foreach (var x in ControlesUniversales)
			{
				x.Dibujar (gameTime);
			}

			//mouse.Dibujar (gameTime);
			Batch.End ();

		}

		/// <summary>
		/// Gets the color of the background.
		/// </summary>
		public Color BackgroundColor
		{
			get
			{
				return CurrentScreen.BgColor;
			}
		}

		#region IScreen

		Game IScreen.Juego { get { return this; } }

		Color IScreen.BgColor
		{
			get
			{
				return BackgroundColor;
			}
		}

		/// <summary>
		/// Devuelve un nuevo batch de dibujo.
		/// </summary>
		/// <returns>The new batch.</returns>
		public SpriteBatch GetNewBatch ()
		{
			return new SpriteBatch (GraphicsDevice);
		}

		/// <summary>
		/// Devuelve el controlador gráfico.
		/// </summary>
		/// <value>The device.</value>
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
			updateControls (gametime);
		}

		void updateControls (GameTime gametime)
		{
			foreach (var cu in new List<IControl> (ControlesUniversales))
				cu.Update (gametime);
		}

		void IScreen.UnloadContent ()
		{
			foreach (var cu in new List<IControl> (ControlesUniversales))
			{
				cu.Dispose ();
			}
			CurrentScreen.UnloadContent ();
		}

		/// <summary>
		/// Devuelve el modo actual de display gráfico.
		/// </summary>
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
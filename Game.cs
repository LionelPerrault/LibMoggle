using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Comm;
using Moggle.Controles;
using Moggle.Screens;
using MonoGame.Extended.InputListeners;

namespace Moggle
{
	/// <summary>
	/// Clase global de un juego.
	/// </summary>
	public class Game :
	Microsoft.Xna.Framework.Game, 
	IEmisor<KeyboardEventArgs>,				// Para enviar señales de teclado a componentes
	IComponentContainerComponent<IControl>	// Para controlar sus componentes
	{
		/// <summary>
		/// La pantalla mostrada actualmente
		/// </summary>
		public IScreen CurrentScreen
		{ 
			get
			{ 
				try
				{
					return ScreenManager.ActiveThread.Current; 
				}
				catch (InvalidOperationException)
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Devuelve el manejador de pantallas
		/// </summary>
		/// <value>The screen manager.</value>
		public ScreenThreadManager ScreenManager { get; }

		/// <summary>
		/// The graphics.
		/// </summary>
		public readonly GraphicsDeviceManager Graphics;

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
		/// Gets the input listener.
		/// </summary>
		public InputListenerComponent InputListener { get; }

		/// <summary>
		/// Batch de dibujo
		/// </summary>
		public SpriteBatch Batch { get; private set; }

		/// <summary>
		/// </summary>
		public Game ()
		{
			Graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";

			ScreenManager = new ScreenThreadManager ();

			TargetElapsedTime = TimeSpan.FromMilliseconds (7);
			IsFixedTimeStep = false;

			// Crear los listeners
			KeyListener = new KeyboardListener ();
			MouseListener = new MouseListener ();
			InputListener = new InputListenerComponent (
				this,
				KeyListener,
				MouseListener);

			Components.Add (InputListener);
			
		}

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>The container.</value>
		IComponentContainerComponent<IControl> IControl.Container
		{
			get
			{
				// Este valor hace que GetScreen sea más fácil
				return CurrentScreen;
			}
		}

		/// <summary>
		/// Inicializa las componentes globales, así como la pantalla actual (si existe)
		/// También agrega los contenidos al manejador de contenido y carga su contenido.
		/// Finalmente se suscribe a los eventos necesarios
		/// </summary>
		protected override void Initialize ()
		{
			foreach (var x in Components)
				x.Initialize ();
			CurrentScreen?.Initialize ();
			base.Initialize ();

			LoadContent ();
			KeyListener.KeyPressed += keyPressed;
		}

		/// <summary>
		/// Dispose the game
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			KeyListener.KeyPressed -= keyPressed;
			base.Dispose (disposing);
		}

		void keyPressed (object sender, KeyboardEventArgs e)
		{
			MandarSeñal (e);
		}

		/// <summary>
		/// Manda señal de tecla presionada.
		/// </summary>
		/// <param name="key">Tecla señal</param>
		protected virtual void MandarSeñal (KeyboardEventArgs key)
		{
			var sign = new Tuple<KeyboardEventArgs, ScreenThread> (
				           key,
				           ScreenManager.ActiveThread);
			CurrentScreen?.RecibirSeñal (sign);
		}

		void IEmisor<KeyboardEventArgs>.MandarSeñal (KeyboardEventArgs key)
		{
			MandarSeñal (key);
		}


		/// <summary>
		/// Carga el contenido del juego, incluyendo los controles universales.
		/// </summary>
		protected override void LoadContent ()
		{
			foreach (var x in Components.OfType<IComponent> ())
				x.LoadContent (Content);
			CurrentScreen?.LoadContent (Content);
		}

		void IComponent.LoadContent (ContentManager manager)
		{
			foreach (var x in Components.OfType<IComponent> ())
				x.LoadContent (manager);
			CurrentScreen?.LoadContent (manager);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			ScreenManager.UpdateActive (gameTime);
		}

		#region Component

		void IGameComponent.Initialize ()
		{
			Initialize ();
		}

		#endregion

		#region Container

		/// <summary>
		/// Agrega un componente
		/// </summary>
		/// <param name="component">Component.</param>
		public void AddComponent (IControl component)
		{
			Components.Add (component);
		}

		/// <summary>
		/// Elimina un componente
		/// </summary>
		/// <returns><c>true</c> si se eliminó el componente especificado.</returns>
		/// <param name="component">Component.</param>
		public bool RemoveComponent (IControl component)
		{
			return Components.Remove (component);
		}

		System.Collections.Generic.IEnumerable<IControl> IComponentContainerComponent<IControl>.Components
		{
			get { return Components.OfType<IControl> (); }
		}




		#endregion

		/// <summary>
		/// Gets the color of the background.
		/// </summary>
		public Color BackgroundColor
		{
			get
			{
				return ScreenManager.ActiveThread.BgColor ?? Color.Black;
			}
		}

		/// <summary>
		/// Draw the game.
		/// </summary>
		protected override void Draw (GameTime gameTime)
		{
			GraphicsDevice.Clear (BackgroundColor);
			ScreenManager.DrawActive ();
			base.Draw (gameTime);
		}

		#region IScreen

		/// <summary>
		/// Devuelve un nuevo batch de dibujo.
		/// </summary>
		/// <returns>The new batch.</returns>
		public SpriteBatch GetNewBatch ()
		{
			return new SpriteBatch (GraphicsDevice);
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

		#endregion
	}
}
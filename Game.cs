using System;
using System.Linq;
using Microsoft.Xna.Framework;
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
	IEmisorTeclado,  // Para enviar señales de teclado a componentes
	IComponentContainerComponent<IControl>, // Para controlar sus componentes
	IControl
	{
		/// <summary>
		/// La pantalla mostrada actualmente
		/// </summary>
		public IScreen CurrentScreen;

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
		/// Batch de dibujo
		/// </summary>
		public SpriteBatch Batch { get; private set; }

		/// <summary>
		/// Gets the content library
		/// </summary>
		public BibliotecaContenido Contenido { get; }

		/// <summary>
		/// </summary>
		public Game ()
		{
			Graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			Contenido = new BibliotecaContenido (Content);
			//Mouse = new Ratón (this);

			TargetElapsedTime = TimeSpan.FromMilliseconds (7);
			IsFixedTimeStep = false;

			// Crear los listeners
			KeyListener = new KeyboardListener ();
			MouseListener = new MouseListener ();

			Components.Add (new InputListenerComponent (
				this,
				KeyListener,
				MouseListener));
			
		}

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>The container.</value>
		public IComponentContainerComponent<IControl> Container
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// </summary>
		protected override void Initialize ()
		{
			foreach (var x in Components)
				x.Initialize ();

			CurrentScreen?.Initialize ();

			foreach (var x in Components.OfType<IComponent> ())
				x.AddContent (Contenido);
			CurrentScreen?.AddContent (Contenido);

			base.Initialize ();

			LoadContent ();
			KeyListener.KeyPressed += keyPressed;
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
			CurrentScreen?.RecibirSeñal (key);
		}

		void IEmisorTeclado.MandarSeñal (KeyboardEventArgs key)
		{
			MandarSeñal (key);
		}


		/// <summary>
		/// Carga el contenido del juego, incluyendo los controles universales.
		/// </summary>
		protected override void LoadContent ()
		{
			Contenido.Load ();
			OnContentLoaded ();
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
		}

		/// <summary>
		/// This method is invoked when all the content is loaded
		/// </summary>
		protected virtual void OnContentLoaded ()
		{
			foreach (var c in Components.OfType<IComponent> ())
				c.InitializeContent (Contenido);

			CurrentScreen?.InitializeContent (Contenido);
		}

		#region Component

		/// <summary>
		/// Tell the components and the current screen to get the content from the library
		/// </summary>
		/// <param name="manager">Content library</param>
		protected virtual void InitializeContent (BibliotecaContenido manager)
		{
			foreach (var x in Components.OfType<IComponent> ())
				x.InitializeContent (manager);
			CurrentScreen?.InitializeContent (manager);
		}

		void IComponent.InitializeContent (BibliotecaContenido manager)
		{
			InitializeContent (manager);
		}

		void IComponent.AddContent (BibliotecaContenido manager)
		{
		}

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
				return CurrentScreen.BgColor;
			}
		}

		/// <summary>
		/// Draw the game.
		/// </summary>
		protected override void Draw (GameTime gameTime)
		{
			Device.Clear (BackgroundColor);
			CurrentScreen?.Draw (gameTime);
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

		/// <summary>
		/// Unloads the content.
		/// </summary>
		protected override void UnloadContent ()
		{
			Contenido.ClearAll ();
			base.UnloadContent ();
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
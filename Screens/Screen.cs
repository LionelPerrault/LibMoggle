using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Comm;
using Moggle.Controles;
using MonoGame.Extended.InputListeners;

namespace Moggle.Screens
{
	/// <summary>
	/// Implementación común de un <see cref="IScreen"/>
	/// </summary>
	public abstract class Screen : IScreen
	{
		#region Como componente

		/// <summary>
		/// Devuelve el juego.
		/// </summary>
		public Game Juego { get; }

		#endregion

		#region Control

		IComponentContainerComponent<IControl> IControl.Container
		{
			get
			{
				return this;
			}
		}

		#endregion

		#region Listeners

		KeyboardListener KeyListener{ get { return Juego.KeyListener; } }

		MouseListener MouseListener{ get { return Juego.MouseListener; } }

		/// <summary>
		/// El observador del ratón
		/// </summary>
		public readonly MouseObserver MouseObserver = new MouseObserver ();

		#endregion

		#region Memoria

		/// <summary>
		/// Devuelve un valor indicando si esta pantalla ha sido inicializada
		/// </summary>
		public bool IsInitialized { get; private set; }

		public bool Disposed { get; private set; }

		/// <summary>
		/// Cargar contenido de cada control incluido.
		/// </summary>
		public virtual void LoadAllContent ()
		{
			foreach (var x in Components.OfType<IComponent> ())
				x.LoadContent (Juego.Content);
		}

		void IComponent.LoadContent (Microsoft.Xna.Framework.Content.ContentManager manager)
		{
			foreach (var x in Components.OfType<IComponent> ())
				x.LoadContent (manager);
		}

		/// <summary>
		/// Gets the game's content manager
		/// </summary>
		public Microsoft.Xna.Framework.Content.ContentManager Content { get { return Juego.Content; } }

		void IDisposable.Dispose ()
		{
			DisposeChildren ();
			DisposeSelf ();
		}

		/// <summary>
		/// Releases all resource used by the childrends of this <see cref="Moggle.Screens.Screen"/>
		/// </summary>
		protected void DisposeChildren ()
		{
			foreach (var comp in Components.OfType<IDisposable> ())
				comp.Dispose ();
		}

		/// <summary>
		/// Dispose this object (not its childrens)
		/// </summary>
		protected virtual void DisposeSelf ()
		{
			Disposed = true;
			IsInitialized = false;
		}

		/// <summary>
		/// Libera a cada control y deja de escuchar.
		/// </summary>
		public virtual void UnloadContent ()
		{
			foreach (var x in Components.OfType<IDisposable> ())
				x.Dispose ();
		}

		#endregion

		#region Señales

		/// <summary>
		/// Manda la señal de tecla presionada a cada uno de sus controles.
		/// </summary>
		/// <param name="key">Key.</param>
		public virtual void MandarSeñal (KeyboardEventArgs key)
		{
			foreach (var x in Components.OfType<IReceptor<KeyboardEventArgs>> ())
				x.RecibirSeñal (key);
		}

		/// <summary>
		/// Rebice señal del teclado
		/// </summary>
		/// <returns>Devuelve <c>true</c> si la señal fue aceptada.</returns>
		/// <param name="data">Señal tecla</param>
		public virtual bool RecibirSeñal (Tuple<KeyboardEventArgs, ScreenThread> data)
		{
			MandarSeñal (data.Item1);
			return true;
		}

		#endregion

		#region Comportamiento

		/// <summary>
		/// Inicializa la pantalla y la establece como activa en un thread dado
		/// </summary>
		/// <param name="thread">Thread donde ejecutar</param>
		/// <param name="opt">Opciones</param>
		public void Execute (ScreenThread thread,
		                     ScreenThread.ScreenStackOptions opt)
		{
			if (thread == null)
				throw new ArgumentNullException ("thread");
			Prepare ();
			thread.Stack (this, opt);
		}

		/// <summary>
		/// Inicializa la pantalla y la establece como activa en un thread dado
		/// </summary>
		public void Execute ()
		{
			Execute (Juego.ScreenManager.ActiveThread);
		}

		/// <summary>
		/// Inicializa la pantalla y la establece como activa en un thread dado
		/// </summary>
		/// <param name="opt">Opciones</param>
		public void Execute (ScreenThread.ScreenStackOptions opt)
		{
			Execute (Juego.ScreenManager.ActiveThread, opt);
		}

		/// <summary>
		/// Inicializa la pantalla y la establece como activa en un thread dado
		/// </summary>
		/// <param name="thread">Thread donde ejecutar</param>
		public void Execute (ScreenThread thread)
		{
			Execute (thread, ScreenThread.ScreenStackOptions.Default);
		}

		/// <summary>
		/// Devuelve el color de fondo.
		/// </summary>
		/// <value>The color of the background.</value>
		public virtual Color? BgColor{ get { return null; } }

		/// <summary>
		/// Inicializa esta panatalla si es necesario
		/// </summary>
		public void Initialize ()
		{
			if (Disposed)
				throw new ObjectDisposedException ("Screen disposed.");
			if (!IsInitialized)
			{
				DoInitialization ();
				IsInitialized = true;
			}
		}

		/// <summary>
		/// Realiza la inicialización
		/// </summary>
		protected virtual void DoInitialization ()
		{
			foreach (var x in Components)
				x.Initialize ();

			Batch = Juego.GetNewBatch ();
		}

		/// <summary>
		/// Inicializa estructura y contenido de los controles de esta pantalla
		/// </summary>
		public virtual void Prepare ()
		{
			Initialize ();
			LoadAllContent ();
		}


		/// <summary>
		/// Ciclo de la lógica de la pantalla.
		/// Actualiza a cada control.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		/// <param name="currentThread">The thread that is doing the update</param>
		public virtual void Update (GameTime gameTime, ScreenThread currentThread)
		{
			foreach (var x in Components.OfType<IUpdateable> ().OrderBy (z => z.UpdateOrder))
				if (x.Enabled)
					x.Update (gameTime);

			if (MouseObserver.Enabled)
				MouseObserver.Update (gameTime);
		}

		#endregion

		#region Dibujo

		/// <summary>
		/// Dibuja esta pantalla
		/// </summary>
		public virtual void Draw ()
		{
			Batch.Begin ();
			EntreBatches ();
			Batch.End ();
		}

		/// <summary>
		/// Se invoca entre Batch.Begin y Batch.End
		/// </summary>
		protected virtual void EntreBatches ()
		{
			drawComponents ();
		}

		void drawComponents ()
		{
			foreach (var x in Components.OfType<IDrawable> ())
				x.Draw (null);
		}

		/// <summary>
		/// Construye un nuevo Batch de dibujo.
		/// </summary>
		public SpriteBatch GetNewBatch ()
		{
			return Juego.GetNewBatch ();
		}

		/// <summary>
		/// Devuelve el batch de dibujo actual.
		/// </summary>
		public SpriteBatch Batch { get; private set; }

		#endregion

		#region Hardware

		/// <summary>
		/// Devuelve el modo actual de display gráfico.
		/// </summary>
		/// <value>The get display mode.</value>
		public DisplayMode GetDisplayMode
		{
			get
			{
				return Juego.GetDisplayMode;
			}
		}

		/// <summary>
		/// Devuelve el controlador gráfico.
		/// </summary>
		/// <value>The device.</value>
		public GraphicsDevice Device
		{
			get
			{
				return Juego.GraphicsDevice;
			}
		}

		#endregion

		#region Component container

		/// <summary>
		/// Devuelve la lista de controles
		/// </summary>
		/// <value>The controles.</value>
		protected List<IControl> Components { get; }

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

		IEnumerable<IControl> IComponentContainerComponent<IControl>.Components
		{ get { return Components; } }

		#endregion

		#region ctor

		/// <summary>
		/// Initializes a new instance of the <see cref="Moggle.Screens.Screen"/> class.
		/// </summary>
		/// <param name="game">Game.</param>
		protected Screen (Game game)
			: this ()
		{
			Juego = game;
		}

		Screen ()
		{
			Components = new List<IControl> ();
		}

		#endregion
	}
}
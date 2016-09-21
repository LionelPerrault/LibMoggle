using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Comm;
using Moggle.Controles;
using MonoGame.Extended.InputListeners;
using System.Collections.Generic;

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

		#region Listeners

		KeyboardListener KeyListener{ get { return Juego.KeyListener; } }

		MouseListener MouseListener{ get { return Juego.MouseListener; } }

		#endregion

		#region Memoria

		/// <summary>
		/// Cargar contenido de cada control incluido.
		/// Y también se le pide al controlador gráfico un nuevo Batch.
		/// </summary>
		public virtual void LoadContent ()
		{
			foreach (var x in Components.OfType<IComponent> ())
				x.LoadContent (Content);
		}

		void IComponent.LoadContent (ContentManager manager)
		{
			foreach (var x in Components.OfType<IComponent> ())
				x.LoadContent (manager);
		}

		void System.IDisposable.Dispose ()
		{
		}

		/// <summary>
		/// Libera a cada control y deja de escuchar.
		/// </summary>
		public virtual void UnloadContent ()
		{
			foreach (var x in Components.OfType<IComponent> ())
				x.UnloadContent ();
		}

		/// <summary>
		/// Devuelve el manejador de contenidos del juego.
		/// </summary>
		public ContentManager Content
		{
			get
			{
				return Juego.Content;
			}
		}

		#endregion

		#region Señales

		/// <summary>
		/// Manda la señal de tecla presionada a cada uno de sus controles.
		/// </summary>
		/// <param name="key">Key.</param>
		public virtual void MandarSeñal (KeyboardEventArgs key)
		{
			foreach (var x in Components.OfType<IReceptorTeclado> ())
				x.RecibirSeñal (key);
		}

		/// <summary>
		/// Rebice señal del teclado
		/// </summary>
		/// <returns>Devuelve <c>true</c> si la señal fue aceptada.</returns>
		/// <param name="key">Señal tecla</param>
		public virtual bool RecibirSeñal (KeyboardEventArgs key)
		{
			MandarSeñal (key);
			return true;
		}

		#endregion

		#region Comportamiento

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Moggle.Screens.Screen"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Moggle.Screens.Screen"/>.</returns>
		public override string ToString ()
		{
			return string.Format ("[{0}]", GetType ());
		}

		/// <summary>
		/// Devuelve el color de fondo.
		/// </summary>
		/// <value>The color of the background.</value>
		public abstract Color BgColor { get; }

		/// <summary>
		/// Inicializa sus controles
		/// </summary>
		public virtual void Initialize ()
		{
			foreach (var x in Components)
				x.Initialize ();
		}

		/// <summary>
		/// Ejecuta esta pantalla.
		/// Deja de escuchar a la pantalla anterior y yo comienzo a escuchar.
		/// </summary>
		public virtual void Ejecutar ()
		{
			Juego.CurrentScreen = this;
		}

		/// <summary>
		/// Ciclo de la lógica de la pantalla.
		/// Actualiza a cada control.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public virtual void Update (GameTime gameTime)
		{
			foreach (var x in Components.OfType<IUpdateable> ().OrderBy (z => z.UpdateOrder))
				if (x.Enabled)
					x.Update (gameTime);
		}

		#endregion

		#region Dibujo

		/// <summary>
		/// Dibuja esta pantalla
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public virtual void Draw (GameTime gameTime)
		{
			Batch = GetNewBatch ();

			Batch.Begin ();
			EntreBatches (gameTime);
			Batch.End ();
		}

		/// <summary>
		/// Se invoca entre Batch.Begin y Batch.End
		/// </summary>
		protected virtual void EntreBatches (GameTime gameTime)
		{
			drawComponents (gameTime);
		}

		void drawComponents (GameTime gameTime)
		{
			foreach (var x in Components.OfType<IDrawable> ())
				x.Draw (gameTime);
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
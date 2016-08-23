using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Moggle.Controles;
using MonoGame.Extended.InputListeners;

namespace Moggle.Screens
{
	/// <summary>
	/// Implementación común de un <see cref="IScreen"/>
	/// </summary>
	public abstract class Screen : IScreen
	{
		/// <summary>
		/// Devuelve el juego.
		/// </summary>
		public Game Juego { get; }

		/// <summary>
		/// Devuelve la lista de controles
		/// </summary>
		/// <value>The controles.</value>
		public ListaControl Controles { get; }

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
			Controles = new ListaControl ();
		}

		KeyboardListener KeyListener{ get { return Juego.KeyListener; } }

		MouseListener MouseListener{ get { return Juego.MouseListener; } }

		/// <summary>
		/// Manda la señal de tecla presionada a cada uno de sus controles.
		/// </summary>
		/// <param name="key">Key.</param>
		public virtual void TeclaPresionada (KeyboardEventArgs key)
		{
			foreach (var x in Controles)
				x.CatchKey (key);
		}

		/// <summary>
		/// Devuelve el color de fondo.
		/// </summary>
		/// <value>The color of the background.</value>
		public abstract Color BgColor { get; }

		/// <summary>
		/// Inicializa sus controles
		/// </summary>
		public virtual void Inicializar ()
		{
			foreach (var x in Controles)
				x.Inicializar ();

			// Listeners
			//KeyListener.KeyPressed += (sender, e) => TeclaPresionada (e);

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
		/// Dibuja esta pantalla
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public virtual void Dibujar (GameTime gameTime)
		{
			//base.Draw (gameTime);

			//var Batch = GetNewBatch ();
			Batch.Begin ();
			EntreBatches (gameTime);
			Batch.End ();
		}

		/// <summary>
		/// Se invoca entre Batch.Begin y Batch.End
		/// </summary>
		protected virtual void EntreBatches (GameTime gameTime)
		{
			foreach (var x in Controles)
				x.Dibujar (gameTime);
		}

		/// <summary>
		/// Cargar contenido de cada control incluido.
		/// Y también se le pide al controlador gráfico un nuevo Batch.
		/// </summary>
		public virtual void LoadContent ()
		{
			Batch = new SpriteBatch (Juego.GraphicsDevice);
			foreach (var x in Controles)
				x.LoadContent ();
		}

		/// <summary>
		/// Construye un nuevo Batch de dibujo.
		/// </summary>
		public SpriteBatch GetNewBatch ()
		{
			return Juego.GetNewBatch ();
		}

		/// <summary>
		/// Ciclo de la lógica de la pantalla.
		/// Actualiza a cada control.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public virtual void Update (GameTime gameTime)
		{
			foreach (var x in new List<IControl> (Controles))
				x.Update (gameTime);
		}

		/// <summary>
		/// Libera a cada control y deja de escuchar.
		/// </summary>
		public virtual void UnloadContent ()
		{
			foreach (var x in new List<IControl> (Controles))
				x.Dispose ();
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

		/// <summary>
		/// Devuelve el batch de dibujo actual.
		/// </summary>
		public SpriteBatch Batch { get; private set; }

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

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Moggle.Screens.Screen"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Moggle.Screens.Screen"/>.</returns>
		public override string ToString ()
		{
			return string.Format ("[{0}]", GetType ());
		}
	}
}
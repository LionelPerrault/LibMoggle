using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;


namespace Moggle.Screens
{
	/// <summary>
	/// Representa una pantalla con controles visibles al jugador.
	/// </summary>
	public interface IScreen
	{
		/// <summary>
		/// Devuelve el campo Juego.
		/// </summary>
		Game Juego { get; }

		/// <summary>
		/// Dibuja la pantalla
		/// </summary>
		void Dibujar (GameTime gameTime);

		/// <summary>
		/// Cargar contenido
		/// </summary>
		void LoadContent ();

		/// <summary>
		/// Ciclo de la lógica
		/// </summary>
		void Update (GameTime gameTime);

		/// <summary>
		/// Descargar contenido
		/// </summary>
		void UnloadContent ();

		/// <summary>
		/// Color de fondo
		/// </summary>
		Color BgColor { get; }

		/// <summary>
		/// La lista de controles de esta Screen
		/// </summary>
		ListaControl Controles { get; }

		/// <summary>
		/// El manejador de contenido
		/// </summary>
		/// <value>The content.</value>
		ContentManager Content { get; }

		#region hardware

		/// <summary>
		/// Batch de dibujo
		/// </summary>
		SpriteBatch Batch { get; }

		/// <summary>
		/// Devuelve un nuevo batchd e dibujo.
		/// </summary>
		SpriteBatch GetNewBatch ();

		/// <summary>
		/// Devuelve el modo actual de display gráfico.
		/// </summary>
		DisplayMode GetDisplayMode { get; }

		/// <summary>
		/// Devuelve el controlador gráfico.
		/// </summary>
		GraphicsDevice Device { get; }

		#endregion

		/// <summary>
		/// Inicializa la pantalla
		/// </summary>
		void Inicializar ();

		/// <summary>
		/// Manda señal de tecla presionada a esta pantalla
		/// </summary>
		/// <param name="key">Tecla de la señal</param>
		void TeclaPresionada (KeyboardEventArgs key);
	}
}
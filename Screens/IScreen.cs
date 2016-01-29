using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Moggle.Screens
{
	public interface IScreen
	{
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

		bool Escuchando { set; }

		#region hardware

		/// <summary>
		/// Batch de dibujo
		/// </summary>
		SpriteBatch Batch { get; }

		SpriteBatch GetNewBatch ();

		DisplayMode GetDisplayMode { get; }

		GraphicsDevice Device { get; }

		#endregion

		void Inicializar ();
	}
}
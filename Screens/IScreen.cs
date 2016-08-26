using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Comm;
using Moggle.Controles;


namespace Moggle.Screens
{
	/// <summary>
	/// Representa una pantalla con controles visibles al jugador.
	/// </summary>
	public interface IScreen : IEmisorTeclado, IReceptorTeclado, IComponentContainerComponent<IGameComponent>
	{
		/// <summary>
		/// Devuelve el campo Juego.
		/// </summary>
		Game Juego { get; }

		/// <summary>
		/// Dibuja la pantalla
		/// </summary>
		void Draw (GameTime gameTime);

		/// <summary>
		/// Ciclo de la lógica
		/// </summary>
		void Update (GameTime gameTime);

		/// <summary>
		/// Color de fondo
		/// </summary>
		Color BgColor { get; }

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
		/// Devuelve el modo actual de display gráfico.
		/// </summary>
		DisplayMode GetDisplayMode { get; }

		/// <summary>
		/// Devuelve el controlador gráfico.
		/// </summary>
		GraphicsDevice Device { get; }

		#endregion
	}
}
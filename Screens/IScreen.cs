using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Comm;
using Moggle.Controles;
using System;
using MonoGame.Extended.InputListeners;


namespace Moggle.Screens
{
	/// <summary>
	/// Representa una pantalla con controles visibles al jugador.
	/// </summary>
	public interface IScreen : 
	IEmisor<KeyboardEventArgs>, 
	IReceptor<KeyboardEventArgs>, 
	IComponentContainerComponent<IControl>, 
	IControl,
	IDisposable
	{
		#region Dibujo

		/// <summary>
		/// Dibuja la pantalla
		/// </summary>
		void Draw (GameTime gameTime);

		#endregion

		#region Comportamiento

		/// <summary>
		/// Ciclo de la lógica
		/// </summary>
		void Update (GameTime gameTime, ScreenThread currentThread);

		/// <summary>
		/// Color de fondo
		/// </summary>
		Color? BgColor { get; }

		#endregion

		#region Memoria

		/// <summary>
		/// El manejador de contenido
		/// </summary>
		/// <value>The content.</value>
		BibliotecaContenido Content { get; }

		#endregion

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

		#region ctor

		/// <summary>
		/// Devuelve el campo Juego.
		/// </summary>
		Game Juego { get; }

		#endregion
	}
}
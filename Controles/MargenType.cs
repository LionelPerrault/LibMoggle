using Microsoft.Xna.Framework;

namespace Moggle.Controles
{
	/// <summary>
	/// Margen.
	/// </summary>
	public struct MargenType
	{
		/// <summary>
		/// Margen superior
		/// </summary>
		public int Top;
		/// <summary>
		/// Margen inferior
		/// </summary>
		public int Bot;
		/// <summary>
		/// Márgen izquierdo.
		/// </summary>
		public int Left;
		/// <summary>
		/// Márgen derecho.
		/// </summary>
		public int Right;

		/// <summary>
		/// Devuelve un nuevo rectángulo que representa otro rectándulo dado, quitándole estos maŕgenes
		/// </summary>
		public Rectangle ExtractMargin (Rectangle rectangle)
		{
			return new Rectangle (
				rectangle.Left + Left,
				rectangle.Top + Top,
				rectangle.Width - Left - Right,
				rectangle.Height - Top - Bot
			);
		}
	}
}
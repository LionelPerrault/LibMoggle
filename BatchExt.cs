using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Moggle
{
	/// <summary>
	/// Extensiones de SpriteBatch
	/// </summary>
	public static class BatchExt
	{
		/// <summary>
		/// Over de Draw
		/// </summary>
		public static void Draw (this SpriteBatch bat,
								 Texture2D texture,
								 Rectangle rectangle,
								 Color color)
		{
			bat.Draw (texture, rectangle, color);
		}
	}
}
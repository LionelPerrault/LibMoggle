using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Shapes;
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
		                         RectangleF rectangle,
		                         Color color)
		{
			bat.Draw (texture, (Rectangle)rectangle, color);
		}

		/// <summary>
		/// Over de Draw
		/// </summary>
		public static void Draw (this SpriteBatch bat,
		                         Texture2D texture,
		                         IShapeF shape,
		                         Color color)
		{
			bat.Draw (texture, shape.GetBoundingRectangle (), color);
		}
	}
}
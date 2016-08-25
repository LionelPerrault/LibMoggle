using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Shapes;
using Microsoft.Xna.Framework;

namespace Moggle
{
	public static class BatchExt
	{
		public static void Draw (this SpriteBatch bat,
		                         Texture2D texture,
		                         RectangleF rectangle,
		                         Color color)
		{
			bat.Draw (texture, (Rectangle)rectangle, color);
		}

		public static void Draw (this SpriteBatch bat,
		                         Texture2D texture,
		                         IShapeF shape,
		                         Color color)
		{
			bat.Draw (texture, shape.GetBoundingRectangle (), color);
		}
	}
}
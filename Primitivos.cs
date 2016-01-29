using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Moggle
{
	public static class Primitivos
	{
		public static void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color color, Texture2D text)
		{
			Vector2 edge = end - start;
			// calculate angle to rotate line
			float angle = (float)Math.Atan2(edge.Y, edge.X);

			sb.Draw(text,
				start,			         			   
				new Rectangle(
					(int)start.X,
					(int)start.Y,
					(int)edge.Length(),
					1),
				color,
				angle,
				Vector2.Zero,
				1.0f,
				SpriteEffects.None,
				0);

		}

		/// <summary>
		/// Dibuja un rectángulo en un SpriteBatch
		/// </summary>
		public static void DrawRectangle(SpriteBatch bat, Rectangle rect, Color color, Texture2D textura)
		{
			var tl = new Vector2(rect.Left, rect.Top);
			var tr = new Vector2(rect.Right, rect.Top);
			var bl = new Vector2(rect.Left, rect.Bottom);
			var br = new Vector2(rect.Right, rect.Bottom);

			DrawLine(bat, tl, tr, color, textura);
			DrawLine(bat, tr, br, color, textura);
			DrawLine(bat, br, bl, color, textura);
			DrawLine(bat, bl, tl, color, textura);
		}
	}
}
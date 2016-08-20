using Microsoft.Xna.Framework;
using System;

namespace Moggle.Shape
{
	public struct Ellipse : IShape
	{
		public float RadiusX;
		public float RadiusY;
		public Vector2 Center;

		public Ellipse (float radiusX, float radiusY, Vector2 center)
		{
			RadiusX = radiusX;
			RadiusY = radiusY;
			Center = center;
		}

		public bool Contains (Point p)
		{
			var vect = Center - p.ToVector2 ();
			// TODO Test
			return Math.Pow (RadiusX * vect.X, 2) + Math.Pow (RadiusY * vect.Y, 2) < 1;
		}

		public IShape MoveBy (Vector2 v)
		{
			return new Ellipse (RadiusX, RadiusY, Center + v);
		}

		public IShape Scale (float factor)
		{
			return new Ellipse (RadiusX * factor, RadiusY * factor, Center);
		}

		public Microsoft.Xna.Framework.Rectangle GetContainingRectangle ()
		{
			var tl = new Point (
				         (int)(Center.X - RadiusX),
				         (int)(Center.Y - RadiusY));
			var size = new Point ((int)(2 * RadiusX), (int)(2 * RadiusY));
			return new Microsoft.Xna.Framework.Rectangle (tl, size);
		}
	}
}
using Microsoft.Xna.Framework;

namespace Moggle.Shape
{
	public struct Circle : IShape
	{
		public readonly float Radius;
		public readonly Vector2 Center;

		public Circle (float radius, Vector2 center)
		{
			Radius = radius;
			Center = center;
		}

		public bool Contains (Point p)
		{
			var vect = Center - p.ToVector2 ();
			return vect.LengthSquared () < Radius * Radius;
		}

		public IShape MoveBy (Vector2 v)
		{
			return new Circle (Radius, Center + v);
		}

		public IShape Scale (float factor)
		{
			return new Circle (Radius * factor, Center);
		}

		public Microsoft.Xna.Framework.Rectangle GetContainingRectangle ()
		{
			var tl = new Point ((int)(Center.X - Radius), (int)(Center.X - Radius));
			var size = new Point (2 * (int)Radius, 2 * (int)Radius);
			return new Microsoft.Xna.Framework.Rectangle (tl, size);
		}

	}
}
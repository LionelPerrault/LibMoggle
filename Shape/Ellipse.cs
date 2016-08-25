using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;

namespace Moggle.Shape
{
	/// <summary>
	/// Representa una forma elíptica
	/// </summary>
	public struct Ellipse : IShapeF
	{
		/// <summary>
		/// Radio horizontal.
		/// </summary>
		public float RadiusX;
		/// <summary>
		/// Radio vertical.
		/// </summary>
		public float RadiusY;
		/// <summary>
		/// Centro.
		/// </summary>
		public Vector2 Center;

		/// <summary>
		/// Initializes a new instance of the <see cref="Moggle.Shape.Ellipse"/> struct.
		/// </summary>
		/// <param name="radiusX">Radius x.</param>
		/// <param name="radiusY">Radius y.</param>
		/// <param name="center">Center.</param>
		public Ellipse (float radiusX, float radiusY, Vector2 center)
		{
			RadiusX = radiusX;
			RadiusY = radiusY;
			Center = center;
		}

		/// <summary>
		/// Gets the bounding rectangle.
		/// </summary>
		/// <returns>The smallest rectangle containing this shape.</returns>
		public RectangleF GetBoundingRectangle ()
		{
			var tl = new Vector2 (Center.X - RadiusX, Center.Y - RadiusY);
			var size = new SizeF (2 * RadiusX, 2 * RadiusY);
			return new RectangleF (tl, size);
		}

		/// <summary>
		/// Revisa si esta forma contiene un punto dado.
		/// </summary>
		public bool Contains (float x, float y)
		{
			return Contains (new Vector2 (x, y));
		}

		/// <summary>
		/// Revisa si esta forma contiene un punto dado.
		/// </summary>
		/// <param name="point">Punto</param>
		public bool Contains (Vector2 point)
		{
			var vect = Center - point;
			return Math.Pow (RadiusX * vect.X, 2) + Math.Pow (RadiusY * vect.Y, 2) < 1;
		}

		/// <summary>
		/// Gets the left.
		/// </summary>
		public float Left { get { return Center.X - RadiusX; } }

		/// <summary>
		/// Gets the top.
		/// </summary>
		public float Top { get { return Center.Y - RadiusY; } }

		/// <summary>
		/// Gets the right.
		/// </summary>
		public float Right { get { return Center.X + RadiusX; } }

		/// <summary>
		/// Gets the bottom.
		/// </summary>
		public float Bottom{ get { return Center.Y + RadiusY; } }
	}
}
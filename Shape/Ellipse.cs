using System;
using Microsoft.Xna.Framework;

namespace Moggle.Shape
{
	/// <summary>
	/// Representa una forma elíptica
	/// </summary>
	[Obsolete]
	public struct Ellipse : IShape
	{
		#region Parámetros

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

		#endregion

		#region Shape

		/// <summary>
		/// Gets the bounding rectangle.
		/// </summary>
		/// <returns>The smallest rectangle containing this shape.</returns>
		public Microsoft.Xna.Framework.Rectangle GetContainingRectangle ()
		{
			var tl = new Vector2 (Center.X - RadiusX, Center.Y - RadiusY).ToPoint ();
			var size = new Point ((int)(2 * RadiusX), (int)(2 * RadiusY));
			return new Microsoft.Xna.Framework.Rectangle (tl, size);
		}


		/// <summary>
		/// Revisa si esta forma contiene un punto dado.
		/// </summary>
		public bool Contains (float x, float y)
		{
			return Contains (new Vector2 (x, y).ToPoint ());
		}

		/// <summary>
		/// Revisa si esta forma contiene un punto dado.
		/// </summary>
		/// <param name="point">Punto</param>
		public bool Contains (Point point)
		{
			var vect = Center - point.ToVector2 ();
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
		public float Bottom { get { return Center.Y + RadiusY; } }

		#endregion

		#region ctor

		/// <summary>
		/// Initializes a new instance of the <see cref="Ellipse"/> struct.
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

		#endregion
	}
}
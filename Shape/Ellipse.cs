using Microsoft.Xna.Framework;
using System;

namespace Moggle.Shape
{
	/// <summary>
	/// Representa una forma elíptica
	/// </summary>
	public struct Ellipse : IShape
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
		/// Revisa si esta forma contiene un punto dado.
		/// </summary>
		/// <param name="p">Punto</param>
		public bool Contains (Point p)
		{
			var vect = Center - p.ToVector2 ();
			// TODO Test
			return Math.Pow (RadiusX * vect.X, 2) + Math.Pow (RadiusY * vect.Y, 2) < 1;
		}

		/// <summary>
		/// Devuelve una forma que es el resultado de una translación
		/// </summary>
		/// <param name="v">V.</param>
		public IShape MoveBy (Vector2 v)
		{
			return new Ellipse (RadiusX, RadiusY, Center + v);
		}

		/// <summary>
		/// Devuelve una forma que es el resultado de una reescalación
		/// </summary>
		/// <param name="factor">Factor.</param>
		public IShape Scale (float factor)
		{
			return new Ellipse (RadiusX * factor, RadiusY * factor, Center);
		}

		/// <summary>
		/// Devuelve el rectángulo más pequeño que lo contiene
		/// </summary>
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
using Microsoft.Xna.Framework;
using System;

namespace Moggle.Shape
{
	/// <summary>
	/// Representa un círculo
	/// </summary>
	[Obsolete]
	public struct Circle : IShape
	{
		/// <summary>
		/// Radio
		/// </summary>
		public readonly float Radius;

		/// <summary>
		/// Centro
		/// </summary>
		public readonly Vector2 Center;

		/// <summary>
		/// Initializes a new instance of the <see cref="Circle"/> struct.
		/// </summary>
		/// <param name="radius">Radius.</param>
		/// <param name="center">Center.</param>
		public Circle (float radius, Vector2 center)
		{
			Radius = radius;
			Center = center;
		}

		/// <summary>
		/// Revisa si esta forma contiene un punto dado.
		/// </summary>
		/// <param name="p">Punto</param>
		public bool Contains (Point p)
		{
			var vect = Center - p.ToVector2 ();
			return vect.LengthSquared () < Radius * Radius;
		}

		/// <summary>
		/// Devuelve una forma que es el resultado de una translación
		/// </summary>
		/// <param name="v">Vector de translado</param>
		public IShape MoveBy (Vector2 v)
		{
			return new Circle (Radius, Center + v);
		}

		/// <summary>
		/// Devuelve una forma que es el resultado de una reescalación
		/// </summary>
		/// <param name="factor">Factor.</param>
		public IShape Scale (float factor)
		{
			return new Circle (Radius * factor, Center);
		}

		/// <summary>
		/// Devuelve el rectángulo más pequeño que lo contiene.
		/// </summary>
		/// <returns>The containing rectangle.</returns>
		public Microsoft.Xna.Framework.Rectangle GetContainingRectangle ()
		{
			var tl = new Point ((int)(Center.X - Radius), (int)(Center.X - Radius));
			var size = new Point (2 * (int)Radius, 2 * (int)Radius);
			return new Microsoft.Xna.Framework.Rectangle (tl, size);
		}
	}
}
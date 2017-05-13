using System;
using Microsoft.Xna.Framework;

namespace Moggle.Shape
{
	/// <summary>
	/// <para>
	/// Representa una forma en 2D.
	/// </para>
	/// <para>
	/// Provee métodos para mover, estalar, pedir rectángulo excrito, y método para saber si contiene un punto dado.
	/// </para>
	/// </summary>
	[Obsolete]
	public interface IShape
	{
		/// <summary>
		/// Revisa si esta forma contiene un punto dado.
		/// </summary>
		bool Contains (Point p);

		/// <summary>
		/// Devuelve el rectángulo más pequeño que lo contiene
		/// </summary>
		Microsoft.Xna.Framework.Rectangle GetContainingRectangle ();
	}

	/// <summary>
	/// Shapes.
	/// </summary>
	[Obsolete]
	public static class Shapes
	{
		/// <summary>
		/// Devuelve una <see cref="IShape"/> vacía.
		/// </summary>
		public static IShape NoShape { get { return Shapeless.NoShape; } }
	}

	[Obsolete]
	class Shapeless : IShape
	{
		public static readonly Shapeless NoShape = new Shapeless ();

		public Microsoft.Xna.Framework.Rectangle GetContainingRectangle ()
		{
			return Microsoft.Xna.Framework.Rectangle.Empty;
		}

		public bool Contains (float x, float y)
		{
			return false;
		}

		public bool Contains (Point point)
		{
			return false;
		}

		public float Left { get { return 0; } }

		public float Top { get { return 0; } }

		public float Right { get { return 0; } }

		public float Bottom { get { return 0; } }
	}
}
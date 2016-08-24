using Microsoft.Xna.Framework;
using Moggle.Shape;

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
	public interface IShape
	{
		/// <summary>
		/// Revisa si esta forma contiene un punto dado.
		/// </summary>
		bool Contains (Point p);

		/// <summary>
		/// Devuelve una forma que es el resultado de una translación
		/// </summary>
		IShape MoveBy (Vector2 v);

		/// <summary>
		/// Devuelve una forma que es el resultado de una reescalación
		/// </summary>
		IShape Scale (float factor);

		/// <summary>
		/// Devuelve el rectángulo más pequeño que lo contiene
		/// </summary>
		Microsoft.Xna.Framework.Rectangle GetContainingRectangle ();

	}

	/// <summary>
	/// Shapes.
	/// </summary>
	public static class Shapes
	{
		/// <summary>
		/// Devuelve una <see cref="IShape"/> vacía.
		/// </summary>
		public static IShape NoShape { get { return Shapeless.NoShape; } }
	}

	class Shapeless : IShape
	{
		public static readonly Shapeless NoShape = new Shapeless ();

		public bool Contains (Point p)
		{
			return false;
		}

		public IShape MoveBy (Vector2 v)
		{
			return NoShape;
		}

		public IShape Scale (float factor)
		{
			return NoShape;
		}

		public Microsoft.Xna.Framework.Rectangle GetContainingRectangle ()
		{
			return Microsoft.Xna.Framework.Rectangle.Empty;
		}
	}
}
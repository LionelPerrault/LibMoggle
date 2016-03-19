using Microsoft.Xna.Framework;
using Moggle.Shape;

namespace Moggle.Shape
{
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

	public static class Shapes
	{
		static IShape NoShape
		{
			get
			{
				return new Shapeless ();
			}
		}
	}

	class Shapeless : IShape
	{
		public bool Contains (Point p)
		{
			return false;
		}

		public IShape MoveBy (Vector2 v)
		{
			return new Shapeless ();
		}

		public IShape Scale (float factor)
		{
			return new Shapeless ();
		}

		public Microsoft.Xna.Framework.Rectangle GetContainingRectangle ()
		{
			return Microsoft.Xna.Framework.Rectangle.Empty;
		}
	}
}
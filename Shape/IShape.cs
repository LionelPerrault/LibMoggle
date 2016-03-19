using Microsoft.Xna.Framework;

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
		Rectangle GetContainingRectangle ();
	}
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un objeto que puede ser dibujado a un rectángulo dado
	/// </summary>
	public interface IDibujable
	{
		/// <summary>
		/// Dibuja el objeto sobre un rectpangulo específico
		/// </summary>
		/// <param name="bat">Batch</param>
		/// <param name="rect">Rectángulo de dibujo</param>
		void Draw (SpriteBatch bat, Rectangle rect);
	}
}
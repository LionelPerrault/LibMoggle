using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Moggle.Controles
{
	/// <summary>
	/// Provee los métodos que definen a un control activable, actualizable y dibujable.
	/// </summary>
	public interface IBotón : IActivable, IControl, IUpdate, IDrawable
	{
	}
}
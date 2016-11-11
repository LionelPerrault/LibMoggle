using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un objeto que ocupa un espacio en la pantalla
	/// </summary>
	public interface ISpaceable
	{
		/// <summary>
		/// Devuelve el espacio que ocupa en la pantalla
		/// </summary>
		IShapeF GetBounds ();
	}
}
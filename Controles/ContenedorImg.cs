using Microsoft.Xna.Framework;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un contenedor de sprites
	/// </summary>
	public class ContenedorImg : Contenedor<FlyingSprite>
	{
		/// <summary>
		/// Agrega un sprite
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="color">Color.</param>
		public void Add (string texture, Color color)
		{
			var newItem = new FlyingSprite
			{
				Color = color,
				TextureName = texture
			};
			Add (newItem);
		}

		/// <summary>
		/// </summary>
		/// <param name="cont">Container</param>
		public ContenedorImg (IComponentContainerComponent<IControl> cont)
			: base (cont)
		{
		}
	}
}
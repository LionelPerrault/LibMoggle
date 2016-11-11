using Microsoft.Xna.Framework;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un contenedor de sprites
	/// </summary>
	public class ContenedorImage : Contenedor<FlyingSprite>
	{
		/// <summary>
		/// Agrega un sprite
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="color">Color.</param>
		public void Add (string texture, Color color)
		{
			var newItem = new FlyingSprite (Screen.Content)
			{
				Color = color,
				TextureName = texture
			};
			Add (newItem);
		}

		/// <summary>
		/// </summary>
		/// <param name="cont">Container</param>
		public ContenedorImage (IComponentContainerComponent<IControl> cont)
			: base (cont)
		{
		}
	}
}
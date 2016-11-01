using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Moggle.Textures
{
	// TEST all

	/// <summary>
	/// Provee métodos para construir texturas sencillas.
	/// </summary>
	public class SimpleTextures
	{
		readonly GraphicsDevice _grDevice;

		/// <summary>
		/// Genera una textura a partir de un tamaño y una función de asignación de color
		/// </summary>
		/// <param name="textureSize">Tamaño de la textura</param>
		/// <param name="mapping">Función de coloración</param>
		protected Texture2D generateFromFunc (Size textureSize,
		                                      Func<int, int, Color> mapping)
		{
			var len = textureSize.Width * textureSize.Height;
			var data = new Color[len];
			for (int i = 0; i < len; i++)
				data [i] = mapping (i / textureSize.Width, i / textureSize.Height);
			var ret = new Texture2D (_grDevice, textureSize.Width, textureSize.Height);
			ret.SetData<Color> (data);
			return ret;
		}

		/// <summary>
		/// Devuelve una textura sólida de un color dado
		/// </summary>
		/// <param name="textureSize">Tamaño de la textura</param>
		/// <param name="color">Color</param>
		public Texture2D SolidTexture (Size textureSize, Color color)
		{
			return generateFromFunc (textureSize, (i, j) => color);
		}

		/// <summary>
		/// Devuelve una textura de colores alternantes
		/// </summary>
		/// <param name="textureSize">Tamaño de la textura</param>
		/// <param name="colors">Una lista de colores el cual usar como alternados</param>
		public Texture2D AlternatingTexture (Size textureSize, IList<Color> colors)
		{
			return generateFromFunc (
				textureSize,
				(i, j) => colors [(i + j) % colors.Count]);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Moggle.Textures.SimpleTextures"/> class.
		/// </summary>
		/// <param name="device">Graphics device</param>
		public SimpleTextures (GraphicsDevice device)
		{
			if (device == null)
				throw new ArgumentNullException ("device");

			_grDevice = device;
		}
	}
}
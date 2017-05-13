using System;
using System.Collections.Generic;
using CE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Moggle.Textures
{
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
			var data = new Color [len];
			for (int i = 0; i < len; i++)
				data [i] = mapping (i % textureSize.Width, i / textureSize.Width);
			var ret = new Texture2D (_grDevice, textureSize.Width, textureSize.Height);
			ret.SetData (data);
			return ret;
		}

		/// <summary>
		/// Devuelve una textura sólida de un color dado
		/// </summary>
		/// <param name="textureSize">Tamaño de la textura</param>
		/// <param name="color">Color</param>
		public Texture2D SolidTexture (Size textureSize, Color color)
		{
			if (textureSize.Height * textureSize.Width == 0)
				throw new InvalidOperationException ("Cannot make a texture with heigth or width equal to zero.");
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
		/// Genera una textura que incluye el contorno de un color y el interior de otro
		/// </summary>
		/// <param name="textureSize">Tamaño de la textura</param>
		/// <param name="outlineColor">Color del contorno</param>
		/// <param name="insideColor">Color interior</param>
		public Texture2D OutlineTexture (Size textureSize,
													Color outlineColor,
													Color? insideColor = null)
		{
			var useInsideColor = insideColor ?? Color.Transparent;
			if (textureSize.Height == 1 || textureSize.Width == 1)
				return SolidTexture (textureSize, outlineColor);
			return generateFromFunc (
				textureSize,
				(x, y) => (x % (textureSize.Width - 1)) * (y % (textureSize.Height - 1)) == 0 ? outlineColor : useInsideColor);
		}



		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleTextures"/> class.
		/// </summary>
		/// <param name="device">Graphics device</param>
		public SimpleTextures (GraphicsDevice device)
		{
			if (device == null)
				throw new ArgumentNullException (nameof (device));

			_grDevice = device;
		}
	}
}
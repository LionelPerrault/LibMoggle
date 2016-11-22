using MonoGame.Extended.BitmapFonts;
using System.Collections.Generic;

namespace Moggle.Text
{
	public static class StringExt
	{
		/// <summary>
		/// Separa las líneas de texto para que quepan en un espacio horizontal dado
		/// </summary>
		/// <returns>Un arreglo de <c>string</c>que contiene los renglones del texto</returns>
		/// <param name="bmFont">Textura del texto</param>
		/// <param name="text">Texto</param>
		/// <param name="maxWidth">Grosor máximo de la salida</param>
		public static string[] SepararLíneas (BitmapFont bmFont,
		                                      string text,
		                                      int maxWidth)
		{
			var ret = new List<string> ();
			int sep = 0;
			while (sep < text.Length)
			{
				var newLine = tomarUnaLínea (bmFont, text, maxWidth, sep, out sep);
				ret.Add (newLine);
			}
			return ret.ToArray ();
		}

		static string tomarUnaLínea (BitmapFont bmFont,
		                             string text,
		                             int maxWidth,
		                             int startIndex,
		                             out int endIndex)
		{
			endIndex = startIndex;
			while (true)
			{
				var i = text.IndexOf (' ', startIndex);
				var nextStr = text.Substring (startIndex, i - startIndex);
				var cSize = bmFont.MeasureString (nextStr);
				if (cSize.Width > maxWidth)
				{
					//endIndex = indexSeeker
					return text.Substring (startIndex, endIndex - startIndex);
				}
				endIndex = i;
			}
		}
	}
}
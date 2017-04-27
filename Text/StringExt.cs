using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended.BitmapFonts;

namespace Moggle.Text
{
	/// <summary>
	/// Extensiones de <c>string</c>
	/// </summary>
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
			if (string.IsNullOrWhiteSpace (text))
				return new string [] { };
			var ret = new List<string> ();
			var splitText = new List<string> (text.Split (' '));
			while (splitText.Count > 0)
			{
				var newLine = tomarUnaLínea (bmFont, splitText, maxWidth);
				if (string.IsNullOrWhiteSpace (newLine))
					throw new Exception ("Cannot make string fit.");
				ret.Add (newLine);
			}
			return ret.ToArray ();
		}

		static string joinString (IList<string> sepString)
		{
			var ret = new StringBuilder ();
			foreach (var x in sepString)
				ret.Append (x.Trim () + " ");
			return ret.ToString ().Trim ();
		}

		static string tomarUnaLínea (BitmapFont bmFont,
		                             IList<string> spacedText,
		                             int maxWidth)
		{
			var ret = "";
			while (spacedText.Count > 0)
			{
				var nextWord = spacedText [0].Trim ();
				var nextStr = string.Format ("{0} {1}", ret, nextWord).Trim ();
				if (bmFont.MeasureString (nextStr).Width > maxWidth)
					return ret;
				ret = nextStr;
				spacedText.RemoveAt (0);
			}
			return ret;
		}
	}
}
using NUnit.Framework;
using System;
using Moggle.Text;
using MonoGame.Extended.BitmapFonts;
using System.Text;

namespace Test
{
	[TestFixture]
	public class SeparaString
	{
		[Ignore]
		public void SeparaLines ()
		{
			var _r = new Random ();
			const string longLine = "Esto es una línea larga de prueba, necesito que separe en partes";
			var maxLen = _r.Next (10, 200); // Longitud máxima
			var juego = new Game1 ();
			var z = juego.Content;
			z.RootDirectory = "Content";
			var font = z.Load<BitmapFont> ("cont//font");
			var lines = StringExt.SepararLíneas (font, longLine, maxLen);
			// Assert dos cosas: 1) Şum(lines)=longLine, 2) max_{l in lines}(len(l)) <= maxLen
			// (1)
			var rebuild = new StringBuilder ();
			foreach (var l in lines)
				rebuild.Append (l + " ");
			Assert.AreEqual (longLine, rebuild.ToString ().Trim ());
			// (2)
			foreach (var l in lines)
				Assert.LessOrEqual (font.GetSize (l).Width, maxLen);
		}
	}
}
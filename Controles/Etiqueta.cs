using System;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework;
using Moggle.Shape;

namespace Moggle.Controles
{
	public class Etiqueta : SBC
	{
		public Etiqueta (IScreen screen)
			: base (screen)
		{
			Color = Color.White;
		}

		public string UseFont { get; set; }

		BitmapFont font;

		public Func<string> Texto;

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			var txt = Texto ();
			bat.DrawString (font, txt, Posición.ToVector2 (), Color);
		}

		public Point Posición { get; set; }

		public Color Color { get; set; }

		public override IShape GetBounds ()
		{
			return (Moggle.Shape.Rectangle)font.GetStringRectangle (
				Texto (),
				Posición.ToVector2 ());
		}

		public override void LoadContent ()
		{
			font = Screen.Content.Load<BitmapFont> (UseFont);
		}

		protected override void Dispose ()
		{
			font = null;
			base.Dispose ();
		}
	}
}
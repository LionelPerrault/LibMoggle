using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Shape;

namespace Moggle.Controles
{
	public class Botón : SBC
	{
		public Botón (IScreen screen, Rectangle bounds)
			: base (screen)
		{
			Bounds = bounds;
			Color = Color.White;
		}

		public Rectangle Bounds { get; set; }

		public override IShape GetBounds ()
		{
			return Bounds;
		}

		public Texture2D TexturaInstancia { get; protected set; }

		public Color Color { get; set; }

		public string Textura { get; set; }

		public override void Dibujar (GameTime gameTime)
		{
			if (Bounds.Left == 0)
				Console.WriteLine ();
			Screen.Batch.Draw (TexturaInstancia, Bounds, Color);
		}

		public override void LoadContent ()
		{
			Textura = Textura ?? "Rect";
			TexturaInstancia = Screen.Content.Load<Texture2D> (Textura);
		}

		protected override void Dispose ()
		{
			TexturaInstancia = null;
			Textura = "";
			base.Dispose ();
		}

		public bool Habilidato { get; set; }

		public override string ToString ()
		{
			return string.Format ("{0}{1}", Habilidato ? "[H]" : "", Textura);
		}
	}
}


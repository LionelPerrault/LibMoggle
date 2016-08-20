using Moggle.Shape;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Moggle.Controles
{
	public class Botón : SBC
	{
		public Botón (Moggle.Screens.IScreen screen)
			: base (screen)
		{
			Color = Color.White;
		}

		public Botón (Moggle.Screens.IScreen screen, IShape shape)
			: this (screen)
		{
			Bounds = shape;
		}

		public Botón (Moggle.Screens.IScreen screen,
		              Microsoft.Xna.Framework.Rectangle bounds)
			: this (screen)
		{
			Bounds = new Moggle.Shape.Rectangle (bounds.Location, bounds.Size);
		}

		public bool Habilidato { get; set; }

		public IShape Bounds { get; set; }

		public Texture2D TexturaInstancia { get; protected set; }

		public Color Color { get; set; }

		public string Textura { get; set; }

		public override void Dibujar (GameTime gameTime)
		{
			Screen.Batch.Draw (
				TexturaInstancia,
				Bounds.GetContainingRectangle (),
				Color);
		}

		public override void LoadContent ()
		{
			try
			{
				TexturaInstancia = Screen.Content.Load<Texture2D> (Textura);
			}
			catch (Exception ex)
			{
				throw new Exception ("Error trying to load texture " + Textura, ex);
			}
		}

		protected override void Dispose ()
		{
			TexturaInstancia = null;
			Textura = null;
			base.Dispose ();
		}

		public override string ToString ()
		{
			return string.Format ("{0}{1}", Habilidato ? "[H]" : "", Textura);
		}

		public override IShape GetBounds ()
		{
			return Bounds;
		}
	}
}
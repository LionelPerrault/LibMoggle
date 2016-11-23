using System;
using Microsoft.Xna.Framework;
using Moggle.Controles;
using Moggle.Screens;
using Moggle.Text;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Shapes;
using Microsoft.Xna.Framework.Graphics;

namespace Moggle.Controles
{
	public class EtiquetaMultiLínea : DSBC
	{
		public string Texto { get; set; }

		public string UseFont { get; set; }

		public BitmapFont Font { get; private set; }

		public Point TopLeft { get; set; }

		Texture2D bgTexture;
		string [] drawingLines;
		int maxWidth;

		protected override void AddContent ()
		{
			var textures = new Textures.SimpleTextures (Screen.Device);
			bgTexture = textures.SolidTexture (new Size (1, 1), Color.White);
			Screen.Content.AddContent (UseFont);
		}

		protected override IShapeF GetBounds ()
		{
			return new RectangleF (TopLeft.ToVector2 (), Size);
		}

		public int MaxWidth
		{
			get
			{
				return maxWidth;
			}
			set
			{
				maxWidth = value;
				RecalcularLíneas ();
			}
		}

		public int Height { get { return VisibleLines * Font.LineHeight; } }

		public Size Size { get { return new Size (MaxWidth, Height); } }

		protected void RecalcularLíneas ()
		{
			if (!IsInitialized) // Si no está inicializado, Font es nulo así que no se debe correr
				return;
			var lins = StringExt.SepararLíneas (Font, Texto, MaxWidth);
			drawingLines = lins;
		}

		public int VisibleLines
		{
			get
			{
				if (!IsInitialized)
					throw new InvalidOperationException ("Item not initialized");
				return drawingLines.Length;
			}
		}

		public Color TextColor { get; set; }

		public Color BackgroundColor { get; set; }

		protected override void Draw ()
		{
			var currTop = TopLeft.Y;
			var bat = Screen.Batch;

			bat.Draw (bgTexture, new Rectangle (TopLeft, Size), BackgroundColor);

			for (int i = 0; i < VisibleLines; i++)
			{
				var currLine = drawingLines [i];
				bat.DrawString (
					Font,
					currLine,
					new Vector2 (TopLeft.X, currTop),
					TextColor);
				currTop += Font.LineHeight;
			}
		}

		protected override void InitializeContent ()
		{
			base.InitializeContent ();
			Font = Screen.Content.GetContent<BitmapFont> (UseFont);
			RecalcularLíneas ();
		}

		public override void Update (GameTime gameTime)
		{
		}

		public EtiquetaMultiLínea (IScreen screen)
			: base (screen)
		{
			TextColor = Color.White;
			BackgroundColor = Color.Transparent;
		}
	}
}
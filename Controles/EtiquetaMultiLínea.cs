using System;
using Microsoft.Xna.Framework;
using Moggle.Controles;
using Moggle.Screens;
using Moggle.Text;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	public class EtiquetaMultiLínea : DSBC
	{
		public string Texto { get; set; }

		public string UseFont { get; set; }

		public BitmapFont Font { get; private set; }

		public Point TopLeft { get; set; }

		string [] drawingLines;
		Size maxSize;

		protected override void AddContent ()
		{
			Screen.Content.AddContent (UseFont);
		}

		protected override IShapeF GetBounds ()
		{
			return new RectangleF (TopLeft.ToVector2 (), MaxSize);
		}

		public Size MaxSize
		{
			get
			{
				return maxSize;
			}
			set
			{
				maxSize = value;
				RecalcularLíneas ();
			}
		}

		protected void RecalcularLíneas ()
		{
			if (!IsInitialized) // Si no está inicializado, Font es nulo así que no se debe correr
				return;
			var lins = StringExt.SepararLíneas (Font, Texto, MaxSize.Width);
			drawingLines = lins;
		}

		public int VisibleLines
		{
			get
			{
				if (!IsInitialized)
					throw new InvalidOperationException ("Item not initialized");
				return Math.Min (MaxSize.Height / Font.LineHeight, drawingLines.Length);
			}
		}

		public Color Color { get; set; }

		protected override void Draw ()
		{
			var currTop = TopLeft.Y;
			var bat = Screen.Batch;

			for (int i = 0; i < VisibleLines; i++)
			{
				var currLine = drawingLines [i];
				bat.DrawString (Font, currLine, new Vector2 (TopLeft.X, currTop), Color);
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
		}
	}
}
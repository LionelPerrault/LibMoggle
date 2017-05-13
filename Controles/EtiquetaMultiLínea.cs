using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Screens;
using Moggle.Text;
using MonoGame.Extended.BitmapFonts;

namespace Moggle.Controles
{
	/// <summary>
	/// Una etiqueta con máximo grosor
	/// </summary>
	public class EtiquetaMultiLínea : DSBC
	{
		string _texto;

		/// <summary>
		/// Devuelve o establece el texto
		/// </summary>
		public string Texto
		{
			get
			{
				return _texto;
			}
			set
			{
				_texto = value;
				RecalcularLíneas ();
			}
		}

		/// <summary>
		/// Devuelve o establece el nombre de la fuente como contenido
		/// </summary>
		public string UseFont { get; set; }

		/// <summary>
		/// Devuelve la fuente de impresión
		/// </summary>
		public BitmapFont Font { get; private set; }

		/// <summary>
		/// Gets the texture generator (for background)
		/// </summary>
		protected Textures.SimpleTextures TextureGenerator { get; }

		/// <summary>
		/// Devuelve o establece la posición de la etiqueta
		/// </summary>
		/// <value>La posición de la esquena superior izquierda</value>
		public Point TopLeft { get; set; }

		Texture2D bgTexture;
		string [] drawingLines;
		int maxWidth;

		/// <summary>
		/// Loads the content using a given manager
		/// </summary>
		/// <param name="manager">Manager.</param>
		protected override void LoadContent (Microsoft.Xna.Framework.Content.ContentManager manager)
		{
			base.LoadContent (manager);
			Font = Screen.Content.Load<BitmapFont> (UseFont);
			RecalcularLíneas ();
		}

		void buildBackgroundTexture ()
		{
			RecalcularLíneas ();
			bgTexture = TextureGenerator.OutlineTexture (
				Size,
				BackgroundColor,
				Color.White);
		}

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores.
		/// No invoca LoadContent por lo que es seguro agregar componentes
		/// </summary>
		protected override void Initialize ()
		{
			base.Initialize ();
			buildBackgroundTexture ();
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		protected override Rectangle GetBounds ()
		{
			return new Rectangle (TopLeft, new Point (Size.Width, Size.Height));
		}

		/// <summary>
		/// Devuelve o establece el máximo grosor de cada línea
		/// </summary>
		public int MaxWidth
		{
			get
			{
				return maxWidth;
			}
			set
			{
				maxWidth = value;
				if (IsInitialized)
					buildBackgroundTexture ();
			}
		}

		/// <summary>
		/// Devuelve la altura actual de la etiqueta
		/// </summary>
		public int Height { get { return LinesCount * Font.LineHeight; } }

		/// <summary>
		/// Devuelve el tamaño de la etiqueta
		/// </summary>
		public CE.Size Size { get { return new CE.Size (MaxWidth, Height); } }

		/// <summary>
		/// Vuelve a calcular las líneas.
		/// Debe invocarse cuando el texto, la fuente o el tamaño cambia
		/// </summary>
		protected void RecalcularLíneas ()
		{
			if (!IsInitialized) // Si no está inicializado, Font es nulo así que no se debe correr
				return;
			var lins = StringExt.SepararLíneas (Font, Texto, MaxWidth);
			drawingLines = lins;
		}

		/// <summary>
		/// Devuelve el número de líneas
		/// </summary>
		public int LinesCount
		{
			get
			{
				if (!IsInitialized)
					throw new InvalidOperationException ("Item not initialized");
				return drawingLines.Length;
			}
		}

		/// <summary>
		/// Devuelve o establece el color del texto
		/// </summary>
		public Color TextColor { get; set; }

		/// <summary>
		/// Devuelve o establece el color de fondo
		/// </summary>
		public Color BackgroundColor { get; set; }

		/// <summary>
		/// Imprime la etiqueta
		/// </summary>
		protected override void Draw ()
		{
			var currTop = TopLeft.Y;
			var bat = Screen.Batch;

			bat.Draw (bgTexture, new Rectangle (TopLeft, new Point (Size.Width, Size.Height)), BackgroundColor);

			for (int i = 0; i < LinesCount; i++)
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

		/// <summary>
		/// Update lógico
		/// </summary>
		public override void Update (GameTime gameTime)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public EtiquetaMultiLínea (IScreen screen)
			: base (screen)
		{
			TextColor = Color.White;
			BackgroundColor = Color.Transparent;
			TextureGenerator = new Textures.SimpleTextures (screen.Device);
		}
	}
}
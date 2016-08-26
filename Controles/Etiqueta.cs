using System;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	/// <summary>
	/// Es un texto que se muestra en pantalla.
	/// </summary>
	public class Etiqueta : DSBC
	{
		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public Etiqueta (IScreen screen)
			: base (screen)
		{
			Color = Color.White;
		}

		/// <summary>
		/// Devuelve o establece la fuente de texto a usar.
		/// </summary>
		/// <value>The use font.</value>
		public string UseFont { get; set; }

		BitmapFont font;

		/// <summary>
		/// Una función que determina el texto a mostrar.
		/// </summary>
		public Func<string> Texto;

		/// <summary>
		/// Dibuja el control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Draw (GameTime gameTime)
		{
			var bat = Screen.Batch;
			var txt = Texto ();
			bat.DrawString (font, txt, Posición.ToVector2 (), Color);
		}

		/// <summary>
		/// Devuelve o establece la posición de la etiqueta.
		/// </summary>
		public Point Posición { get; set; }

		/// <summary>
		/// Devuelve o establece el color del texto.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		public override IShapeF GetBounds ()
		{
			return (RectangleF)font.GetStringRectangle (
				Texto (),
				Posición.ToVector2 ());
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void LoadContent ()
		{
			font = Screen.Content.Load<BitmapFont> (UseFont);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.Etiqueta"/> object.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			font = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Update lógico
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (GameTime gameTime)
		{
		}
	}
}
using System;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Shapes;
using Microsoft.Xna.Framework.Content;

namespace Moggle.Controles
{
	/// <summary>
	/// Es un texto que se muestra en pantalla.
	/// </summary>
	public class Etiqueta : DSBC
	{
		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public Etiqueta (IScreen screen)
			: base (screen)
		{
			Color = Color.White;
		}

		#endregion

		#region Comportamiento

		/// <summary>
		/// Devuelve o establece la fuente de texto a usar.
		/// </summary>
		/// <value>The use font.</value>
		public string UseFont { get; set; }

		/// <summary>
		/// Una función que determina el texto a mostrar.
		/// </summary>
		public Func<string> Texto;

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
		/// Update lógico
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (GameTime gameTime)
		{
		}

		#endregion

		#region Dibujo

		BitmapFont font;

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

		#endregion

		#region Memoria

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void LoadContent (ContentManager manager)
		{
			font = manager.Load<BitmapFont> (UseFont);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.Etiqueta"/> object.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			font = null;
			base.Dispose (disposing);
		}

		#endregion
	}
}
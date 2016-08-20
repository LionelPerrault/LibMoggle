﻿using System;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework;
using Moggle.Shape;

namespace Moggle.Controles
{
	/// <summary>
	/// Es un texto que se muestra en pantalla.
	/// </summary>
	public class Etiqueta : SBC
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
		public override void Dibujar (GameTime gameTime)
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
		public override IShape GetBounds ()
		{
			return (Moggle.Shape.Rectangle)font.GetStringRectangle (
				Texto (),
				Posición.ToVector2 ());
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		public override void LoadContent ()
		{
			font = Screen.Content.Load<BitmapFont> (UseFont);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.Etiqueta"/> object.
		/// </summary>
		protected override void Dispose ()
		{
			font = null;
			base.Dispose ();
		}
	}
}
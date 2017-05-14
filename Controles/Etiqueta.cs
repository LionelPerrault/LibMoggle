using System;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;

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
		public virtual Func<string> Texto { get; set; }

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
		protected override Rectangle GetBounds ()
		{
			return Font.GetStringRectangle (
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

		/// <summary>
		/// La <see cref="BitmapFont"/> que se usa para imprimir el texto
		/// </summary>
		protected BitmapFont Font;

		/// <summary>
		/// Dibuja el control
		/// </summary>
		protected override void Draw ()
		{
			var bat = Screen.Batch;
			var txt = Texto ();
			bat.DrawString (Font, txt, Posición.ToVector2 (), Color);
		}

		#endregion

		#region Memoria

		/// <summary>
		/// Loads the content using a given manager
		/// </summary>
		/// <param name="manager">Manager.</param>
		protected override void LoadContent (Microsoft.Xna.Framework.Content.ContentManager manager)
		{
			Font = Screen.Content.Load<BitmapFont> (UseFont);
		}

		#endregion
	}
}
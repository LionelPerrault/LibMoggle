using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using OpenTK.Input;
using System.Collections.Generic;
using Moggle.Controles;
using Moggle.Screens;
using Moggle.IO;
using Moggle.Shape;

namespace Moggle.Controles
{
	/// <summary>
	/// Permite entrar un renglón de texto
	/// </summary>
	public class EntradaTexto : SBC
	{
		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public EntradaTexto (IScreen screen)
			: base (screen)
		{
			Texto = "";

			TeclasPermitidas = new Dictionary<Key, string> ();

			// Construir teclas permitidas
			for (Key i = Key.A; i <= Key.Z; i++)
			{
				TeclasPermitidas.Add (i, i.ToString ());
			}
			for (Key i = Key.Number0; i <= Key.Number9; i++)
			{
				TeclasPermitidas.Add (i, ((int)i - 109).ToString ());
			}
		}

		#region Estado

		/// <summary>
		/// Devuelve o establece el texto visible (editable)
		/// </summary>
		public string Texto { get; set; }

		/// <summary>
		/// Si el control debe responder al estado del teclado.
		/// </summary>
		public bool CatchKeys = true;

		#endregion

		/// <summary>
		/// Devuelve o establece la posición del control.
		/// </summary>
		/// <value>The position.</value>
		public Point Pos
		{
			get
			{
				return Bounds.TopLeft.ToPoint ();
			}
			set
			{
				Bounds = new Shape.Rectangle (value, Bounds.Size);
			}
		}

		/// <summary>
		/// Color del contorno
		/// </summary>
		public Color ColorContorno = Color.White;
		/// <summary>
		/// Color del texto
		/// </summary>
		public Color ColorTexto = Color.White;
		Texture2D contornoTexture;
		BitmapFont fontTexture;

		/// <summary>
		/// Límites de el control
		/// </summary>
		public Moggle.Shape.Rectangle Bounds { get; set; }

		/// <summary>
		/// Dibuja el control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			Primitivos.DrawRectangle (
				bat,
				GetBounds ().GetContainingRectangle (),
				ColorContorno,
				contornoTexture);
			bat.DrawString (fontTexture, Texto, Pos.ToVector2 (), ColorTexto);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		public override void LoadContent ()
		{
			contornoTexture = Screen.Content.Load<Texture2D> ("Rect");
			fontTexture = Screen.Content.Load<BitmapFont> ("fonts");
		}

		/// <summary>
		/// Esta función establece el comportamiento de este control cuando el jugador presiona una tecla dada.
		/// </summary>
		/// <param name="key">Tecla presionada por el usuario.</param>
		public override void CatchKey (Key key)
		{
			if (TeclasPermitidas.ContainsKey (key))
			{
				var tx = TeclasPermitidas [key];
				if (!InputManager.EstadoActualTeclado.IsKeyDown (Key.ShiftLeft) && !InputManager.EstadoActualTeclado.IsKeyDown (Key.ShiftRight))
					tx = tx.ToLower ();
				Texto += tx;
			}

			if (key == Key.Space)
			{
				Texto += " ";
			}
			if (key == Key.Back)
			{
				if (Texto.Length > 0)
					Texto = Texto.Remove (Texto.Length - 1);
			}
		}

		/// <summary>
		/// Devuelve o establece el mapeo entre tecla presionada y el caracter o cadena visible.
		/// </summary>
		public IDictionary<Key, string> TeclasPermitidas { get; set; }

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		public override IShape GetBounds ()
		{
			return Bounds;
		}
	}
}
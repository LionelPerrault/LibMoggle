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

		public string Texto { get; set; }

		/// <summary>
		/// Si el control debe responder a el estado del teclado.
		/// </summary>
		public bool CatchKeys = true;

		#endregion

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

		public override void LoadContent ()
		{
			contornoTexture = Screen.Content.Load<Texture2D> ("Rect");
			fontTexture = Screen.Content.Load<BitmapFont> ("fonts");
		}

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

		public IDictionary<Key, string> TeclasPermitidas { get; set; }

		public override IShape GetBounds ()
		{
			return Bounds;
		}
	}
}
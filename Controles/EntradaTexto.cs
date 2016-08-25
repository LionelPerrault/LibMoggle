using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Moggle.Comm;
using Moggle.Controles;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	/// <summary>
	/// Permite entrar un renglón de texto
	/// </summary>
	public class EntradaTexto : SBC, IReceptorTeclado
	{
		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public EntradaTexto (IScreen screen)
			: base (screen)
		{
			Texto = "";

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
		public Vector2 Pos
		{
			get
			{
				return Bounds.Location;
			}
			set
			{
				Bounds = new RectangleF (value, Bounds.Size);
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
		public RectangleF Bounds { get; set; }

		/// <summary>
		/// Dibuja el control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Draw (GameTime gameTime)
		{
			var bat = Screen.Batch;
			Primitivos.DrawRectangle (
				bat,
				GetBounds ().GetBoundingRectangle (),
				ColorContorno,
				contornoTexture);
			bat.DrawString (fontTexture, Texto, Pos, ColorTexto);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void LoadContent ()
		{
			contornoTexture = Screen.Content.Load<Texture2D> ("Rect");
			fontTexture = Screen.Content.Load<BitmapFont> ("fonts");
		}

		/// <summary>
		/// Esta función establece el comportamiento de este control cuando el jugador presiona una tecla dada.
		/// </summary>
		/// <param name="key">Tecla presionada por el usuario.</param>
		bool IReceptorTeclado.RecibirSeñal (KeyboardEventArgs key)
		{

			if (key.Key == Keys.Back)
			{
				if (Texto.Length > 0)
					Texto = Texto.Remove (Texto.Length - 1);
				return true;
			}

			Texto += key.Character;
			return true;
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		public override IShapeF GetBounds ()
		{
			return Bounds;
		}
	}
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Comm;
using Moggle.Controles;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;
using Microsoft.Xna.Framework.Content;

namespace Moggle.Controles
{
	/// <summary>
	/// Permite entrar un renglón de texto
	/// </summary>
	public class EntradaTexto : DSBC, IReceptorTeclado
	{
		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public EntradaTexto (IScreen screen)
			: base (screen)
		{
			StringListen = new KeyStringListener ();
		}

		#endregion

		#region Estado

		/// <summary>
		/// Devuelve o establece el texto visible (editable)
		/// </summary>
		public string Texto
		{
			get { return StringListen.CurrentString; }
			set { StringListen.CurrentString = value; }
		}

		/// <summary>
		/// Si el control debe responder al estado del teclado.
		/// </summary>
		public bool CatchKeys = true;

		/// <summary>
		/// Gets or sets the background texture.
		/// </summary>
		/// <value>The background texture.</value>
		public string BgTexture { get; set; }

		/// <summary>
		/// Gets or sets the font texture.
		/// </summary>
		/// <value>The font texture.</value>
		public string FontTexture { get; set; }

		#endregion

		#region Comportamiento

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

		/// <summary>
		/// Límites de el control
		/// </summary>
		public RectangleF Bounds { get; set; }

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		protected override IShapeF GetBounds ()
		{
			return Bounds;
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

		Texture2D contornoTexture;
		BitmapFont fontTexture;

		/// <summary>
		/// Dibuja el control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		protected override void Draw (GameTime gameTime)
		{
			var bat = Screen.Batch;
			Primitivos.DrawRectangle (
				bat,
				GetBounds ().GetBoundingRectangle (),
				ColorContorno,
				contornoTexture);
			bat.DrawString (fontTexture, Texto, Pos, ColorTexto);
		}

		#endregion

		#region Memoria

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void AddContent (BibliotecaContenido manager)
		{
			manager.AddContent (BgTexture);
			manager.AddContent (FontTexture);
			//contornoTexture = manager.Load<Texture2D> (BgTexture);
			//fontTexture = manager.Load<BitmapFont> (FontTexture);
		}

		/// <summary>
		/// Unloads the content.
		/// </summary>
		protected override void Dispose ()
		{
			contornoTexture = null;
			fontTexture = null;
		}

		#endregion

		#region Teclado

		KeyStringListener StringListen { get; }

		/// <summary>
		/// Esta función establece el comportamiento de este control cuando el jugador presiona una tecla dada.
		/// </summary>
		/// <param name="key">Tecla presionada por el usuario.</param>
		bool IReceptorTeclado.RecibirSeñal (KeyboardEventArgs key)
		{
			return StringListen.RecibirSeñal (key);
		}

		#endregion
	}
}
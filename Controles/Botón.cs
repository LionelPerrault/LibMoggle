using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Inputs = MonoGame.Extended.InputListeners;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	/// <summary>
	/// Un control que interactúa con los clicks del ratón.
	/// </summary>
	public class Botón : SBC, IColorable
	{
		#region ctor

		/// <summary>
		/// Inicaliza un <see cref="Botón"/> rectangular.
		/// </summary>
		/// <param name="screen">Screen.</param>
		public Botón (Moggle.Screens.IScreen screen)
			: base (screen)
		{
			Color = Color.White;
		}

		/// <summary>
		/// Inicaliza un <see cref="Botón"/> de una forma arbitraria.
		/// </summary>
		/// <param name="screen">Screen.</param>
		/// <param name="shape">Forma del botón</param>
		public Botón (Moggle.Screens.IScreen screen, IShapeF shape)
			: this (screen)
		{
			Bounds = shape;
		}

		/// <summary>
		/// Inicaliza un <see cref="Botón"/> rectangular.
		/// </summary>
		/// <param name="screen">Screen.</param>
		/// <param name="bounds">Límites del rectángulo.</param>
		public Botón (Moggle.Screens.IScreen screen,
		              RectangleF bounds)
			: this (screen)
		{
			Bounds = new RectangleF (bounds.Location, bounds.Size);
		}

		#endregion

		/// <summary>
		/// Devuelve o establece si el botón está habilitado para interacción con el jugador.
		/// </summary>
		/// <value><c>true</c> if habilidato; otherwise, <c>false</c>.</value>
		public bool Habilidato { get; set; }

		/// <summary>
		/// Devuelve o restablece la forma del botón
		/// </summary>
		/// <value>The bounds.</value>
		public IShapeF Bounds { get; set; }

		/// <summary>
		/// Textura del fondo del botón
		/// </summary>
		public Texture2D TexturaInstancia { get; protected set; }

		/// <summary>
		/// Devuelve o establece el color de la textura de fondo <see cref="TexturaInstancia"/>
		/// </summary>
		/// <value>The color.</value>
		public Color Color { get; set; }

		/// <summary>
		/// Devuelve o establece el nombre de la textura.
		/// Si se establece, <see cref="LoadContent"/> cargará la <see cref="TexturaInstancia"/>
		/// </summary>
		public string Textura { get; set; }

		/// <summary>
		/// Dibuja el botón
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Draw (GameTime gameTime)
		{
			Screen.Batch.Draw (TexturaInstancia, Bounds, Color);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void LoadContent ()
		{
			TexturaInstancia = Screen.Content.Load<Texture2D> (Textura);
		}

		/// <summary>
		/// Unloads the content.
		/// </summary>
		protected override void UnloadContent ()
		{
			Textura = null;
			base.UnloadContent ();
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.Botón"/> object.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Moggle.Controles.Botón"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Moggle.Controles.Botón"/>.</returns>
		public override string ToString ()
		{
			return string.Format ("{0}{1}", Habilidato ? "[H]" : "", Textura);
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		public override IShapeF GetBounds ()
		{
			return Bounds;
		}

		/// <summary>
		/// Botón hecho clic.
		/// </summary>
		/// <param name="args">Argumentos de ratón.</param>
		protected override void OnClick (MouseEventArgs args)
		{
			AlClick?.Invoke (this, args);
			switch (args.Button)
			{
				case MouseButton.Left:
					AlClickIzquierdo?.Invoke (this, args);
					break;

				case MouseButton.Right:
					AlClickDerecho?.Invoke (this, args);
					break;
			}
		}

		/// <summary>
		/// Ocurre cuando se hace click (cualquiera( en este control.
		/// </summary>
		public event EventHandler<MouseEventArgs> AlClick;
		/// <summary>
		/// Ocurre cuando se hace click izquierdo en este control.
		/// </summary>
		public event EventHandler AlClickIzquierdo;
		/// <summary>
		/// Ucurre cuando se hace click derecho en este control
		/// </summary>
		public event EventHandler AlClickDerecho;
	}
}
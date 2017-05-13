using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controles
{
	/// <summary>
	/// Un control que interactúa con los clicks del ratón.
	/// </summary>
	public class Botón : DSBC, IColorable, IBotón
	{
		#region Comportamiento

		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="Botón"/>.
		/// </summary>
		public override string ToString ()
		{
			return string.Format ("{0}{1}", Habilidato ? "[H]" : "", Textura);
		}

		/// <summary>
		/// Update lógico
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (GameTime gameTime)
		{
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		protected override Rectangle GetBounds ()
		{
			return Bounds;
		}

		/// <summary>
		/// Devuelve o establece si el botón está habilitado para interacción con el jugador.
		/// </summary>
		/// <value><c>true</c> if habilidato; otherwise, <c>false</c>.</value>
		public bool Habilidato { get; set; }

		/// <summary>
		/// Devuelve o restablece la forma del botón
		/// </summary>
		/// <value>The bounds.</value>
		public Rectangle Bounds { get; set; }

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
		/// </summary>
		public string Textura { get; set; }

		#endregion

		#region Dibujo

		/// <summary>
		/// Dibuja el botón
		/// </summary>
		protected override void Draw ()
		{
			Screen.Batch.Draw (TexturaInstancia, Bounds, Color);
		}

		#endregion

		#region Memoria

		/// <summary>
		/// Loads the content using a given manager
		/// </summary>
		/// <param name="manager">Manager.</param>
		protected override void LoadContent (Microsoft.Xna.Framework.Content.ContentManager manager)
		{
			TexturaInstancia = Screen.Content.Load<Texture2D> (Textura);
		}

		#endregion

		#region Eventos

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

		bool IActivable.Activar ()
		{
			AlClickIzquierdo?.Invoke (this, EventArgs.Empty);
			return true;
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

		event EventHandler IActivable.AlActivar
		{
			add { AlClickIzquierdo += value; }
			remove { AlClickIzquierdo += value; }
		}

		#endregion

		#region ctor

		/// <summary>
		/// Inicaliza un <see cref="Botón"/> rectangular.
		/// </summary>
		/// <param name="screen">Screen.</param>
		public Botón (Screens.IScreen screen)
			: base (screen)
		{
			Color = Color.White;
		}

		/// <summary>
		/// Inicaliza un <see cref="Botón"/> de una forma arbitraria.
		/// </summary>
		/// <param name="screen">Screen.</param>
		/// <param name="shape">Forma del botón</param>
		public Botón (Screens.IScreen screen, Rectangle shape)
			: this (screen)
		{
			Bounds = shape;
		}

		#endregion
	}
}
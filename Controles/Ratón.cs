using Moggle.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;
using Moggle.Shape;
using System;

namespace Moggle.Controles
{
	/// <summary>
	/// Control simple que hace visible al apuntador del ratón.
	/// </summary>
	public class Ratón : SBC
	{
		/// <summary>
		/// </summary>
		/// <param name="scr">Pantalla</param>
		/// <param name="tamaño">Tamaño del icono del cursor.</param>
		public Ratón (IScreen scr, Point tamaño)
			: this (scr)
		{
			Tamaño = tamaño;
		}

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public Ratón (IScreen screen)
			: base (screen)
		{
			Tamaño = new Point (20, 20);
			Prioridad = 1000;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Moggle.Controles.Ratón"/> class.
		/// </summary>
		public Ratón ()
			: base (null)
		{
			Prioridad = 1000;
		}

		/// <summary>
		/// Devuelve o establece el archivo que contiene la textura del ratón.
		/// </summary>
		public string ArchivoTextura { get; set; }

		/// <summary>
		/// Devuelve un valor determinando si el ratón está habilitado para esta aplicación.
		/// </summary>
		public bool Habilitado
		{
			get
			{
				return Textura != null || !string.IsNullOrWhiteSpace (ArchivoTextura);
			}
		}

		/// <summary>
		/// Devuelve la textura usada.
		/// </summary>
		public Texture2D Textura { get; protected set; }

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.Ratón"/> object.
		/// </summary>
		protected override void Dispose ()
		{
			Textura = null;
			base.Dispose ();
		}

		/// <summary>
		/// Devuelve o establece la posición actual del apuntador del ratón.
		/// </summary>
		/// <value>The position.</value>
		public static Point Pos
		{
			get
			{
				return Microsoft.Xna.Framework.Input.Mouse.GetState ().Position;
			}
			set
			{
				Mouse.SetPosition (value.X, value.Y);
			}
		}

		/// <summary>
		/// Devuelve el tamaño del apuntador.
		/// </summary>
		public readonly Point Tamaño;

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		public override IShape GetBounds ()
		{
			return new Moggle.Shape.Rectangle (Pos, Tamaño);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		public override void LoadContent ()
		{
			Textura = Screen.Content.Load<Texture2D> (ArchivoTextura);
		}

		/// <summary>
		/// Dibuja el control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			bat.Draw (Textura, GetBounds ().GetContainingRectangle (), Color.WhiteSmoke);
		}
	}
}
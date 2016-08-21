using Moggle.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;
using Moggle.Shape;
using System;

namespace Moggle.Controles
{
	public class Ratón : SBC
	{
		public Ratón (IScreen scr, Point tamaño)
			: this (scr)
		{
			Tamaño = tamaño;
		}

		public Ratón (IScreen screen)
			: base (screen)
		{
			Tamaño = new Point (20, 20);
			Prioridad = 1000;
		}

		public Ratón ()
			: base (null)
		{
			Tamaño = new Point (15, 15);
			Prioridad = 1000;
		}

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

		protected override void Dispose ()
		{
			Textura = null;
			base.Dispose ();
		}

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


		public readonly Point Tamaño;

		public override IShape GetBounds ()
		{
			return new Moggle.Shape.Rectangle (Pos, Tamaño);
		}

		public override void LoadContent ()
		{
			Textura = Screen.Content.Load<Texture2D> (ArchivoTextura);
		}

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			bat.Draw (Textura, GetBounds ().GetContainingRectangle (), Color.WhiteSmoke);
		}
	}
}
using Moggle.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;

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

		public override Rectangle GetBounds ()
		{
			return new Rectangle (Pos, Tamaño);
		}

		public override void LoadContent ()
		{
			Textura = Screen.Content.Load<Texture2D> (ArchivoTextura);
		}

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			bat.Draw (Textura, GetBounds (), Color.WhiteSmoke);
		}
	}
}


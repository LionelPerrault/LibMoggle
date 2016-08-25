using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;
using MonoGame.Extended.Shapes;
using MonoGame.Extended;

namespace Moggle.Controles
{
	/// <summary>
	/// Control simple que hace visible al apuntador del ratón.
	/// </summary>
	public class Ratón : SBC
	{
		/// <summary>
		/// </summary>
		/// <param name="gm">Pantalla</param>
		/// <param name="tamaño">Tamaño del icono del cursor.</param>
		public Ratón (Game gm, Size tamaño)
			: base (gm)
		{
			Tamaño = tamaño;
		}

		/// <summary>
		/// </summary>
		/// <param name="gm">Screen.</param>
		public Ratón (Game gm)
			: base (gm)
		{
			Tamaño = new Size (20, 20);
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
		/// Shuts down the component.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose (bool disposing)
		{
			Textura = null;
			base.Dispose (disposing);
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
		public readonly Size Tamaño;

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		public override IShapeF GetBounds ()
		{
			return new RectangleF (Pos.ToVector2 (), (SizeF)Tamaño);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void LoadContent ()
		{
			Textura = Game.Content.Load<Texture2D> (ArchivoTextura);
		}

		/// <summary>
		/// Dibuja el control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Draw (GameTime gameTime)
		{
			var bat = Game.GetNewBatch ();
			bat.Begin ();
			bat.Draw (
				Textura,
				(Rectangle)GetBounds ().GetBoundingRectangle (),
				Color.WhiteSmoke);
			bat.End ();
		}
	}
}
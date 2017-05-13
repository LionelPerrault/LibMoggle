using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Moggle.Controles
{
	/// <summary>
	/// Control simple que hace visible al apuntador del ratón.
	/// </summary>
	public class Ratón : DSBC
	{
		#region Dibujo

		/// <summary>
		/// Devuelve o establece el offset del cursor con respecto a su posición real
		/// </summary>
		public Point OffSet { get; set; }

		/// <summary>
		/// Devuelve o establece el archivo que contiene la textura del ratón.
		/// </summary>
		public string ArchivoTextura { get; set; }

		/// <summary>
		/// Devuelve la textura usada.
		/// </summary>
		public Texture2D Textura { get; protected set; }
		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		protected override Rectangle GetBounds ()
		{
			return new Rectangle (Pos.X, Pos.Y, Tamaño.Width, Tamaño.Height);
		}

		Rectangle GetOffsetBounds ()
		{
			return new Rectangle ((Pos + OffSet), new Point (Tamaño.Width, Tamaño.Height));
		}

		readonly SpriteBatch drawBatch;

		/// <summary>
		/// Dibuja el control
		/// </summary>
		protected override void Draw ()
		{
			var bat = drawBatch;
			bat.Begin ();
			bat.Draw (
				Textura,
				GetOffsetBounds (),
				Color.WhiteSmoke);
			bat.End ();
		}

		#endregion

		#region Comportamiento

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
		/// Devuelve o establece la posición actual del apuntador del ratón.
		/// </summary>
		/// <value>The position.</value>
		public static Point Pos
		{
			get
			{
				return Mouse.GetState ().Position;
			}
			set
			{
				Mouse.SetPosition (value.X, value.Y);
			}
		}

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores.
		/// No invoca LoadContent por lo que es seguro agregar componentes
		/// </summary>
		protected override void ForceInitialization ()
		{
			base.ForceInitialization ();
			var displ = Game.GraphicsDevice.Adapter.CurrentDisplayMode;
			Pos = new Point (displ.Width / 2, displ.Height / 2);
		}

		/// <summary>
		/// Devuelve el tamaño del apuntador.
		/// </summary>
		public readonly CE.Size Tamaño;

		/// <summary>
		/// Update lógico
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (GameTime gameTime)
		{
		}

		#endregion

		#region Memoria


		#endregion

		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="gm">Pantalla</param>
		/// <param name="tamaño">Tamaño del icono del cursor.</param>
		public Ratón (Game gm, CE.Size tamaño)
			: base (gm)
		{
			Tamaño = tamaño;
			drawBatch = Game.GetNewBatch ();
		}

		/// <summary>
		/// </summary>
		/// <param name="gm">Screen.</param>
		public Ratón (Game gm)
			: base (gm)
		{
			Tamaño = new CE.Size (20, 20);
			drawBatch = Game.GetNewBatch ();
		}

		#endregion
	}
}
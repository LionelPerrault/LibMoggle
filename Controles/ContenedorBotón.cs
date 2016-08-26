using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Screens;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	/// <summary>
	/// Un control rectangular que inteligentemente acomoda una lista de botones.
	/// </summary>
	public class ContenedorBotón : DSBC
	{
		/// <summary>
		/// Tipo de orden lexicográfico para los botones.
		/// </summary>
		public enum TipoOrdenEnum
		{
			/// <summary>
			/// Llena las columnas antes que las filas.
			/// </summary>
			ColumnaPrimero,
			/// <summary>
			/// Llena las filas antes que las columnas.
			/// </summary>
			FilaPrimero
		}

		/// <summary>
		/// Margen.
		/// </summary>
		public struct MargenType
		{
			/// <summary>
			/// Margen superior
			/// </summary>
			public int Top;
			/// <summary>
			/// Margen inferior
			/// </summary>
			public int Bot;
			/// <summary>
			/// Márgen izquierdo.
			/// </summary>
			public int Left;
			/// <summary>
			/// Márgen derecho.
			/// </summary>
			public int Right;
		}

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public ContenedorBotón (IScreen screen)
			: base (screen)
		{
			controles = new GameComponentCollection ();
		}

		/// <summary>
		/// Lista de botones en el contenedor.
		/// </summary>
		GameComponentCollection controles { get; }

		Texture2D texturaFondo;
		/// <summary>
		/// Color de fondo.
		/// </summary>
		public Color BgColor = Color.DarkBlue;

		/// <summary>
		/// Nombre de la textura de fondo
		/// </summary>
		public string TextureFondo { get; set; }

		int filas = 3;
		int columnas = 10;
		MargenType márgenes;
		Point tamañobotón = new Point (30, 30);
		Point posición;
		TipoOrdenEnum tipoOrden;

		public override void Update (GameTime gameTime)
		{
		}

		/// <summary>
		/// Devuelve o establece el tipo de orden en el que se establece la posición
		/// de los botones.
		/// </summary>
		public TipoOrdenEnum TipoOrden
		{
			get
			{
				return tipoOrden;
			}
			set
			{
				tipoOrden = value;
				OnRedimensionar ();
			}
		}

		/// <summary>
		/// Devuelve o establece la posición del control.
		/// </summary>
		public Point Posición
		{
			get
			{
				return posición;
			}
			set
			{
				posición = value;
				OnRedimensionar ();
			}
		}

		/// <summary>
		/// Devuelve o establece el tamaño de los botones.
		/// </summary>
		/// <value>The tamaño botón.</value>
		public Point TamañoBotón
		{
			get
			{
				return tamañobotón;
			}
			set
			{
				tamañobotón = value;
				OnRedimensionar ();
			}
		}

		/// <summary>
		/// Devuelve o establece el márgen de los botones respecto a ellos mismos y al contenedor.
		/// </summary>
		/// <value>The márgenes.</value>
		public MargenType Márgenes
		{
			get
			{
				return márgenes;
			}
			set
			{
				márgenes = value;
				OnRedimensionar ();
			}
		}

		/// <summary>
		/// Devuelve o establece el número de columnas que puede contener.
		/// </summary>
		public int Columnas
		{
			get
			{
				return columnas;
			}
			set
			{
				columnas = value;
				OnRedimensionar ();
			}
		}

		/// <summary>
		/// Devuelve o establece el número de filas que puede contener.
		/// </summary>
		public int Filas
		{
			get
			{
				return filas;
			}
			set
			{
				filas = value;
				OnRedimensionar ();
			}
		}

		/// <summary>
		/// Agrega un botón al contenedor y lo devuelve.
		/// </summary>
		public Botón Add ()
		{
			return Add (Count);
		}

		/// <summary>
		/// Inserta un botón en un índice dado, y lo devuelve.
		/// </summary>
		/// <param name="índice">Índice del botón</param>
		public Botón Add (int índice)
		{
			var ret = new Botón (Screen, CalcularPosición (Count));
			controles.Insert (índice, ret);
			ret.Habilidato = true;
			return ret;
		}

		/// <summary>
		/// Vacía y desecha los botones.
		/// </summary>
		public void Clear ()
		{
			controles.Clear ();
		}

		/// <summary>
		/// Recalcula la posición de cada botón
		/// </summary>
		protected void OnRedimensionar ()
		{
			for (int i = 0; i < Count; i++)
			{
				var bot = BotónEnÍndice (i);
				bot.Bounds = CalcularPosición (i);
			}
		}

		/// <summary>
		/// Elimina un botón dado.
		/// </summary>
		/// <param name="control">Botón a eliminar.</param>
		public void Remove (Botón control)
		{
			controles.Remove (control);
		}

		/// <summary>
		/// Elimina el botón en un índice dado
		/// </summary>
		/// <param name="i">Índice base cero.</param>
		public void RemoveAt (int i)
		{
			controles.RemoveAt (i);
		}

		/// <summary>
		/// Devuelve el número de botones.
		/// </summary>
		public int Count
		{
			get
			{
				return controles.Count;
			}
		}

		/// <summary>
		/// Dibuja el control.
		/// Esto por sí solo no dibujará los botones.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Draw (GameTime gameTime)
		{
			Screen.Batch.Draw (
				texturaFondo,
				GetBounds (),
				BgColor);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void LoadContent ()
		{
			texturaFondo = Screen.Content.Load<Texture2D> (TextureFondo);
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		public override IShapeF GetBounds ()
		{
			return new RectangleF (Posición.ToVector2 (),
				new SizeF (
					Márgenes.Left + Márgenes.Right + Columnas * TamañoBotón.X,
					Márgenes.Top + Márgenes.Bot + Filas * TamañoBotón.Y));
		}

		/// <summary>
		/// Devuelve el botón que está en un índice dado.
		/// </summary>
		/// <param name="index">Índice base cero del botón.</param>
		public Botón BotónEnÍndice (int index)
		{
			return (Botón)controles [index];
		}

		/// <summary>
		/// Calcula y devuelve el rectángulo de posición de el botón de un índice dado.
		/// </summary>
		/// <param name="index">Índice del botón.</param>
		protected RectangleF CalcularPosición (int index)
		{
			RectangleF bounds;
			var bb = GetBounds ().GetBoundingRectangle ();
			Point locGrid;
			int orden = index;
			locGrid = TipoOrden == TipoOrdenEnum.ColumnaPrimero ? 
				new Point (orden / Filas, orden % Filas) : 
				new Point (orden % Columnas, orden / Columnas);
			bounds = new RectangleF (bb.Left + Márgenes.Left + TamañoBotón.X * locGrid.X,
				bb.Top + Márgenes.Top + TamañoBotón.Y * locGrid.Y,
				TamañoBotón.X, TamañoBotón.Y);
			return bounds;
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.ContenedorBotón"/> object.
		/// Libera a este control y a cada uno de sus botones.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			texturaFondo = null;
			base.Dispose (disposing);
		}
	}
}
using Moggle.Screens;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Moggle.Shape;

namespace Moggle.Controles
{
	/// <summary>
	/// Un control rectangular que inteligentemente acomoda una lista de botones.
	/// </summary>
	public class ContenedorBotón : SBC
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
			Prioridad = -5;
			controles = new List<Botón> (Filas * Columnas);
		}

		/// <summary>
		/// Lista de botones en el contenedor.
		/// </summary>
		List<Botón> controles { get; }

		Texture2D texturaFondo;
		/// <summary>
		/// Color de fondo.
		/// </summary>
		public Color BgColor = Color.DarkBlue;

		int filas = 3;
		int columnas = 10;
		MargenType márgenes;
		Point tamañobotón = new Point (30, 30);
		Point posición;
		TipoOrdenEnum tipoOrden;

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
			ret.Include ();
			return ret;
		}

		/// <summary>
		/// Vacía y desecha los botones.
		/// </summary>
		public void Clear ()
		{
			foreach (var x in controles)
				x.Exclude ();
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
			control.Exclude ();
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
		/// Incluir este control y todos sus botones en su pantalla.
		/// </summary>
		public override void Include ()
		{
			// Incluye a cada botón en Game
			foreach (var x in controles)
				x.Include ();

			base.Include ();
		}

		/// <summary>
		/// Excluir este control y todos sus botones de su pantalla
		/// </summary>
		public override void Exclude ()
		{
			// Excluye a cada botón de Game
			foreach (var x in controles)
				x.Exclude ();
			base.Exclude ();
		}

		/// <summary>
		/// Dibuja el control.
		/// Esto por sí solo no dibujará los botones.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Dibujar (GameTime gameTime)
		{
			Screen.Batch.Draw (
				texturaFondo,
				GetBounds ().GetContainingRectangle (),
				BgColor);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		public override void LoadContent ()
		{
			texturaFondo = Screen.Content.Load<Texture2D> ("Rect");
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		public override IShape GetBounds ()
		{
			return new Moggle.Shape.Rectangle (Posición,
				new Point (
					Márgenes.Left + Márgenes.Right + Columnas * TamañoBotón.X,
					Márgenes.Top + Márgenes.Bot + Filas * TamañoBotón.Y));
		}

		/// <summary>
		/// Devuelve el botón que está en un índice dado.
		/// </summary>
		/// <param name="index">Índice base cero del botón.</param>
		public Botón BotónEnÍndice (int index)
		{
			return controles [index];
		}

		/// <summary>
		/// Calcula y devuelve el rectángulo de posición de el botón de un índice dado.
		/// </summary>
		/// <param name="index">Índice del botón.</param>
		protected Moggle.Shape.Rectangle CalcularPosición (int index)
		{
			Moggle.Shape.Rectangle bounds;
			var bb = GetBounds ().GetContainingRectangle (); // Cota del contenedor
			Point locGrid;
			int orden = index;
			locGrid = TipoOrden == TipoOrdenEnum.ColumnaPrimero ? 
				new Point (orden / Filas, orden % Filas) : 
				new Point (orden % Columnas, orden / Columnas);
			bounds = new Moggle.Shape.Rectangle (bb.Left + Márgenes.Left + TamañoBotón.X * locGrid.X,
				bb.Top + Márgenes.Top + TamañoBotón.Y * locGrid.Y,
				TamañoBotón.X, TamañoBotón.Y);
			return bounds;
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.ContenedorBotón"/> object.
		/// Libera a este control y a cada uno de sus botones.
		/// </summary>
		protected override void Dispose ()
		{
			texturaFondo = null;
			foreach (IControl x in controles)
				x.Dispose ();
			Clear ();
			base.Dispose ();
		}
	}
}
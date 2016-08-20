using Moggle.Screens;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Moggle.Shape;

namespace Moggle.Controles
{
	public class ContenedorBotón : SBC
	{
		public enum TipoOrdenEnum
		{
			ColumnaPrimero,
			FilaPrimero
		}

		public struct MargenType
		{
			public int Top;
			public int Bot;
			public int Left;
			public int Right;
		}

		public ContenedorBotón (IScreen screen)
			: base (screen)
		{
			Prioridad = -5;
			controles = new List<Botón> (Filas * Columnas);
		}

		List<Botón> controles { get; }

		Texture2D texturaFondo;
		public Color BgColor = Color.DarkBlue;

		int filas = 3;
		int columnas = 10;
		MargenType márgenes;
		Point tamañobotón = new Point (30, 30);
		Point posición;
		TipoOrdenEnum tipoOrden;

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

		public Botón Add ()
		{
			return Add (Count);
		}

		public Botón Add (int índice)
		{
			var ret = new Botón (Screen, CalcularPosición (Count));
			controles.Insert (índice, ret);
			ret.Habilidato = true;
			ret.Include ();
			return ret;
		}

		public void Clear ()
		{
			foreach (var x in controles)
			{
				x.Exclude ();
			}
			controles.Clear ();
		}

		protected void OnRedimensionar ()
		{
			for (int i = 0; i < Count; i++)
			{
				var bot = BotónEnÍndice (i);
				bot.Bounds = CalcularPosición (i);
			}
		}

		public void Remove (Botón control)
		{
			controles.Remove (control);
			control.Exclude ();
		}

		public int Count
		{
			get
			{
				return controles.Count;
			}
		}

		public override void Include ()
		{
			// Incluye a cada botón en Game
			foreach (var x in controles)
			{
				x.Include ();
			}

			base.Include ();
		}

		public override void Exclude ()
		{
			// Excluye a cada botón de Game
			foreach (var x in controles)
			{
				x.Exclude ();
			}
			base.Exclude ();
		}

		public override void Dibujar (GameTime gameTime)
		{
			Screen.Batch.Draw (
				texturaFondo,
				GetBounds ().GetContainingRectangle (),
				BgColor);
		}

		public override void LoadContent ()
		{
			texturaFondo = Screen.Content.Load<Texture2D> ("Rect");
		}

		public override IShape GetBounds ()
		{
			return new Moggle.Shape.Rectangle (Posición,
				new Point (
					Márgenes.Left + Márgenes.Right + Columnas * TamañoBotón.X,
					Márgenes.Top + Márgenes.Bot + Filas * TamañoBotón.Y));
		}

		public Botón BotónEnÍndice (int index)
		{
			return controles [index];
		}

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

		protected override void Dispose ()
		{
			texturaFondo = null;
			foreach (IControl x in controles)
			{
				x.Dispose ();
			}
			Clear ();
			base.Dispose ();
		}
	}
}
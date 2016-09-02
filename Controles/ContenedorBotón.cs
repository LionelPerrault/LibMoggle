using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Screens;
using MonoGame.Extended;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	/// <summary>
	/// Un control rectangular que inteligentemente acomoda una lista de botones.
	/// </summary>
	public class ContenedorBotón : DSBC
	{
		#region Estado

		int filas = 3;
		int columnas = 10;
		MargenType márgenes;
		Point tamañobotón = new Point (30, 30);
		Point posición;
		TipoOrdenEnum tipoOrden;

		/// <summary>
		/// Lista de botones en el contenedor.
		/// </summary>
		List<InternalBotón> controles { get; }

		#endregion

		#region Dibujo

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

		Texture2D texturaFondo;

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

		#endregion

		#region Comportamiento

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
		/// Color de fondo.
		/// </summary>
		public Color BgColor = Color.DarkBlue;

		/// <summary>
		/// Nombre de la textura de fondo
		/// </summary>
		public string TextureFondo { get; set; }

		/// <summary>
		/// Update the control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
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
		/// Recalcula la posición de cada botón
		/// </summary>
		protected void OnRedimensionar ()
		{
			for (int i = 0; i < Count; i++)
			{
				var bot = botónEnÍndice (i);
				bot.Bounds = CalcularPosición (i);
			}
		}

		#endregion

		#region Contenedor

		InternalBotón botónEnÍndice (int index)
		{
			return controles [index];
		}

		/// <summary>
		/// Devuelve el botón que está en un índice dado.
		/// </summary>
		/// <param name="index">Índice base cero del botón.</param>
		public IBotón BotónEnÍndice (int index)
		{
			return controles [index];
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
			var ret = new InternalBotón (Screen, CalcularPosición (índice));
			controles.Insert (índice, ret);
			initializeButton (índice);
			
			// desplazar los otros controles
			for (int i = índice + 1; i < Count; i++)
				controles [i].Bounds = CalcularPosición (i);
			
			ret.Habilidato = true;
			return ret;
		}

		/// <summary>
		/// Vacía y desecha los botones.
		/// </summary>
		public void Clear ()
		{
			for (int i = 0; i < controles.Count; i++)
				deinitializeButton (i);
			controles.Clear ();
		}

		void deinitializeButton (int index)
		{
			var bt = controles [index];
			bt.Enabled = false;
			bt.AlClick -= clickOnAButton;
		}

		void initializeButton (int index)
		{
			var bt = controles [index];
			bt.Enabled = true;
			bt.AlClick += clickOnAButton;
		}

		/// <summary>
		/// Elimina un botón dado.
		/// </summary>
		/// <param name="control">Botón a eliminar.</param>
		[Obsolete ("Usar RemoveAt")]
		public void Remove (Botón control)
		{
			throw new NotImplementedException ();
			//controles.Remove (control);
		}

		/// <summary>
		/// Elimina el botón en un índice dado
		/// </summary>
		/// <param name="i">Índice base cero.</param>
		public void RemoveAt (int i)
		{
			deinitializeButton (i);
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

		#endregion

		#region Evento

		void clickOnAButton (object sender, MouseEventArgs e)
		{
			var index = controles.IndexOf (sender as InternalBotón);
			AlActivarBotón?.Invoke (
				this,
				new ContenedorBotónIndexEventArgs (
					index,
					controles [index],
					e));
		}

		/// <summary>
		/// Ocurre cuando un botón es activado
		/// </summary>
		public event EventHandler< ContenedorBotónIndexEventArgs> AlActivarBotón;

		#endregion

		#region Memoria

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.ContenedorBotón"/> object.
		/// Libera a este control y a cada uno de sus botones.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			texturaFondo = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void LoadContent ()
		{
			texturaFondo = Screen.Content.Load<Texture2D> (TextureFondo);
		}

		#endregion

		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public ContenedorBotón (IScreen screen)
			: base (screen)
		{
			controles = new List<InternalBotón> ();
		}

		#endregion

		/// <summary>
		/// </summary>
		public class ContenedorBotónIndexEventArgs : EventArgs
		{
			/// <summary>
			/// Gets the index of the pressed button
			/// </summary>
			/// <value>The index.</value>
			public int Index { get; }

			/// <summary>
			/// Gets the pressed button;
			/// </summary>
			public IBotón Botón { get; }

			/// <summary>
			/// The mouse event args
			/// </summary>
			public MouseEventArgs Mouse;

			internal ContenedorBotónIndexEventArgs (int index,
			                                        IBotón bt,
			                                        MouseEventArgs m)
			{
				Index = index;
				Botón = bt;
				Mouse = m;
			}
		}

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

		class InternalBotón : Botón
		{
			public InternalBotón (IScreen screen, RectangleF bounds)
				: base (screen, bounds)
			{
			}
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
	}
}
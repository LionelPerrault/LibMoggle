using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un contenedor de objetos
	/// </summary>
	public class Contenedor<T> : DSBC
		where T : IDibujable
	{
		/// <summary>
		/// Devuelve o establece la lista de objetos
		/// </summary>
		protected List<T> Objetos { get; set; }

		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add (T item)
		{
			Objetos.Add (item);
		}

		/// <summary>
		/// El tipo de orden
		/// </summary>
		public TipoOrdenEnum TipoOrden;

		/// <summary>
		/// Devuelve o establece la posición del control.
		/// </summary>
		public Point Posición { get; set; }

		/// <summary>
		/// Color de fondo.
		/// </summary>
		public Color BgColor = Color.DarkBlue;

		/// <summary>
		/// Nombre de la textura de fondo
		/// </summary>
		public string TextureFondoName { get; set; }

		/// <summary>
		/// Devuelve la textura de fondo cargada.
		/// </summary>
		/// <value>The textura fondo.</value>
		public Texture2D TexturaFondo { get; private set; }

		/// <summary>
		/// Devuelve o establece el tamaño de los botones.
		/// </summary>
		/// <value>The tamaño botón.</value>
		public Size TamañoBotón { get; set; }

		/// <summary>
		/// Devuelve o establece el número de objetos que pueden existir visiblemente en el control
		/// </summary>
		public Size GridSize { get; set; }

		/// <summary>
		/// Devuelve o establece el márgen de los botones respecto a ellos mismos y al contenedor.
		/// </summary>
		/// <value>The márgenes.</value>
		public MargenType Márgenes { get; set; }

		/// <summary>
		/// Devuelve o establece el número de columnas que puede contener.
		/// </summary>
		public int Columnas { get { return GridSize.Width; } }

		/// <summary>
		/// Devuelve o establece el número de filas que puede contener.
		/// </summary>
		public int Filas { get { return GridSize.Height; } }

		/// <summary>
		/// Devuelve el objeto que está en un índice dado.
		/// </summary>
		/// <param name="index">Índice base cero del botón.</param>
		public T BotónEnÍndice (int index)
		{
			return Objetos [index];
		}

		/// <summary>
		/// Dibuja el control.
		/// </summary>
		public override void Draw (GameTime gameTime)
		{
			var bat = Screen.Batch;
			bat.Draw (TexturaFondo, GetBounds ().GetBoundingRectangle (), BgColor);
			for (int i = 0; i < Objetos.Count; i++)
				DrawObject (bat, i);
		}

		/// <summary>
		/// Dibuja el objeto de un índice dado
		/// </summary>
		/// <param name="bat">Sprite batch</param>
		/// <param name="index">Índice del objeto a dibujar</param>
		protected virtual void DrawObject (SpriteBatch bat, int index)
		{
			var item = Objetos [index];
			item.Draw (bat, CalcularPosición (index));
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		public override IShapeF GetBounds ()
		{
			return new RectangleF (Posición.ToVector2 (),
				new SizeF (
					Márgenes.Left + Márgenes.Right + Columnas * TamañoBotón.Width,
					Márgenes.Top + Márgenes.Bot + Filas * TamañoBotón.Height));
		}

		/// <summary>
		/// Update lógico
		/// </summary>
		public override void Update (GameTime gameTime)
		{
		}

		/// <summary>
		/// Calcula y devuelve el rectángulo de posición de el botón de un índice dado.
		/// </summary>
		/// <param name="index">Índice del objeto.</param>
		protected Rectangle CalcularPosición (int index)
		{
			Rectangle bounds;
			Point locGrid;
			int orden = index;
			locGrid = TipoOrden == TipoOrdenEnum.ColumnaPrimero ? 
				new Point (orden / Filas, orden % Filas) : 
				new Point (orden % Columnas, orden / Columnas);
			bounds = new Rectangle (Posición.X + Márgenes.Left + TamañoBotón.Width * locGrid.X,
				Posición.Y + Márgenes.Top + TamañoBotón.Height * locGrid.Y,
				TamañoBotón.Width, TamañoBotón.Height);
			return bounds;
		}

		/// <summary>
		/// Devuelve el número de objetos
		/// </summary>
		public int Count { get { return Objetos.Count; } }

		/// <summary>
		/// This control was clicked.
		/// </summary>
		/// <param name="args">Arguments.</param>
		protected override void OnClick (MonoGame.Extended.InputListeners.MouseEventArgs args)
		{
			Debug.WriteLine ("Click on container");
			for (int i = 0; i < Count; i++)
			{
				var act = Objetos [i] as IActivable;
				if (act != null)
				{
					var rect = CalcularPosición (i);
					if (rect.Contains (args.Position))
						act.Activar ();
				}
			}
		}

		/// <summary>
		/// Representa el orden en el que se enlistan los objetos
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
		/// Loads the content.
		/// </summary>
		/// <param name="manager">Manager.</param>
		protected override void LoadContent (Microsoft.Xna.Framework.Content.ContentManager manager)
		{
			TexturaFondo = manager.Load<Texture2D> (TextureFondoName);
		}

		/// <summary>
		/// </summary>
		/// <param name="cont">Container</param>
		public Contenedor (IComponentContainerComponent<IControl> cont)
			: base (cont)
		{
			Objetos = new List<T> ();
		}
		
	}
	/*

	/// <summary>
	/// Un control rectangular que inteligentemente acomoda una lista de botones.
	/// </summary>
	public class ContenedorBotón : Contenedor<InternalBotón>
	{
		#region Estado

		/// <summary>
		/// Lista de botones en el contenedor.
		/// </summary>
		List<InternalBotón> controles { get; }

		#endregion

		#region Dibujo

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
		protected override void LoadContent (ContentManager manager)
		{
			texturaFondo = manager.Load<Texture2D> (TextureFondo);
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


	}

	public class InternalBotón : Botón
	{
		public InternalBotón (IScreen screen, RectangleF bounds)
			: base (screen, bounds)
		{
		}
	}
	*/
}
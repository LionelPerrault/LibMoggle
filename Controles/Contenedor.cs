using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un contenedor de objetos
	/// </summary>
	public class Contenedor<T> : DSBC, IComponentContainerComponent<T>
		where T : IDibujable
	{
		void IComponentContainerComponent<T>.AddComponent (T component)
		{
			Add (component);
		}

		bool IComponentContainerComponent<T>.RemoveComponent (T component)
		{
			return Remove (component);
		}

		IEnumerable<T> IComponentContainerComponent<T>.Components
		{
			get { return Objetos; }
		}

		/// <summary>
		/// Devuelve o establece la lista de objetos
		/// </summary>
		protected List<T> Objetos { get; set; }

		/// <summary>
		/// Add the specified item.
		/// It initializes the item if it is requiered
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add (T item)
		{
			Objetos.Add (item);
			if (IsInitialized)
			{
				(item as IGameComponent)?.Initialize ();
				(item as IComponent)?.AddContent ();
				(item as IComponent)?.InitializeContent ();
			}
		}

		/// <summary>
		/// Elimina un objeto del contenedor
		/// </summary>
		/// <param name="item">Objeto a eliminar</param>
		public bool Remove (T item)
		{
			return Objetos.Remove (item);
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
		public MargenType MargenExterno { get; set; }

		/// <summary>
		/// Devuelve o establece los márgenes internos de cada item
		/// </summary>
		/// <value>The margen interno.</value>
		public MargenType MargenInterno { get; set; }

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
		protected override void Draw ()
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
			item.Draw (bat, CalcularRectánguloObjeto (index));
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		protected override IShapeF GetBounds ()
		{
			return new RectangleF (Posición.ToVector2 (),
				new SizeF (
					MargenExterno.Left + MargenExterno.Right + Columnas * TamañoBotón.Width,
					MargenExterno.Top + MargenExterno.Bot + Filas * TamañoBotón.Height));
		}

		/// <summary>
		/// Update lógico
		/// </summary>
		public override void Update (GameTime gameTime)
		{
		}

		/// <summary>
		/// Calcula y devuelve el rectángulo de posición de el botón de un índice dado.
		/// Toma en cuenta los maŕgenes
		/// </summary>
		/// <param name="index">Índice del objeto.</param>
		protected Rectangle CalcularRectánguloObjeto (int index)
		{
			return MargenInterno.ExtractMargin (CalcularPosición (index));
		}

		/// <summary>
		/// Calcula y devuelve el rectángulo de posición de el botón de un índice dado.
		/// Ignora márgenes
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
			bounds = new Rectangle (Posición.X + MargenExterno.Left + TamañoBotón.Width * locGrid.X,
				Posición.Y + MargenExterno.Top + TamañoBotón.Height * locGrid.Y,
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
					var rect = CalcularRectánguloObjeto (i);
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
		protected override void AddContent ()
		{
			var manager = Screen.Content;
			manager.AddContent (TextureFondoName);
			foreach (var c in Objetos.OfType<IComponent> ())
				c.AddContent ();
		}

		/// <summary>
		/// Vincula el contenido a campos de clase
		/// </summary>
		protected override void InitializeContent ()
		{
			var manager = Screen.Content;
			TexturaFondo = manager.GetContent<Texture2D> (TextureFondoName);
			foreach (var c in Objetos.OfType<IComponent> ())
				c.InitializeContent ();
		}

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores.
		/// No invoca LoadContent por lo que es seguro agregar componentes
		/// </summary>
		public override void Initialize ()
		{
			foreach (var c in Objetos.OfType<IGameComponent> ())
				c.Initialize ();
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
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Moggle.Comm;
using Moggle.Textures;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles.Listas
{
	/// <summary>
	/// <para>
	/// Un control que muestra una lista genérica de objetos.
	/// </para>
	/// <para>
	/// Interactúa con el teclado.
	/// </para>
	/// </summary>
	public class Lista<TObj> : DSBC, IList<TObj>, IListaControl<TObj>, IReceptorTeclado
	{
		/// <summary>
		/// Representa una entrada de la lista.
		/// </summary>
		public struct Entrada
		{
			/// <summary>
			/// El objeto.
			/// </summary>
			public TObj Objeto;
			/// <summary>
			/// El color.
			/// </summary>
			public Color Color;

			/// <summary>
			/// </summary>
			/// <param name="obj">Objeto</param>
			public Entrada (TObj obj)
				: this (obj, Color.White)
			{
			}

			/// <summary>
			/// </summary>
			/// <param name="obj">Objeto</param>
			/// <param name="color">Color</param>
			public Entrada (TObj obj, Color color)
			{
				Objeto = obj;
				Color = color;
			}
		}

		/// <summary>
		/// Updates the list
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (GameTime gameTime)
		{
		}

		/// <summary>
		/// Dibuja la lista.
		/// </summary>
		/// <param name="gameTime">Duración del tick</param>
		protected override void Draw (GameTime gameTime)
		{
			// Dibujar el rectángulo
			var bat = Screen.Batch;

			Primitivos.DrawRectangle (
				bat,
				Bounds.GetBoundingRectangle (),
				Color.White,
				pixel);

			// Background
			bat.Draw (pixel, Bounds, ColorBG);

			// TODO: Que no se me salga el texto.
			var currY = Bounds.Location;
			var inic = PrimerVisible;
			var final = Math.Min (Objetos.Count, inic + MaxVisible);
			for (int i = inic; i < final; i++)
			{
				var x = Objetos [i];
				var strTxt = Stringificación (x.Objeto);
				if (i == CursorIndex)
				{
					var rect = Fuente.GetStringRectangle (strTxt, currY);
					bat.Draw (pixel, rect, ColorSel);
				}
				bat.DrawString (Fuente, strTxt, currY, x.Color);
				currY.Y += Fuente.LineHeight;
			}
		}

		/// <summary>
		/// Devuelve el número de entradas que son visibles, a lo más
		/// </summary>
		/// <value>The max visible.</value>
		public int MaxVisible
		{
			get
			{
				return (int)Bounds.Height / Fuente.LineHeight;
			}
		}

		int _primerVisible;

		/// <summary>
		/// Devuelve el primer elemento visible en la lista
		/// </summary>
		/// <value>The primer visible.</value>
		protected int PrimerVisible
		{
			get
			{
				return _primerVisible;
			}
			set
			{
				_primerVisible = Math.Max (0, Math.Min (value, Count - MaxVisible));
			}
		}

		/// <summary>
		/// Devuelve la lista de objetos
		/// </summary>
		public List<Entrada> Objetos { get; }

		/// <summary>
		/// Devuelve o establece el método para convertir el objeto en <c>string</c>
		/// </summary>
		public Func<TObj, string> Stringificación { get; set; }

		int _cursorIndex;

		/// <summary>
		/// El índice del cursor
		/// </summary>
		public int CursorIndex
		{
			get
			{
				return _cursorIndex;
			}
			set
			{
				_cursorIndex = Math.Max (Math.Min (Objetos.Count - 1, value), 0);
				AlMoverCursor?.Invoke (this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Devuelve el objeto que está seleccionado por el cursor del control mismo.
		/// </summary>
		public TObj ObjetoEnCursor
		{
			get
			{
				if (CursorIndex < Objetos.Count)
					return Objetos [CursorIndex].Objeto;
				throw new ArgumentOutOfRangeException ();
			}
		}

		/// <summary>
		/// Devuelve o establece la fuente a usar para imprimir el texto.
		/// </summary>
		public BitmapFont Fuente { get; set; }

		Texture2D pixel { get; set; }

		Texture2D cursorTexture { get; set; }

		/// <summary>
		/// Color del fondo del control
		/// </summary>
		public Color ColorBG { get; set; }

		/// <summary>
		/// Color del fondo del elemento seleccionado
		/// </summary>
		public Color ColorSel { get; set; }

		/// <summary>
		/// Devuelve o establece el límite del control
		/// </summary>
		public RectangleF Bounds { get; set; }

		/// <summary>
		/// Devuelve el menor rectángulo que contiene a este control.
		/// </summary>
		protected override IShapeF GetBounds ()
		{
			return Bounds;
		}

		/// <summary>
		/// Devuelve o establece si este control puede interactuar por sí mismo con el teclado
		/// </summary>
		public bool InterceptarTeclado { get; set; }

		/// <summary>
		/// The nombre textura de la fuente.
		/// </summary>
		public string NombreTexturaFuente = "fonts";

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void AddContent (BibliotecaContenido manager)
		{
			manager.AddContent (NombreTexturaFuente);
		}

		/// <summary>
		/// Vincula el contenido a campos de clase
		/// </summary>
		/// <param name="manager">Manager.</param>
		protected override void InitializeContent (BibliotecaContenido manager)
		{
			var st = new SimpleTextures (Game.Device);
			Fuente = manager.GetContent<BitmapFont> (NombreTexturaFuente);
			pixel = st.SolidTexture (new Size (1, 1), Color.White);
			base.InitializeContent (manager);
		}

		/// <summary>
		/// Dispose.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			Fuente = null;
			pixel = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Tecla para desplazarse hacia abajo.
		/// </summary>
		/// <seealso cref="InterceptarTeclado"/>
		public Keys AbajoKey = Keys.Down;

		/// <summary>
		/// Tecla para desplazarse hacia arriba.
		/// </summary>
		/// <seealso cref="InterceptarTeclado"/>
		public Keys ArribaKey = Keys.Up;

		/// <summary>
		/// Catchs the key.
		/// </summary>

		public bool RecibirSeñal (KeyboardEventArgs key)
		{
			if (!InterceptarTeclado)
				return false;
			if (key.Key == AbajoKey)
			{
				SeleccionaSiguiente ();
				return true;
			}
			if (key.Key == ArribaKey)
			{
				SeleccionaAnterior ();
				return true;
			}
			return false;
		}

		#region IListaControl

		/// <summary>
		/// Selecciona el siguiente objeto en la lista
		/// </summary>
		public void SeleccionaSiguiente ()
		{
			if (++CursorIndex >= PrimerVisible + MaxVisible)
				PrimerVisible++;
		}

		/// <summary>
		/// Selecciona el objeto anterior en la lista.
		/// </summary>
		public void SeleccionaAnterior ()
		{
			if (--CursorIndex < PrimerVisible)
				PrimerVisible--;
		}

		/// <summary>
		/// Devuelve el objeto bajo el cursor.
		/// </summary>
		public TObj Seleccionado
		{
			get
			{
				return ObjetoEnCursor;
			}
		}

		object IListaControl.Seleccionado
		{
			get
			{
				return ObjetoEnCursor;
			}
		}

		#endregion

		#region IList

		int IList<TObj>.IndexOf (TObj item)
		{
			var comparer = EqualityComparer<TObj>.Default;
			return Objetos.FindIndex (z => comparer.Equals (z.Objeto, item));
		}

		void IList<TObj>.Insert (int index, TObj item)
		{
			Objetos.Insert (index, new Entrada (item));
		}

		void IList<TObj>.RemoveAt (int index)
		{
			Objetos.RemoveAt (index);
		}

		/// <summary>
		/// Agrega un objeto a la lista.
		/// </summary>
		/// <param name="item">Objeto</param>
		public void Add (TObj item)
		{
			Add (item, Color.White);
		}

		/// <summary>
		/// Agrega un objeto a la lista.
		/// </summary>
		/// <param name="item">Objeto</param>
		/// <param name="color">Color de impresión</param>
		public void Add (TObj item, Color color)
		{
			Add (new Entrada (item, color));
		}

		/// <summary>
		/// Agrega un objeto a la lista.
		/// </summary>
		/// <param name="entrada">Objeto y color</param>
		public void Add (Entrada entrada)
		{
			Objetos.Add (entrada);
		}

		/// <summary>
		/// Vacía los objetos de la lista.
		/// </summary>
		public void Clear ()
		{
			Objetos.Clear ();
		}

		/// <summary>
		/// Determina si un objeto está en la lista
		/// </summary>
		/// <param name="item">Objeto</param>
		public bool Contains (TObj item)
		{
			var comparer = EqualityComparer<TObj>.Default;
			return Objetos.Any (z => comparer.Equals (item, z.Objeto));
		}

		void ICollection<TObj>.CopyTo (TObj [] array, int arrayIndex)
		{
			if (array.Length < arrayIndex + Count)
				throw new IndexOutOfRangeException ("Cannot copy the collection into the array.");
			
			for (int i = 0; i < Objetos.Count - 1; i++)
				array [i + arrayIndex] = Objetos [i].Objeto;
		}

		/// <summary>
		/// Elimina la primera aparición de un objeto en la lista.
		/// </summary>
		public bool Remove (TObj item)
		{
			var comparer = EqualityComparer<TObj>.Default;
			foreach (var i in Objetos)
			{
				if (comparer.Equals (i.Objeto, item))
				{
					Objetos.Remove (i);
					return true;
				}
			}
			return false;
		}

		IEnumerator<TObj> IEnumerable<TObj>.GetEnumerator ()
		{
			foreach (var i in Objetos)
				yield return i.Objeto;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			foreach (var i in Objetos)
				yield return i.Objeto;
		}

		/// <summary>
		/// Devuelve la entrada de la lista en base a su índice.
		/// </summary>
		/// <param name="index">Índice base cero.</param>
		public TObj this [int index]
		{
			get
			{
				return Objetos [index].Objeto;
			}
			set
			{
				var old = Objetos [index];
				Objetos [index] = new Entrada (value, old.Color);
			}
		}

		/// <summary>
		/// Devuelve el número de elementos de la lista.
		/// </summary>
		public int Count
		{
			get
			{
				return Objetos.Count;
			}
		}

		bool ICollection<TObj>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		#endregion

		#region Eventos

		/// <summary>
		/// Ocurre cuando el cursor cambia de posición
		/// </summary>
		public event EventHandler AlMoverCursor;

		#endregion

		/// <summary>
		/// </summary>
		/// <param name="comp">Container</param>
		public Lista (IComponentContainerComponent<IControl> comp)
			: base (comp)
		{
			Objetos = new List<Entrada> ();
			ColorBG = Color.Blue * 0.3f;
			ColorSel = Color.White * 0.5f;
			InterceptarTeclado = true;
		}

	}
}
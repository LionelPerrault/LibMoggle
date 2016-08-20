using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;
using Moggle.Shape;

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
	public class Lista<TObj> : SBC, IList<TObj>, IListaControl<TObj>
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
		/// </summary>
		/// <param name="screen">Pantalla donde está colocado.</param>
		public Lista (IScreen screen)
			: base (screen)
		{
			Objetos = new List<Entrada> ();
			ColorBG = Color.Blue * 0.3f;
			ColorSel = Color.White * 0.5f;
			InterceptarTeclado = true;
		}

		/// <summary>
		/// Dibuja la lista.
		/// </summary>
		/// <param name="gameTime">Duración del tick</param>
		public override void Dibujar (GameTime gameTime)
		{
			// Dibujar el rectángulo
			var bat = Screen.Batch;

			Primitivos.DrawRectangle (
				bat,
				Bounds.GetContainingRectangle (),
				Color.White,
				noTexture);

			// Background
			bat.Draw (noTexture, Bounds.GetContainingRectangle (), ColorBG);

			// TODO: Que no se me salga el texto.
			var currY = Bounds.TopLeft;
			var inic = PrimerVisible;
			var final = Math.Min (Objetos.Count, inic + MaxVisible);
			for (int i = inic; i < final; i++)
			{
				var x = Objetos [i];
				var strTxt = Stringificación (x.Objeto);
				if (i == CursorIndex)
				{
					var rect = Fuente.GetStringRectangle (strTxt, currY);
					bat.Draw (noTexture, rect, ColorSel);
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

		Texture2D noTexture { get; set; }

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
		public Moggle.Shape.Rectangle Bounds { get; set; }

		/// <summary>
		/// Devuelve el menor rectángulo que contiene a este control.
		/// </summary>
		public override IShape GetBounds ()
		{
			return Bounds;
		}

		/// <summary>
		/// Devuelve o establece si este control puede interactuar por sí mismo con el teclado
		/// </summary>
		public bool InterceptarTeclado { get; set; }

		/// <summary>
		/// Cargar contenido
		/// </summary>
		public override void LoadContent ()
		{
			Fuente = Screen.Content.Load<BitmapFont> ("fonts");
			noTexture = Screen.Content.Load<Texture2D> ("Rect");
		}

		/// <summary>
		/// Dispose.
		/// </summary>
		protected override void Dispose ()
		{
			Fuente = null;
			noTexture = null;
			base.Dispose ();
		}

		/// <summary>
		/// Tecla para desplazarse hacia abajo.
		/// </summary>
		/// <seealso cref="InterceptarTeclado"/>
		public Key AbajoKey = Key.Down;

		/// <summary>
		/// Tecla para desplazarse hacia arriba.
		/// </summary>
		/// <seealso cref="InterceptarTeclado"/>
		public Key ArribaKey = Key.Up;

		/// <summary>
		/// Catchs the key.
		/// </summary>
		public override void CatchKey (Key key)
		{
			if (!InterceptarTeclado)
				return;
			if (key == AbajoKey)
				SeleccionaSiguiente ();
			else if (key == ArribaKey)
				SeleccionaAnterior ();
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
			throw new NotImplementedException ();
		}

		void IList<TObj>.Insert (int index, TObj item)
		{
			throw new NotImplementedException ();
		}

		void IList<TObj>.RemoveAt (int index)
		{
			throw new NotImplementedException ();
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
			throw new NotImplementedException ();
		}

		void ICollection<TObj>.CopyTo (TObj [] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Elimina un objeto de la lista
		/// </summary>
		/// <param name="item">Objeto</param>
		public bool Remove (TObj item)
		{
			throw new NotImplementedException ();
		}

		IEnumerator<TObj> IEnumerable<TObj>.GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			throw new NotImplementedException ();
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
	}
}
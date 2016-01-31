using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework.Graphics;
using Moggle.IO;
using OpenTK.Input;

namespace Moggle.Controles.Listas
{
	public class Lista<TObj> : SBC, IList<TObj>, IListaControl<TObj>
	{
		public struct Entrada
		{
			public TObj Objeto;
			public Color Color;

			public Entrada (TObj obj)
				: this (obj, Color.White)
			{
			}

			public Entrada (TObj obj, Color color)
			{
				Objeto = obj;
				Color = color;
			}
		}

		public Lista (IScreen screen)
			: base (screen)
		{
			Objetos = new List<Entrada> ();
			ColorBG = Color.Blue * 0.3f;
			ColorSel = Color.White * 0.5f;
			InterceptarTeclado = true;
		}

		public override void Dibujar (GameTime gameTime)
		{
			// Dibujar el rectángulo
			var bat = Screen.Batch;

			Primitivos.DrawRectangle (bat, Bounds, Color.White, noTexture);

			// Background
			bat.Draw (noTexture, Bounds, ColorBG);

			// TODO: Que no se me salga el texto.
			var currY = Bounds.Location.ToVector2 ();
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
				return Bounds.Height / Fuente.LineHeight;
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

		public List<Entrada> Objetos { get; }

		public Func<TObj, string> Stringificación { get; set; }

		int cursorIndex;

		/// <summary>
		/// El índice del cursor
		/// </summary>
		public int CursorIndex
		{
			get
			{
				return cursorIndex;
			}
			set
			{
				cursorIndex = Math.Max (Math.Min (Objetos.Count - 1, value), 0);
				AlMoverCursor?.Invoke ();
			}
		}

		public TObj ObjetoEnCursor
		{
			get
			{
				if (CursorIndex < Objetos.Count)
					return Objetos [CursorIndex].Objeto;
				throw new ArgumentOutOfRangeException ();
			}
		}

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

		public Rectangle Bounds { get; set; }

		public override Rectangle GetBounds ()
		{
			return Bounds;
		}

		/// <summary>
		/// Devuelve o establece si este control puede interactuar por sí mismo con el teclado
		/// </summary>
		public bool InterceptarTeclado { get; set; }

		public override void LoadContent ()
		{
			Fuente = Screen.Content.Load<BitmapFont> ("fonts");
			noTexture = Screen.Content.Load<Texture2D> ("Rect");
		}

		protected override void Dispose ()
		{
			Fuente = null;
			noTexture = null;
			base.Dispose ();
		}

		public Key AbajoKey = Key.Down;
		public Key ArribaKey = Key.Up;

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

		public void SeleccionaSiguiente ()
		{
			if (++CursorIndex >= PrimerVisible + MaxVisible)
				PrimerVisible++;
		}

		public void SeleccionaAnterior ()
		{
			if (--CursorIndex < PrimerVisible)
				PrimerVisible--;
		}

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

		public void Add (TObj item)
		{
			Add (item, Color.White);
		}

		public void Add (TObj item, Color color)
		{
			Add (new Entrada (item, color));
		}

		public void Add (Entrada entrada)
		{
			Objetos.Add (entrada);
		}

		public void Clear ()
		{
			Objetos.Clear ();
		}

		public bool Contains (TObj item)
		{
			throw new NotImplementedException ();
		}

		void ICollection<TObj>.CopyTo (TObj [] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}

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

		public int Count
		{
			get
			{
				return Objetos.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}



		#endregion

		#region Eventos

		public event Action AlMoverCursor;

		#endregion
	}
}


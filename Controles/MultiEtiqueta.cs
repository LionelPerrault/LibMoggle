using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Screens;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Shapes;
using System.Diagnostics;

namespace Moggle.Controles
{
	/// <summary>
	/// Control que muestra un string de una lista;
	/// cambia con el tiempo
	/// </summary>
	public class MultiEtiqueta : SBCC
	{
		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public MultiEtiqueta (IScreen screen)
			: base (screen)
		{
			Entradas = new List<IEntry> ();
			TiempoEntreCambios = TimeSpan.FromSeconds (1);
			NumEntradasMostrar = 1;
			EspacioEntreLineas = 4;
		}

		#endregion

		#region Entradas

		public void Add (IEntry entry)
		{
			Entradas.Add (entry);
		}

		public IconTextEntry Add (BitmapFont font, string str, Color color)
		{
			var ret = new IconTextEntry (font, str, color);
			Add (ret);
			return ret;
		}

		public IconTextEntry Add (BitmapFont font,
		                          string str,
		                          Color colorTexto,
		                          Texture2D icon,
		                          Color colorIcon)
		{
			var ret = new IconTextEntry (font, icon, str, colorTexto, colorIcon);
			ret.TamañoIcono = new Size (icon.Width, icon.Height);
			Add (ret);
			return ret;
		}

		public void Add (BitmapFont font,
		                 IEnumerable<string> str,
		                 Color color)
		{
			foreach (var item in str)
				Add (font, item, color);
		}

		public void Add (BitmapFont font,
		                 IEnumerable<string> str,
		                 Color colorTexto,
		                 Texture2D icon,
		                 Color colorIcon)
		{
			foreach (var item in str)
				Add (font, item, colorTexto, icon, colorIcon);
		}

		public int Count { get { return Entradas.Count; } }

		public void Clear ()
		{
			Entradas.Clear ();
		}

		public IEntry this [int index]
		{
			get { return Entradas [index]; }
			set
			{
				if (value == null)
					throw new ArgumentNullException ("value");
				Entradas [index] = value;
			}
		}

		public IEnumerable<IEntry> EnumerateEntries ()
		{
			foreach (var x in Entradas)
				yield return x;
		}

		#endregion

		#region Comportamiento

		/// <summary>
		/// La lista de objetos.
		/// </summary>
		protected List<IEntry> Entradas { get; }

		/// <summary>
		/// Posición del control
		/// </summary>
		public Point Pos;

		int numEntradasMostrar;

		/// <summary>
		/// Número de entradas que se muestran;
		/// </summary>
		/// <value>The number entradas mostrar.</value>
		public int NumEntradasMostrar
		{
			get
			{
				return numEntradasMostrar;
			}
			set
			{
				if (value <= 0)
					throw new ArgumentException ("value must be positive.", "value");
				numEntradasMostrar = value;
			}
		}

		/// <summary>
		/// Espacio vertical entre objetos listados.
		/// Este valor es adicional a la altura de la fuente
		/// </summary>
		public int EspacioEntreLineas { get; set; }

		/// <summary>
		/// Devuelve las entradas que son visibles actualmente.
		/// </summary>
		public IEntry[] Actual
		{
			get
			{
				var ret = new IEntry[NumEntradasMostrar];
				for (int i = 0; i < NumEntradasMostrar; i++)
				{
					ret [i] = Entradas [(índiceActualString + i) % Entradas.Count];
				}
				return ret;
			}
		}

		int índiceActualString;

		void StringSiguiente ()
		{
			índiceActualString = (índiceActualString + NumEntradasMostrar) % Entradas.Count;
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		protected override IShapeF GetBounds ()
		{
			var ht = Actual.Aggregate (
				         0,
				         (agg, acc) => agg + acc.Tamaño.Height + EspacioEntreLineas);
			var wd = Actual.Aggregate (
				         0,
				         (agg, acc) => Math.Max (agg, acc.Tamaño.Width));

			return new RectangleF (Pos.ToVector2 (), new SizeF (wd, ht));
		}

		/// <summary>
		/// Se llama cuando ocurre el tick cronometrizado
		/// </summary>
		protected override void OnChrono ()
		{
			StringSiguiente ();
			base.OnChrono ();
		}

		#endregion

		#region Dibujo

		/// <summary>
		/// Dibuja el control
		/// </summary>
		protected override void Draw ()
		{
			if (Count == 0)
				return;
			var bat = Screen.Batch;
			var ht = 0;
			var strs = Actual;
			for (int i = 0; i < NumEntradasMostrar; i++)
			{
				var entry = Actual [i];
				entry.Dibujar (bat, Pos + new Point (0, ht));
				ht += entry.Tamaño.Height + EspacioEntreLineas;
			}
		}

		#endregion

		#region Memoria

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void AddContent ()
		{
		}

		/// <summary>
		/// Vincula el contenido a campos de clase
		/// </summary>
		protected override void InitializeContent ()
		{
		}

		#endregion

		/// <summary>
		/// Una entrada de MultiEtiqueta.
		/// </summary>
		public interface IEntry
		{
			/// <summary>
			/// Dibuja la entrada en una posición dada.
			/// </summary>
			/// <param name="bat">Batch de dibujo.</param>
			/// <param name="pos">Posición de dibujo.</param>
			void Dibujar (SpriteBatch bat, Point pos);

			Size Tamaño { get; }
		}

		/// <summary>
		/// Entrada de <see cref="MultiEtiqueta"/> que muestra texto e icono.
		/// </summary>
		public class IconTextEntry : IEntry
		{
			#region Dibujo

			/// <summary>
			/// Fuente del texto.
			/// </summary>
			public BitmapFont Font;
			/// <summary>
			/// Textura del icono.
			/// </summary>
			public Texture2D TexturaIcon;

			public int SeparaciónIconoTexto = 2;

			/// <summary>
			/// Dibuja la entrada en una posición dada.
			/// </summary>
			/// <param name="bat">Batch de dibujo.</param>
			/// <param name="pos">Posición de dibujo.</param>
			public void Dibujar (SpriteBatch bat, Point pos)
			{
				if (TexturaIcon != null)
				{
					bat.Draw (
						TexturaIcon,
						new Rectangle (pos, TamañoIcono),
						ColorIcon);
					bat.DrawString (
						Font,
						Texto,
						new Vector2 (pos.X + TamañoIcono.Width + SeparaciónIconoTexto, pos.Y),
						ColorTexto);
				}
				else
					bat.DrawString (
						Font,
						Texto,
						new Vector2 (pos.X, pos.Y),
						ColorTexto);
			}

			#endregion

			#region Comportamiento

			/// <summary>
			/// Texto.
			/// </summary>
			public string Texto;

			public Size TamañoIcono { get; set; }

			/// <summary>
			/// Tamaño del icono.
			/// </summary>
			public Size TamañoTexto
			{
				get { return Font.GetSize (Texto); }
			}

			public Size Tamaño
			{
				get
				{
					return TexturaIcon == null ?
						TamañoTexto :
						new Size (
						TamañoTexto.Width + TamañoIcono.Width,
						Math.Max (
							TamañoIcono.Height,
							TamañoTexto.Height));
				}
			}

			/// <summary>
			/// Color del texto.
			/// </summary>
			public Color ColorTexto;
			/// <summary>
			/// Color del icono.
			/// </summary>
			public Color ColorIcon;

			#endregion

			#region ctor

			public IconTextEntry (BitmapFont font, string str, Color colorTexto)
				: this (font, null, str, colorTexto, Color.White)
			{
			}

			/// <summary>
			/// </summary>
			/// <param name="font">Fuente del texto</param>
			/// <param name="texturaIcon">Textura del icono</param>
			/// <param name="str">Texto</param>
			/// <param name="colorTexto">Color del texto.</param>
			/// <param name="colorIcon">Color del icon.</param>
			public IconTextEntry (BitmapFont font,
			                      Texture2D texturaIcon,
			                      string str,
			                      Color colorTexto,
			                      Color colorIcon)
			{
				Font = font;
				TexturaIcon = texturaIcon;
				Texto = str;
				ColorTexto = colorTexto;
				ColorIcon = colorIcon;
			}

			#endregion
		}
	}
}
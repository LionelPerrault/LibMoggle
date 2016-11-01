using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Screens;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Shapes;
using Microsoft.Xna.Framework.Content;

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
		/// <param name="fontName">Fuente del texto a usar.</param>
		public MultiEtiqueta (IScreen screen, string fontName = "fonts")
			: base (screen)
		{
			Mostrables = new List<IEntry> ();
			TiempoEntreCambios = TimeSpan.FromSeconds (1);
			fontString = fontName;
			NumEntradasMostrar = 1;
			EspacioEntreLineas = 4;
		}

		#endregion

		#region Comportamiento

		/// <summary>
		/// La lista de objetos.
		/// </summary>
		public List<IEntry> Mostrables { get; }

		/// <summary>
		/// Posición del control
		/// </summary>
		public Vector2 Pos;

		/// <summary>
		/// Número de entradas que se muestran;
		/// </summary>
		/// <value>The number entradas mostrar.</value>
		public int NumEntradasMostrar { get; set; }

		/// <summary>
		/// Espacio vertical entre objetos listados.
		/// </summary>
		/// <value>The espacio entre lineas.</value>
		public int EspacioEntreLineas { get; set; }

		/// <summary>
		/// Devuelve la entrada actual.
		/// </summary>
		[Obsolete ("Usar Actual")]
		public IEntry EntradaActual
		{
			get
			{
				return índiceActualString < Mostrables.Count ? Mostrables [índiceActualString] : null;
			}
		}

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
					ret [i] = Mostrables [(índiceActualString + i) % Mostrables.Count];
				}
				return ret;
			}
		}

		int índiceActualString;

		void StringSiguiente ()
		{
			índiceActualString = (índiceActualString + NumEntradasMostrar) % Mostrables.Count;
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		protected override IShapeF GetBounds ()
		{
			// Altura
			var ht = NumEntradasMostrar * Font.LineHeight + (NumEntradasMostrar - 1) * EspacioEntreLineas;
			// Grosor
			var wd = Actual.Aggregate (0, (agg, acc) => Math.Max (agg, acc.Largo));

			return new RectangleF (Pos, new SizeF (wd, ht));
		}

		/// <summary>
		/// Se llama cuando ocurre el tick cronometrizado
		/// </summary>
		protected override void OnChrono ()
		{
			StringSiguiente ();
			base.OnChrono ();
		}

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores
		/// </summary>
		public override void Initialize ()
		{
			base.Initialize ();
			Mostrables.Clear ();
		}

		#endregion

		#region Dibujo

		BitmapFont Font;

		string fontString { get; }

		/// <summary>
		/// Dibuja el control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Draw (GameTime gameTime)
		{
			var bat = Screen.Batch;
			var ht = 0f;
			var strs = Actual;
			for (int i = 0; i < NumEntradasMostrar; i++)
			{
				var entry = Actual [i];
				entry.Dibujar (bat, Pos + new Vector2 (0, ht));
				ht += entry.Altura + EspacioEntreLineas;
			}
		}

		#endregion

		#region Memoria

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void LoadContent (ContentManager manager)
		{
			Font = manager.Load<BitmapFont> (fontString);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.MultiEtiqueta"/> object.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			Font = null;
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
			void Dibujar (SpriteBatch bat, Vector2 pos);

			/// <summary>
			/// Del tamaño de la entrada, devuelve la altura.
			/// </summary>
			int Altura { get; }

			/// <summary>
			/// Del tamaño de la entrada, devuelve la longitud.
			/// </summary>
			int Largo { get; }
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

			/// <summary>
			/// Dibuja la entrada en una posición dada.
			/// </summary>
			/// <param name="bat">Batch de dibujo.</param>
			/// <param name="pos">Posición de dibujo.</param>
			public void Dibujar (SpriteBatch bat, Vector2 pos)
			{
				bat.Draw (
					TexturaIcon,
					new Rectangle (pos.ToPoint (), Tamaño),
					ColorIcon);
				bat.DrawString (Font, Str, pos + new Vector2 (Tamaño.X, 0), ColorTexto);
			}

			#endregion

			#region Comportamiento

			/// <summary>
			/// Texto.
			/// </summary>
			public string Str;
			/// <summary>
			/// Tamaño del icono.
			/// </summary>
			public Point Tamaño;
			/// <summary>
			/// Color del texto.
			/// </summary>
			public Color ColorTexto;
			/// <summary>
			/// Color del icono.
			/// </summary>
			public Color ColorIcon;

			/// <summary>
			/// Del tamaño de la entrada, devuelve la altura.
			/// </summary>
			/// <value>The altura.</value>
			public int Altura
			{
				get
				{
					return Math.Max (Font.LineHeight, Tamaño.Y);
				}
			}

			/// <summary>
			/// Del tamaño de la entrada, devuelve la longitud.
			/// </summary>
			/// <value>The largo.</value>
			public int Largo
			{
				get
				{
					return Tamaño.X + Font.GetStringRectangle (Str, Vector2.Zero).Width;
				}
			}

			#endregion

			#region ctor

			/// <summary>
			/// </summary>
			public IconTextEntry ()
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
				Str = str;
				Tamaño = new Point (Font.LineHeight, 24);
				ColorTexto = colorTexto;
				ColorIcon = colorIcon;
			}

			#endregion
		}
	}
}
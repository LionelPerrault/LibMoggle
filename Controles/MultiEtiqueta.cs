using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using Moggle.Screens;
using System.Collections.Generic;
using Moggle.Shape;

namespace Moggle.Controles
{
	/// <summary>
	/// Control que muestra un string de una lista;
	/// cambia con el tiempo
	/// </summary>
	public class MultiEtiqueta : SBCC
	{
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
			/// <summary>
			/// Fuente del texto.
			/// </summary>
			public BitmapFont Font;
			/// <summary>
			/// Textura del icono.
			/// </summary>
			public Texture2D TexturaIcon;

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

			/// <summary>
			/// Dibuja la entrada en una posición dada.
			/// </summary>
			/// <param name="bat">Batch de dibujo.</param>
			/// <param name="pos">Posición de dibujo.</param>
			public void Dibujar (SpriteBatch bat, Vector2 pos)
			{
				bat.Draw (
					TexturaIcon,
					new Microsoft.Xna.Framework.Rectangle (
						pos.ToPoint (),
						Tamaño),
					ColorIcon);
				bat.DrawString (Font, Str, pos + new Vector2 (Tamaño.X, 0), ColorTexto);
			}

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
		}

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

		/// <summary>
		/// La lista de objetos.
		/// </summary>
		public List<IEntry> Mostrables { get; }

		BitmapFont Font;

		string fontString { get; }

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

		/// <summary>
		/// Dibuja el control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Dibujar (GameTime gameTime)
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

		/// <summary>
		/// Cargar contenido
		/// </summary>
		public override void LoadContent ()
		{
			Font = Screen.Content.Load<BitmapFont> (fontString);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.MultiEtiqueta"/> object.
		/// </summary>
		protected override void Dispose ()
		{
			Font = null;
		}

		int índiceActualString;

		void StringSiguiente ()
		{
			índiceActualString = (índiceActualString + NumEntradasMostrar) % Mostrables.Count;
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		public override IShape GetBounds ()
		{
			int ht;
			int wd = 0;
			// Altura
			ht = NumEntradasMostrar * Font.LineHeight + (NumEntradasMostrar - 1) * EspacioEntreLineas;
			// Grosor
			foreach (var entry in Actual)
			{
				wd = Math.Max (wd, entry.Largo);
			}
			return new Shape.Rectangle (Pos.X, Pos.Y, wd, ht);
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
		public override void Inicializar ()
		{
			base.Inicializar ();
			Mostrables.Clear ();
		}
	}
}
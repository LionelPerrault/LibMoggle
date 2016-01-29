using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using Moggle.Screens;
using System.Collections.Generic;

namespace Moggle.Controles
{
	/// <summary>
	/// Control que muestra un string de una lista;
	/// cambia con el tiempo
	/// </summary>
	public class MultiEtiqueta : SBCC
	{
		public interface IEntry
		{
			void Dibujar (SpriteBatch bat, Vector2 pos);

			int Altura { get; }

			int Largo { get; }
		}

		public class IconTextEntry : IEntry
		{
			public BitmapFont Font;
			public Texture2D TexturaIcon;
			public string Str;
			public Point Tamaño;
			public Color ColorTexto;
			public Color ColorIcon;

			public IconTextEntry ()
			{
			}

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


			public void Dibujar (SpriteBatch bat, Vector2 pos)
			{
				bat.Draw (TexturaIcon, new Rectangle (pos.ToPoint (), Tamaño), ColorIcon);
				bat.DrawString (Font, Str, pos + new Vector2 (Tamaño.X, 0), ColorTexto);
			}

			public int Altura
			{
				get
				{
					return Math.Max (Font.LineHeight, Tamaño.Y);
				}
			}

			public int Largo
			{
				get
				{
					return Tamaño.X + Font.GetStringRectangle (Str, Vector2.Zero).Width;
				}
			}
		}

		public MultiEtiqueta (IScreen screen, string fontName = "fonts")
			: base (screen)
		{
			Mostrables = new List<IEntry> ();
			TiempoEntreCambios = TimeSpan.FromSeconds (1);
			fontString = fontName;
			NumEntradasMostrar = 1;
			EspacioEntreLineas = 4;
		}

		public List<IEntry> Mostrables { get; }

		BitmapFont Font;

		string fontString { get; }

		public Vector2 Pos;

		/// <summary>
		/// Número de entradas que se muestran;
		/// </summary>
		/// <value>The number entradas mostrar.</value>
		public int NumEntradasMostrar { get; set; }

		public int EspacioEntreLineas { get; set; }

		[Obsolete ("Usar Actual")]
		public IEntry EntradaActual
		{
			get
			{
				return índiceActualString < Mostrables.Count ? Mostrables [índiceActualString] : null;
			}
		}

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

		public override void LoadContent ()
		{
			Font = Screen.Content.Load<BitmapFont> (fontString);
		}

		protected override void Dispose ()
		{
			Font = null;
		}

		int índiceActualString;

		void StringSiguiente ()
		{
			índiceActualString = (índiceActualString + NumEntradasMostrar) % Mostrables.Count;
		}

		public override Rectangle GetBounds ()
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
			return new Rectangle ((int)Pos.X, (int)Pos.Y, wd, ht);
		}

		protected override void OnChrono ()
		{
			StringSiguiente ();
			base.OnChrono ();
		}

		public override void Inicializar ()
		{
			base.Inicializar ();
			Mostrables.Clear ();
		}
	}
}
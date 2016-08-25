using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Moggle.Controles.Listas;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;

namespace Moggle.Screens
{
	/// <summary>
	/// Pantalla de pedir desde lista
	/// </summary>
	public class MostrarListaScreen<TObj> : DialScreen
	{
		/// <summary>
		/// Lo que devuelve cuando se invoca y termina esta pantalla.
		/// </summary>
		public struct TipoSalida
		{
			/// <summary>
			/// Tipos de salida
			/// </summary>
			public enum EnumTipoSalida
			{
				/// <summary>
				/// Se canceló la pantalla.
				/// </summary>
				Cancelación,
				/// <summary>
				/// Se aceptó y dio una respuesta.
				/// </summary>
				Aceptación
			}

			/// <summary>
			/// </summary>
			/// <param name="tipo">Tipo.</param>
			/// <param name="selección">Selección.</param>
			public TipoSalida (EnumTipoSalida tipo, IEnumerable<TObj> selección)
			{
				Tipo = tipo;
				Selección = selección;
			}

			/// <summary>
			/// Tipo de salida
			/// </summary>
			public EnumTipoSalida Tipo;
			/// <summary>
			/// Selección de objetos.
			/// </summary>
			public IEnumerable<TObj> Selección;
		}

		/// <summary>
		/// La pantalla madre se debe dibujar.
		/// </summary>
		public override bool DibujarBase { get { return true; } }

		/// <summary>
		/// Devuelve o establece el color del fondo de la pantalla.
		/// </summary>
		public Color ColorFondo { get; set; }

		/// <summary>
		/// Devuelve el color de fondo.
		/// </summary>
		public override Color BgColor { get { return ColorFondo; } }

		/// <summary>
		/// Cuando se termina, aquí se almacena la salida del jugador.
		/// </summary>
		public TipoSalida Salida { get; private set; }

		const string fontTexture = "UnitNameFont";

		/// <summary>
		/// </summary>
		/// <param name="game">Game.</param>
		public MostrarListaScreen (Game game)
			: base (game)
		{
			ListaComponente = new Lista<TObj> (this);
			ListaComponente.Bounds = new RectangleF (
				0,
				0,
				GetDisplayMode.Width,
				GetDisplayMode.Height);
			ListaComponente.ColorBG = Color.Blue * 0.4f;
			ListaComponente.Stringificación = x => x.ToString ();
			ListaComponente.InterceptarTeclado = true;
			SelecciónActual = new List<TObj> ();
		}

		/// <summary>
		/// Cuando se presiona una tecla
		/// </summary>
		/// <param name="key">Tecla</param>
		public override bool RecibirSeñal (KeyboardEventArgs key)
		{
			switch (key.Key)
			{
				case Keys.Escape:
					Salida = new TipoSalida (
						TipoSalida.EnumTipoSalida.Cancelación,
						SelecciónActual);
					Salir ();
					return true;
				case Keys.Space:
					var curObj = ObjetoEnCursor;
					if (SelecciónActual.Contains (curObj))
						SelecciónActual.Remove (curObj);
					else
						SelecciónActual.Add (curObj);
					return true;
				case Keys.Enter:
					Salida = new TipoSalida (
						TipoSalida.EnumTipoSalida.Aceptación,
						SelecciónActual);
					Salir ();
					return true;
				default:
					return base.RecibirSeñal (key);
			}
		}

		/// <summary>
		/// Devuelve el objeto seleccionado.
		/// </summary>
		public TObj ObjetoEnCursor { get { return ListaComponente.Objetos [ListaComponente.CursorIndex].Objeto; } }

		/// <summary>
		/// Agrega un objeto a la lista.
		/// </summary>
		/// <param name="t">Objeto</param>
		/// <param name="color">Color</param>
		public void Add (TObj t, Color color)
		{
			ListaComponente.Objetos.Add (new Lista<TObj>.Entrada (t, color));
		}

		/// <summary>
		/// Devuelve el control de la pantalla que es el componente.
		/// </summary>
		public Lista<TObj> ListaComponente { get; }

		/// <summary>
		/// Devuelve la lista de los objetos seleccionados hasta el momento
		/// </summary>
		public List<TObj> SelecciónActual { get; }
	}
}
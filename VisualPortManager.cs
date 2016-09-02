using Microsoft.Xna.Framework;
using System;

namespace Moggle
{
	/// <summary>
	/// Convierte puntos linealmente entre dos sistemas de coordenadas.
	/// </summary>
	[Obsolete]
	public class VisualPortManager
	{
		/// <summary>
		/// Tamaño
		/// </summary>
		public Point Tamaño { get { return ÁreaVisible.Size; } }

		/// <summary>
		/// Devuelve o establece el rectángulo universo.
		/// </summary>
		/// <value>The universo.</value>
		public Rectangle Universo { get; set; }

		/// <summary>
		/// Devuelve o establece el rectángulo de salida.
		/// </summary>
		/// <value>The sistema salida.</value>
		public Rectangle SistemaSalida { get; set; }

		#region Transformaciones elementales

		// T es de la forma AX+B

		Vector2 _A;
		Vector2 _B;

		void RefreshIntValues ()
		{
			_A = new Vector2 (
				(float)SistemaSalida.Width / Universo.Width,
				(float)SistemaSalida.Height / Universo.Height);

			_B = new Vector2 (
				SistemaSalida.Left - _A.X * Universo.Left,
				SistemaSalida.Top - _A.Y * Universo.Top);
		}

		#endregion

		/// <summary>
		/// Devuelve o establece el área visible
		/// </summary>
		public Rectangle ÁreaVisible { get; set; }

		/// <summary>
		/// Convierte de universo a ventana
		/// </summary>
		public Point UniversoAVentana (Point p)
		{
			return UniversoAVentana (p.ToVector2 ()).ToPoint ();
		}

		/// <summary>
		/// Convierte de ventana a universo
		/// </summary>
		public Point VentanaAUniverso (Point p)
		{
			return VentanaAUniverso (p.ToVector2 ()).ToPoint ();
		}

		/// <summary>
		/// Convierte de universo a ventana
		/// </summary>
		public Vector2 UniversoAVentana (Vector2 p)
		{
			return _A * p + _B;
		}

		/// <summary>
		/// Convierte de ventana a universo
		/// </summary>
		public Vector2 VentanaAUniverso (Vector2 p)
		{
			return new Vector2 ((p.X - _B.X) / _A.X, (p.Y - _B.Y) / _A.Y);
		}

		/// <summary>
		/// Convierte de universo a ventana
		/// </summary>
		public Rectangle UniversoAVentana (Rectangle rect)
		{
			return new Rectangle (
				UniversoAVentana (rect.Location),
				(_A * rect.Size.ToVector2 ()).ToPoint ());
		}

		/// <summary>
		/// Convierte de ventana a universo
		/// </summary>
		public Rectangle VentanaAUniverso (Rectangle rect)
		{
			throw new NotImplementedException ();
		}
	}
}
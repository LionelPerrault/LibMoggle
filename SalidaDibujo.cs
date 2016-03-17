using Microsoft.Xna.Framework;
using System;

namespace Moggle
{
	/// <summary>
	/// Convierte puntos linealmente entre dos sistemas de coordenadas.
	/// </summary>
	public class ManejadorVP
	{
		/// <summary>
		/// Tamaño
		/// </summary>
		public Point Tamaño
		{
			get
			{
				return ÁreaVisible.Size;
			}
		}

		public Rectangle Universo { get; set; }

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

		public Rectangle ÁreaVisible { get; set; }

		public Point UniversoAVentana (Point p)
		{
			return UniversoAVentana (p.ToVector2 ()).ToPoint ();
		}

		public Point VentanaAUniverso (Point p)
		{
			return VentanaAUniverso (p.ToVector2 ()).ToPoint ();
		}

		public Vector2 UniversoAVentana (Vector2 p)
		{
			return _A * p + _B;
		}

		public Vector2 VentanaAUniverso (Vector2 p)
		{
			return new Vector2 ((p.X - _B.X) / _A.X, (p.Y - _B.Y) / _A.Y);
		}

		public Rectangle UniversoAVentana (Rectangle rect)
		{
			throw new NotImplementedException ();
		}

		public Rectangle VentanaAUniverso (Rectangle rect)
		{
			throw new NotImplementedException ();
		}
	}
}
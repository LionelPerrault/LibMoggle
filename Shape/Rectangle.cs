using System;
using Microsoft.Xna.Framework;

namespace Moggle.Shape
{
	/// <summary>
	/// Una forma rectangular.
	/// </summary>
	[Obsolete ("Usar IShapeF de MonoGame.Extended")]
	public struct Rectangle : IShape
	{
		float _x;
		float _y;
		float _dx;
		float _dy;

		#region Casts

		/// <param name="rect">Rectángulo</param>
		public static explicit operator Microsoft.Xna.Framework.Rectangle (Rectangle rect)
		{
			return new Microsoft.Xna.Framework.Rectangle (
				rect.TopLeft.ToPoint (),
				rect.Size.ToPoint ());
		}

		/// <param name="rect">Rectángulo</param>
		public static explicit operator Rectangle (Microsoft.Xna.Framework.Rectangle rect)
		{
			return new Rectangle (rect.Location, rect.Size);
		}

		#endregion

		#region Props

		/// <summary>
		/// Devuelve o establece las coordenadas superior izquierda.
		/// </summary>
		public Vector2 TopLeft
		{
			get
			{
				return new Vector2 (_x, _y);
			}
			set
			{
				_x = value.X;
				_y = value.Y;
			}
		}

		/// <summary>
		/// Devuelve o establece las coordenadas inferior derecha.
		/// </summary>
		public Vector2 BottomRight
		{
			get
			{
				return TopLeft + Size;
			}
		}

		/// <summary>
		/// Devuelve el tamaño.
		/// </summary>
		public Vector2 Size
		{
			get
			{
				return new Vector2 (_dx, _dy);
			}
			set
			{
				_dx = value.X;
				_dy = value.Y;
			}
		}

		/// <summary>
		/// Devuelve la altura.
		/// </summary>
		public float Height { get { return _dy; } }

		/// <summary>
		/// Devuelve el grosor.
		/// </summary>
		public float Witdh { get { return _dx; } }

		/// <summary>
		/// Devuelve el punto más izquierdo
		/// </summary>
		public float Left { get { return _x; } }

		/// <summary>
		/// Devuelve el punto más derecho
		/// </summary>
		public float Right { get { return _x + _dx; } }

		/// <summary>
		/// Devuelve el punto más superior.
		/// </summary>
		public float Top { get { return _y; } }

		/// <summary>
		/// Devuelve el punto más inferior.
		/// </summary>
		public float Bottom { get { return _y + _dy; } }

		#endregion

		#region IShape

		/// <summary>
		/// Devuelve una forma que es el resultado de una translación
		/// </summary>
		/// <returns>The by.</returns>
		/// <param name="v">V.</param>
		public IShape MoveBy (Vector2 v)
		{
			var ret = new Rectangle ();
			ret.TopLeft = TopLeft + v;
			ret.Size = Size;
			return ret;
		}

		/// <summary>
		/// Devuelve una forma que es el resultado de una reescalación
		/// </summary>
		/// <param name="factor">Factor.</param>
		public IShape Scale (float factor)
		{
			var ret = new Rectangle ();
			ret.TopLeft = TopLeft;
			ret.Size = Size * factor;
			return ret;
		}

		/// <summary>
		/// Revisa si esta forma contiene un punto dado.
		/// </summary>
		/// <param name="p">Punto</param>
		public bool Contains (Point p)
		{
			return _x <= p.X &&
			p.X <= _x + _dx &&
			_y <= p.Y &&
			p.Y <= _y + _dy;
		}

		/// <summary>
		/// Devuelve el rectángulo más pequeño que lo contiene
		/// </summary>
		/// <returns>The containing rectangle.</returns>
		public Microsoft.Xna.Framework.Rectangle GetContainingRectangle ()
		{
			return new Microsoft.Xna.Framework.Rectangle (
				(int)_x,
				(int)_y,
				(int)_dx,
				(int)_dy);
		}

		#endregion

		#region ctors

		/// <summary>
		/// Initializes a new instance of the <see cref="Rectangle"/> struct.
		/// </summary>
		/// <param name="pos">Posición</param>
		/// <param name="size">Tamaño</param>
		public Rectangle (Point pos, Vector2 size)
		{
			_x = pos.X;
			_y = pos.Y;
			_dx = size.X;
			_dy = size.Y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Rectangle"/> struct.
		/// </summary>
		/// <param name="pos">Posición</param>
		/// <param name="size">tamaño</param>
		public Rectangle (Point pos, Point size)
		{
			_x = pos.X;
			_y = pos.Y;
			_dx = size.X;
			_dy = size.Y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Rectangle"/> struct.
		/// </summary>
		/// <param name="left">Left.</param>
		/// <param name="top">Top.</param>
		/// <param name="witdh">Witdh.</param>
		/// <param name="height">Height.</param>
		public Rectangle (float left, float top, float witdh, float height)
		{
			_x = left;
			_y = top;
			_dx = witdh;
			_dy = height;
		}

		#endregion
	}
}
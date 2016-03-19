using Microsoft.Xna.Framework;

namespace Moggle.Shape
{
	public struct Rectangle : IShape
	{
		float _x;
		float _y;
		float _dx;
		float _dy;

		#region ctors

		public Rectangle (Point pos, Vector2 size)
		{
			_x = pos.X;
			_y = pos.Y;
			_dx = size.X;
			_dy = size.Y;
		}

		public Rectangle (Point pos, Point size)
		{
			_x = pos.X;
			_y = pos.Y;
			_dx = size.X;
			_dy = size.Y;
		}

		public Rectangle (float left, float top, float witdh, float height)
		{
			_x = left;
			_y = top;
			_dx = witdh;
			_dy = height;
		}

		#endregion

		#region Casts

		public static implicit operator Microsoft.Xna.Framework.Rectangle (Rectangle rect)
		{
			return new Microsoft.Xna.Framework.Rectangle (
				rect.TopLeft.ToPoint (),
				rect.Size.ToPoint ());
		}

		public static implicit operator Rectangle (Microsoft.Xna.Framework.Rectangle rect)
		{
			return new Rectangle (rect.Location, rect.Size);
		}

		#endregion

		#region Props

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

		public Vector2 BottomRight
		{
			get
			{
				return TopLeft + Size;
			}
		}

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

		public float Height
		{
			get
			{
				return _dy;
			}
		}

		public float Witdh
		{
			get
			{
				return _dx;
			}
		}

		public float Left
		{
			get
			{
				return _x;
			}
		}

		public float Right
		{
			get
			{
				return _x + _dx;
			}
		}

		public float Top
		{
			get
			{
				return _y;
			}
		}

		public float Bottom
		{
			get
			{
				return _y + _dy;
			}
		}

		#endregion

		#region IShape

		public IShape MoveBy (Vector2 v)
		{
			var ret = new Rectangle ();
			ret.TopLeft = TopLeft + v;
			ret.Size = Size;
			return ret;
		}

		public IShape Scale (float factor)
		{
			var ret = new Rectangle ();
			ret.TopLeft = TopLeft;
			ret.Size = Size * factor;
			return ret;
		}

		public bool Contains (Point p)
		{
			return _x <= p.X &&
			p.X <= _x + _dx &&
			_y <= p.Y &&
			p.Y <= _y + _dy;
		}

		public Rectangle GetContainingRectangle ()
		{
			return new Microsoft.Xna.Framework.Rectangle (
				(int)_x,
				(int)_y,
				(int)_dx,
				(int)_dy);
		}

		#endregion
	}
}
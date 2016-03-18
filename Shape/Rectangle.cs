using Microsoft.Xna.Framework;

namespace Moggle.Shape
{
	public struct Rectangle : IShape
	{
		float _x;
		float _y;
		float _dx;
		float _dy;

		public Rectangle ()
		{
		}

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

		public IShape MoveBy (Vector2 v)
		{
			var ret = new Rectangle ();
			ret.TopLeft = TopLeft + v;
			ret.Size = Size;
			return ret;
		}

		public IShape Scale (double factor)
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
	}
}
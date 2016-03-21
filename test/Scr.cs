using Moggle.Screens;
using Microsoft.Xna.Framework;

namespace Test
{
	public class Scr : Screen
	{
		public Scr (Moggle.Game game)
			: base (game)
		{
		}



		public override Microsoft.Xna.Framework.Color BgColor
		{
			get
			{
				return Color.Blue;
			}
		}
	}
}
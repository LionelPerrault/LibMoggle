using Microsoft.Xna.Framework;
using Moggle.Controles;
using Moggle.Screens;

namespace Test
{
	public class DialScr : Screen
	{
		public override Color? BgColor
		{
			get
			{
				return Color.Red;
			}
		}

		public DialScr (Moggle.Game juego)
			: base (juego)
		{
			var bt = new Botón (this)
			{
				Textura = "outline",
				Color = Color.Green,
				Bounds = new Rectangle (145, 90, 50, 50),
			};

			bt.AlClick += (sender, e) => Juego.Exit ();

			Components.Add (bt);
		}
	}
}
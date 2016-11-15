using Microsoft.Xna.Framework;
using Moggle.Controles;
using Moggle.Screens;
using MonoGame.Extended.Shapes;

namespace Test
{
	public class DialScr : Screen
	{
		public DialScr (Moggle.Game juego)
			: base (juego)
		{
			var bt = new Botón (this)
			{
				Textura = "outline",
				Color = Color.Green,
				Bounds = new RectangleF (400, 400, 50, 50),
			};

			bt.AlClick += delegate
			{
				Juego.Exit ();
			};

			Components.Add (bt);
		}
	}
}
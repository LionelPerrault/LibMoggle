using Microsoft.Xna.Framework;
using Moggle.Controles;
using Moggle.Screens;
using MonoGame.Extended.Shapes;

namespace Test
{
	public class DialScr : DialScreen
	{
		public override Color BgColor
		{
			get
			{
				return Color.Pink;
			}
		}

		public override bool DibujarBase
		{
			get
			{
				return true;
			}
		}

		public DialScr (Moggle.Game juego, IScreen baseScreen)
			: base (juego, baseScreen)
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
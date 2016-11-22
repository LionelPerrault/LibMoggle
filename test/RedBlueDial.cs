using Moggle.Screens;
using Moggle.Controles;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Shapes;
using Moggle.Screens.Dials;
using Moggle.Textures;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;

namespace Test
{
	public class RedBlueDial : Screen, IRespScreen
	{
		public event System.EventHandler<object> HayRespuesta;

		public override void AddAllContent ()
		{
			base.AddAllContent ();
			var tx = new SimpleTextures (Device);
			Content.AddContent ("pixel", tx.SolidTexture (new Size (1, 1), Color.White));
		}

		public override bool RecibirSeñal (System.Tuple<MonoGame.Extended.InputListeners.KeyboardEventArgs, ScreenThread> data)
		{
			if (data.Item1.Key == Keys.D1)
			{
				HayRespuesta?.Invoke (this, 1);
				return true;
			}
			if (data.Item1.Key == Keys.D2)
			{
				HayRespuesta?.Invoke (this, 2);
				return true;
			}
			return base.RecibirSeñal (data);
		}

		public readonly int Id;

		public RedBlueDial (Moggle.Game game, int id)
			: base (game)
		{
			Id = id;
			var btRed = new Botón (this)
			{
				Textura = "pixel",
				Color = Color.Red,
				Bounds = new RectangleF (50, 50, 200, 100)
			};
			var btBlue = new Botón (this)
			{
				Textura = "pixel",
				Color = Color.Green,
				Bounds = new RectangleF (250, 50, 200, 100)
			};

			btRed.AlClick += delegate
			{
				HayRespuesta?.Invoke (this, 0);
			};

			btBlue.AlClick += delegate
			{
				HayRespuesta?.Invoke (this, 1);
			};

			AddComponent (btRed);
			AddComponent (btBlue);
		}
	}
}
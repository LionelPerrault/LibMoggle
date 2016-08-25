using System.Diagnostics;
using Microsoft.Xna.Framework;
using Moggle.Controles;
using Moggle.Screens;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;

namespace Test
{
	public class Scr : Screen
	{
		public Scr (Moggle.Game game)
			: base (game)
		{
			bt = new Botón (this, new CircleF (new Vector2 (200, 200), 100));
			bt.Color = Color.Green;
			bt.Textura = "cont//void";
		}

		readonly Botón bt;

		public override bool RecibirSeñal (KeyboardEventArgs key)
		{
			if (key.Key == Microsoft.Xna.Framework.Input.Keys.Escape)
			{
				Juego.Exit ();
				return true;
			}
			Debug.WriteLine (string.Format (
				"{0}:{1}:{2}",
				key.Character,
				key.Key,
				key.Modifiers));
			return base.RecibirSeñal (key);
		}

		public override Color BgColor
		{
			get
			{
				return Color.Blue;
			}
		}

		public override void Initialize ()
		{
			Components.Add (bt);
			base.Initialize ();
			bt.AlClick += Bt_AlClick;
		}

		protected void Bt_AlClick (object sender, MouseEventArgs e)
		{
			Debug.WriteLine (sender);
			Debug.WriteLine (e.Button);
		}

	}
}
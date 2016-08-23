using Moggle.Screens;
using Microsoft.Xna.Framework;
using Moggle.Controles;
using Moggle.Shape;
using System.Diagnostics;
using MonoGame.Extended.InputListeners;

namespace Test
{
	public class Scr : Screen
	{
		public Scr (Moggle.Game game)
			: base (game)
		{
			bt = new Botón (this, new Circle (100, new Vector2 (200, 200)));
			bt.Color = Color.Green;
			bt.Textura = "cont//void";
		}

		readonly Botón bt;

		public override void TeclaPresionada (KeyboardEventArgs key)
		{
			base.TeclaPresionada (key);
			Debug.WriteLine (string.Format (
				"{0}:{1}:{2}",
				key.Character,
				key.Key,
				key.Modifiers));
		}

		public override Color BgColor
		{
			get
			{
				return Color.Blue;
			}
		}

		public override void LoadContent ()
		{
			bt.LoadContent ();
			base.LoadContent ();
		}

		public override void Inicializar ()
		{
			bt.Include ();
			base.Inicializar ();
		}

	}
}
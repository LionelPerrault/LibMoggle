using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Moggle.Controles;
using Moggle.Screens;
using Moggle.Screens.Dials;
using MonoGame.Extended.Input.InputListeners;
using System;

namespace Test
{
	public class RedBlueDial : Screen, IRespScreen
	{
		public event EventHandler<IntEventArgs> HayRespuesta;

		public override bool RecibirSeñal (System.Tuple<KeyboardEventArgs, ScreenThread> data)
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
				Bounds = new Rectangle (50, 50, 200, 100)
			};
			var btBlue = new Botón (this)
			{
				Textura = "pixel",
				Color = Color.Green,
				Bounds = new Rectangle (250, 50, 200, 100)
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
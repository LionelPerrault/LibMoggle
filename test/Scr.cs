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
			bt = new Botón (this, new RectangleF (100, 100, 50, 50));
			bt.Color = Color.Green;
			bt.Textura = "cont//void";

			var ct = new ContenedorBotón (this)
			{
				Columnas = 2,
				Filas = 2,
				TextureFondo = "cont//void",
				Márgenes = new ContenedorBotón.MargenType
				{
					Top = 3,
					Left = 3,
					Right = 3,
					Bot = 3
				},
				BgColor = Color.Gray * 0.3f
			};
			const int numBot = 3;
			var bts = new Botón[numBot];
			for (int i = 0; i < numBot; i++)
			{
				bts [i] = ct.Add ();
				bts [i].Color = Color.OrangeRed;
				bts [i].Textura = "cont//void";
				bts [i].AlClick += (sender, e) => buttonClicked (e, i);
			}
			ct.AlActivarBotón += Ct_AlActivarBotón;

		}

		void Ct_AlActivarBotón (object sender,
		                        ContenedorBotón.ContenedorBotónIndexEventArgs e)
		{
			System.Console.WriteLine ("botón [{0}] click: {1}", e.Index, e.Mouse.Button);
		}

		static void buttonClicked (MouseEventArgs e, int index)
		{
			System.Console.WriteLine ("botón [{0}] click: {1}", e, index);
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
			//Components.Add (bt);
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
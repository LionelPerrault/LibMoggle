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

			var ct = new ContenedorSelección<FlyingSprite> (this)
			{
				GridSize = new MonoGame.Extended.Size (2, 2),
				TextureFondoName = "cont//void",
				Márgenes = new MargenType
				{
					Top = 3,
					Left = 3,
					Right = 3,
					Bot = 3
				},
				BgColor = Color.Gray * 0.3f,
				TamañoBotón = new MonoGame.Extended.Size (12, 12),
				Posición = new Point (5, 5)
			};
			AddComponent (ct);
			const int numBot = 3;
			var bts = new FlyingSprite [numBot];
			for (int i = 0; i < numBot; i++)
			{
				bts [i] = new FlyingSprite
				{
					Color = Color.PaleVioletRed * 0.8f,
					TextureName = "cont//void"
				};

				bts [i].LoadContent (Content);

				ct.Add (bts [i]);
				//bts [i].AlClick += (sender, e) => buttonClicked (e, i);
			}
			//ct.AlActivarBotón += Ct_AlActivarBotón;

			/* 
			 * //TODO: Agregar un font.
			var tx = new EntradaTexto (this);
			tx.Pos = new Vector2 (50, 50);
			tx.BgTexture = "cont//void";
			tx.FontTexture = "fonts";
			*/

			StrListen = new KeyStringListener (Juego.KeyListener);
		}

		void buttonClicked (MouseEventArgs e, int index)
		{
			System.Console.WriteLine ("botón [{0}] click: {1}", e, index);
			System.Console.WriteLine ("stringActual = {0}", StrListen.CurrentString);
		}

		readonly Botón bt;
		readonly KeyStringListener StrListen;

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
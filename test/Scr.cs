using System.Diagnostics;
using Microsoft.Xna.Framework;
using Moggle.Controles;
using Moggle.Screens;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;
using Microsoft.Xna.Framework.Graphics;

namespace Test
{
	public class Scr : Screen
	{
		public Texture2D Solid
		{
			get
			{
				var ret = new Texture2D (Juego.GraphicsDevice, 2, 2);
				var _data = new []
				{
					Color.White,
					Color.Black,
					Color.Black,
					Color.White
				};
				ret.SetData<Color> (_data);
				return ret;
			}
		}

		public Scr (Moggle.Game game)
			: base (game)
		{
			Content.AddContent ("solid", Solid);

			bt = new Botón (this, new RectangleF (100, 100, 50, 50));
			bt.Color = Color.Green;
			bt.Textura = "solid";

			var ct = new ContenedorSelección<FlyingSprite> (this)
			{
				GridSize = new MonoGame.Extended.Size (4, 4),
				TextureFondoName = "cont//void",
				MargenExterno = new MargenType
				{
					Top = 3,
					Left = 3,
					Right = 3,
					Bot = 3
				},
				MargenInterno = new MargenType
				{
					Top = 1,
					Left = 1,
					Right = 1,
					Bot = 2
				},
				BgColor = Color.Gray * 0.3f,
				TamañoBotón = new MonoGame.Extended.Size (12, 12),
				Posición = new Point (5, 5),
				SelectionEnabled = true
			};
			AddComponent (ct);
			const int numBot = 13;
			var bts = new FlyingSprite [numBot];
			for (int i = 0; i < numBot; i++)
			{
				bts [i] = new FlyingSprite
				{
					Color = Color.PaleVioletRed * 0.8f,
					TextureName = "cont/void"
				};
				//bts [i].AddContent (Content);
				//AddComponent (bts[i]);

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
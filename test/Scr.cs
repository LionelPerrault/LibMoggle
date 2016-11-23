using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Controles;
using Moggle.Screens;
using Moggle.Textures;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;
using Moggle.Screens.Dials;
using System.Text;

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

		SimpleTextures textures;

		void buildTextures ()
		{
			Content.AddContent (
				"outline",
				textures.OutlineTexture (
					new MonoGame.Extended.Size (15, 10),
					Color.White,
					Color.Black));

			Content.AddContent (
				"solid",
				textures.SolidTexture (
					new MonoGame.Extended.Size (1, 1),
					Color.White));


			#region Alternates Test
			var altColor = new [] { Color.White, Color.Black };
			Content.AddContent (
				"alt_1",
				textures.AlternatingTexture (
					new MonoGame.Extended.Size (1, 1),
					altColor));
			Content.AddContent (
				"alt_2",
				textures.AlternatingTexture (
					new MonoGame.Extended.Size (5, 5),
					altColor));
			Content.AddContent (
				"alt_3",
				textures.AlternatingTexture (
					new MonoGame.Extended.Size (50, 20),
					altColor));
			Content.AddContent (
				"alt_4",
				textures.AlternatingTexture (
					new MonoGame.Extended.Size (50, 100),
					altColor));
			
			#endregion
			#region Solid
			Content.AddContent (
				"sol_1",
				textures.AlternatingTexture (
					new MonoGame.Extended.Size (1, 1),
					altColor));
			Content.AddContent (
				"sol_2",
				textures.SolidTexture (
					new MonoGame.Extended.Size (5, 5),
					Color.White));
			Content.AddContent (
				"sol_3",
				textures.SolidTexture (
					new MonoGame.Extended.Size (5, 100),
					Color.White));
			Content.AddContent (
				"sol_4",
				textures.SolidTexture (
					new MonoGame.Extended.Size (100, 100),
					Color.White));

			#endregion
			#region Outline
			Content.AddContent (
				"out_1",
				textures.OutlineTexture (
					new MonoGame.Extended.Size (1, 1),
					Color.White));
			Content.AddContent (
				"out_2",
				textures.OutlineTexture (
					new MonoGame.Extended.Size (3, 3),
					Color.White));
			Content.AddContent (
				"out_3",
				textures.OutlineTexture (
					new MonoGame.Extended.Size (100, 3),
					Color.White));
			Content.AddContent (
				"out_4",
				textures.OutlineTexture (
					new MonoGame.Extended.Size (6, 6),
					Color.White));
			Content.AddContent (
				"out_5",
				textures.OutlineTexture (
					new MonoGame.Extended.Size (20, 20), // El tamaño de la imagen de muestra
					Color.White));
			#endregion
		}

		public Scr (Moggle.Game game)
			: base (game)
		{
			textures = new SimpleTextures (game.GraphicsDevice);

			buildTextures ();

			bt = new Botón (this, new RectangleF (100, 100, 50, 50));
			bt.Color = Color.Green;
			bt.Textura = "outline";

			bt = new Botón (this, new RectangleF (155, 100, 50, 50));
			bt.Color = Color.Green;
			bt.Textura = "outline";
			bt.AlClick += delegate
			{
				var newScr = new DialScr (Juego);
				newScr.Execute (ScreenThread.ScreenStackOptions.Dialog);
			};

			var et = new EtiquetaMultiLínea (this)
			{
				Texto = "Esto es una linea larga, debe que quedar cortada en algún espacio intermedio.",
				TopLeft = new Point (300, 250),
				UseFont = "cont//font",
				MaxWidth = 100,
				TextColor = Color.White,
				BackgroundColor = Color.Red
			};
			AddComponent (et);
				
			MouseObserver.RatónEncima += (sender, e) => Debug.WriteLine (
				"Mouse ahora sobre {0}",
				e.ObservedObject);
			MouseObserver.RatónSeFue += (sender, e) => Debug.WriteLine (
				"Mouse estuvo sobre {0} por {1}",
				e.ObservedObject,
				e.TimeOnObject);




			var ct = new ContenedorSelección<FlyingSprite> (this)
			{
				GridSize = new MonoGame.Extended.Size (4, 4),
				TextureFondoName = "solid",
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
				bts [i] = new FlyingSprite (Content)
				{
					Color = Color.PaleVioletRed * 0.8f,
					TextureName = "solid"
				};

				ct.Add (bts [i]);
			}

			MouseObserver.ObserveObject (ct);

			StrListen = new KeyStringListener (Juego.KeyListener);

			var contImg = new ContenedorImage (this)
			{
				MargenExterno = new MargenType
				{
					Left = 3,
					Right = 3,
					Top = 3,
					Bot = 3
				},
				MargenInterno = new MargenType
				{
					Left = 2,
					Right = 2,
					Top = 2,
					Bot = 2
				},
				TamañoBotón = new MonoGame.Extended.Size (20, 20),
				BgColor = Color.Black,
				Posición = new Point (400, 5),
				TextureFondoName = "solid",
				GridSize = new MonoGame.Extended.Size (4, 4),
				TipoOrden = Contenedor<FlyingSprite>.TipoOrdenEnum.FilaPrimero
			};
			contImg.Add ("sol_1", Color.White);
			contImg.Add ("sol_2", Color.White);
			contImg.Add ("sol_3", Color.White);
			contImg.Add ("sol_4", Color.White);
			contImg.Add ("alt_1", Color.White);
			contImg.Add ("alt_2", Color.White);
			contImg.Add ("alt_3", Color.White);
			contImg.Add ("alt_4", Color.White);
			contImg.Add ("out_1", Color.White);
			contImg.Add ("out_2", Color.White);
			contImg.Add ("out_3", Color.White);
			contImg.Add ("out_4", Color.White);
			contImg.Add ("out_5", Color.White);
			AddComponent (contImg);
		}

		void buttonClicked (MouseEventArgs e, int index)
		{
			Console.WriteLine ("botón [{0}] click: {1}", e, index);
			Console.WriteLine ("stringActual = {0}", StrListen.CurrentString);
		}

		readonly Botón bt;
		readonly KeyStringListener StrListen;

		public override bool RecibirSeñal (Tuple<KeyboardEventArgs, ScreenThread> data)
		{
			var key = data.Item1;
			if (key.Key == Microsoft.Xna.Framework.Input.Keys.Escape)
			{
				Juego.Exit ();
				return true;
			}
			else if (key.Key == Microsoft.Xna.Framework.Input.Keys.T)
			{
				var ser = new ScreenDialSerial ();

				ser.AddRequest (new RedBlueDial (Juego, 2));
				ser.AddRequest (new RedBlueDial (Juego, 1));
				ser.AddRequest (new RedBlueDial (Juego, 0));

				ser.Executar (Juego.ScreenManager.ActiveThread);
				ser.HayRespuesta += delegate(object sender, object [] e)
				{
					Debug.WriteLine (e);
				};
			}
			Debug.WriteLine (string.Format (
				"{0}:{1}:{2}",
				key.Character,
				key.Key,
				key.Modifiers));
			return base.RecibirSeñal (data);
		}

		public override Color? BgColor
		{
			get
			{
				return Color.Blue;
			}
		}

		protected override void DoInitialization ()
		{
			base.DoInitialization ();
			bt.AlClick += Bt_AlClick;
		}

		protected void Bt_AlClick (object sender, MouseEventArgs e)
		{
			Debug.WriteLine (sender);
			Debug.WriteLine (e.Button);
		}
	}
}
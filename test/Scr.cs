using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Controles;
using Moggle.Screens;
using Moggle.Textures;
using Moggle.Screens.Dials;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended;

namespace Test
{
	public class Scr : Screen
	{
		public Texture2D Solid {
			get {
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

		Texture2D [] solids = new Texture2D [4];
		Texture2D [] alts = new Texture2D [4];
		Texture2D [] outlines = new Texture2D [5];

		void buildTextures ()
		{
			#region Alternates Test
			var altColor = new [] { Color.White, Color.Black };
			alts [0] =
				textures.AlternatingTexture (
				new Size (1, 1),
				altColor);
			alts [1] =
				textures.AlternatingTexture (
				new Size (5, 5),
				altColor);
			alts [2] =
				textures.AlternatingTexture (
				new Size (50, 20),
				altColor);
			alts [3] =
				textures.AlternatingTexture (
				new Size (50, 100),
				altColor);

			#endregion
			#region Solid
			solids [0] =
				textures.AlternatingTexture (
				new Size (1, 1),
				altColor);
			solids [1] =
				textures.SolidTexture (
				new Size (5, 5),
				Color.White);
			solids [2] =
				textures.SolidTexture (
				new Size (5, 100),
				Color.White);
			solids [3] =
				textures.SolidTexture (
				new Size (100, 100),
				Color.White);

			#endregion
			#region Outline
			outlines [0] =
				textures.OutlineTexture (
				new Size (1, 1),
				Color.White);
			outlines [1] =
				textures.OutlineTexture (
				new Size (3, 3),
				Color.White);
			outlines [2] =
				textures.OutlineTexture (
				new Size (100, 3),
				Color.White);
			outlines [3] =
				textures.OutlineTexture (
				new Size (6, 6),
				Color.White);
			outlines [4] =
				textures.OutlineTexture (
				new Size (20, 20),
				Color.White);
			#endregion
		}

		public Scr (Moggle.Game game)
			: base (game)
		{
			textures = new SimpleTextures (game.GraphicsDevice);

			var met = new MultiEtiqueta (this);
			met.Add (
				Content.Load<BitmapFont> ("cont//font"),
				"hi",
				Color.Green);
			met.Add (
				Content.Load<BitmapFont> ("cont//font"),
				"hi II",
				Color.Green);
			met.Add (
				Content.Load<BitmapFont> ("cont//font"),
				"hi III", Color.Green, textures.OutlineTexture (
				new Size (10, 10), Color.White, Color.Red), Color.White
			);
			met.NumEntradasMostrar = 2;
			met.EspacioEntreLineas = 3;
			met.Pos = new Point (200, 200);

			buildTextures ();

			bt = new Botón (this, new RectangleF (100, 100, 50, 50), outlines [0]);
			bt.Color = Color.Green;

			bt = new Botón (
				this,
				new RectangleF (155, 100, 50, 50),
				outlines [0]);
			bt.Color = Color.Green;
			bt.AlClick += delegate {
				var newScr = new DialScr (Juego);
				newScr.Execute (ScreenThread.ScreenStackOptions.Dialog);
			};

			var et = new EtiquetaMultiLínea (this) {
				Texto = "Esto es una linea larga, debe que quedar cortada en algún espacio intermedio.",
				TopLeft = new Point (300, 250),
				UseFont = "cont//font",
				MaxWidth = 100,
				TextColor = Color.White,
				BackgroundColor = Color.Red
			};
			AddComponent (et);

			var singleEt = new Etiqueta (this) {
				Texto = () => "Etiqueta normal",
				UseFont = "cont//font",
				Color = Color.White,
				Posición = new Point (410, 250)
			};
			AddComponent (singleEt);

			MouseObserver.RatónEncima += (sender, e) => Debug.WriteLine (
				"Mouse ahora sobre {0}",
				e.ObservedObject);
			MouseObserver.RatónSeFue += (sender, e) => Debug.WriteLine (
				"Mouse estuvo sobre {0} por {1}",
				e.ObservedObject,
				e.TimeOnObject);

			var ct = new ContenedorSelección<FlyingSprite> (this, solids [0]) {
				GridSize = new Size (4, 4),
				MargenExterno = new MargenType {
					Top = 3,
					Left = 3,
					Right = 3,
					Bot = 3
				},
				MargenInterno = new MargenType {
					Top = 1,
					Left = 1,
					Right = 1,
					Bot = 2
				},
				BgColor = Color.Gray * 0.3f,
				TamañoBotón = new Size (12, 12),
				Posición = new Point (5, 5),
				SelectionEnabled = true
			};
			AddComponent (ct);
			const int numBot = 13;
			var bts = new FlyingSprite [numBot];
			for (int i = 0; i < numBot; i++) {
				bts [i] = new FlyingSprite (solids [0]) { Color = Color.PaleVioletRed * 0.8f };

				ct.Add (bts [i]);
			}

			MouseObserver.ObserveObject (ct);

			StrListen = new KeyStringListener (Juego.KeyListener);

			var contImg = new ContenedorImage (this, solids [0]) {
				MargenExterno = new MargenType {
					Left = 3,
					Right = 3,
					Top = 3,
					Bot = 3
				},
				MargenInterno = new MargenType {
					Left = 2,
					Right = 2,
					Top = 2,
					Bot = 2
				},
				TamañoBotón = new Size (20, 20),
				BgColor = Color.Black,
				Posición = new Point (400, 5),
				GridSize = new Size (4, 4),
				TipoOrden = TipoOrdenEnum.FilaPrimero
			};
			for (int i = 0; i < 4; i++)
				contImg.Add (solids [i], Color.White);
			for (int i = 0; i < 4; i++)
				contImg.Add (alts [i], Color.White);
			for (int i = 0; i < 5; i++)
				contImg.Add (outlines [i], Color.White);
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
			if (key.Key == Microsoft.Xna.Framework.Input.Keys.Escape) {
				Juego.Exit ();
				return true;
			} else if (key.Key == Microsoft.Xna.Framework.Input.Keys.T) {
				var ser = new ScreenDialSerial ();

				ser.AddRequest (new RedBlueDial (Juego, 2));
				ser.AddRequest (new RedBlueDial (Juego, 1));
				ser.AddRequest (new RedBlueDial (Juego, 0));

				ser.Executar (Juego.ScreenManager.ActiveThread);
				ser.HayRespuesta += delegate (object sender, object [] e) {
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

		public override Color? BgColor {
			get {
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
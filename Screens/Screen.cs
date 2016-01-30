using Moggle.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Moggle.Controles;

namespace Moggle.Screens
{
	public abstract class Screen : IScreen
	{
		public Game Juego { get; }

		public ListaControl Controles { get; }

		protected Screen (Game game)
			: this ()
		{
			Juego = game;
		}

		Screen ()
		{
			Controles = new ListaControl ();
		}

		protected virtual void TeclaPresionada (OpenTK.Input.Key key)
		{
			foreach (var x in Controles)
			{
				x.CatchKey (key);
			}
		}

		public bool Escuchando
		{
			set
			{
				if (value)
					InputManager.AlSerActivado += TeclaPresionada;
				else
					InputManager.AlSerActivado -= TeclaPresionada;
			}
		}

		public abstract Color BgColor { get; }

		public virtual void Inicializar ()
		{
			foreach (var x in Controles)
			{
				x.Inicializar ();
			}
		}

		public virtual void Dibujar (GameTime gameTime)
		{
			//base.Draw (gameTime);

			//var Batch = GetNewBatch ();
			Batch.Begin ();
			foreach (var x in Controles)
			{
				x.Dibujar (gameTime);
			}
			Batch.End ();
		}

		public virtual void LoadContent ()
		{
			Batch = new SpriteBatch (Juego.GraphicsDevice);
			foreach (var x in Controles)
			{
				x.LoadContent ();
			}
		}

		public SpriteBatch GetNewBatch ()
		{
			return Juego.GetNewBatch ();
		}

		public virtual void Update (GameTime gameTime)
		{
			foreach (var x in new List<IControl> (Controles))
				x.Update (gameTime);
		}

		public virtual void UnloadContent ()
		{
			foreach (var x in new List<IControl> (Controles))
			{
				x.Dispose ();
			}
			Escuchando = false; // Evitar fugas de memoria
		}

		public ContentManager Content
		{
			get
			{
				return Juego.Content;
			}
		}

		public SpriteBatch Batch { get; private set; }

		public DisplayMode GetDisplayMode
		{
			get
			{
				return Juego.GetDisplayMode;
			}
		}

		public GraphicsDevice Device
		{
			get
			{
				return Juego.GraphicsDevice;
			}
		}

		public override string ToString ()
		{
			return string.Format ("[{0}]", GetType ());
		}
	}
}


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un sprite sin posición
	/// </summary>
	public class FlyingSprite : IDibujable, IColorable, IComponent, IActivable
	{
		/// <summary>
		/// Dibuja el sprite en algún rectángulo
		/// </summary>
		/// <param name="bat">Batch</param>
		/// <param name="rect">Rectángulo de dibujo</param>
		public void Draw (SpriteBatch bat, Rectangle rect)
		{
			bat.Draw (Texture, rect, Color);
		}

		/// <summary>
		/// Carga el contenido gráfico.
		/// </summary>
		/// <param name="manager">Manager.</param>
		public void LoadContent (ContentManager manager)
		{
			Texture = manager.Load<Texture2D> (TextureName);
		}

		/// <summary>
		/// Desarga el contenido gráfico.
		/// </summary>
		public void UnloadContent ()
		{
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.FlyingSprite"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Moggle.Controles.FlyingSprite"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Moggle.Controles.FlyingSprite"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="Moggle.Controles.FlyingSprite"/>
		/// so the garbage collector can reclaim the memory that the <see cref="Moggle.Controles.FlyingSprite"/> was occupying.</remarks>
		public void Dispose ()
		{
			UnloadContent ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public void Initialize ()
		{
		}

		public event EventHandler AlActivar;

		public void Activar ()
		{
			AlActivar?.Invoke (this, EventArgs.Empty);
		}

		/// <summary>
		/// Gets the texture.
		/// </summary>
		/// <value>The texture.</value>
		public Texture2D Texture { get; private set; }

		/// <summary>
		/// Gets or sets the name of the texture.
		/// </summary>
		/// <value>The name of the texture.</value>
		public string TextureName { get; set; }

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color { get; set; }
	}

	/*

	/// <summary>
	/// Un control rectangular que inteligentemente acomoda una lista de botones.
	/// </summary>
	public class ContenedorBotón : Contenedor<InternalBotón>
	{
		#region Estado

		/// <summary>
		/// Lista de botones en el contenedor.
		/// </summary>
		List<InternalBotón> controles { get; }

		#endregion

		#region Dibujo

		/// <summary>
		/// Dibuja el control.
		/// Esto por sí solo no dibujará los botones.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Draw (GameTime gameTime)
		{
			Screen.Batch.Draw (
				texturaFondo,
				GetBounds (),
				BgColor);
		}

		#endregion

		#region Contenedor

		InternalBotón botónEnÍndice (int index)
		{
			return controles [index];
		}

		/// <summary>
		/// Devuelve el botón que está en un índice dado.
		/// </summary>
		/// <param name="index">Índice base cero del botón.</param>
		public IBotón BotónEnÍndice (int index)
		{
			return controles [index];
		}

		/// <summary>
		/// Agrega un botón al contenedor y lo devuelve.
		/// </summary>
		public Botón Add ()
		{
			return Add (Count);
		}

		/// <summary>
		/// Inserta un botón en un índice dado, y lo devuelve.
		/// </summary>
		/// <param name="índice">Índice del botón</param>
		public Botón Add (int índice)
		{
			var ret = new InternalBotón (Screen, CalcularPosición (índice));
			controles.Insert (índice, ret);
			initializeButton (índice);
			
			// desplazar los otros controles
			for (int i = índice + 1; i < Count; i++)
				controles [i].Bounds = CalcularPosición (i);
			
			ret.Habilidato = true;
			return ret;
		}

		/// <summary>
		/// Vacía y desecha los botones.
		/// </summary>
		public void Clear ()
		{
			for (int i = 0; i < controles.Count; i++)
				deinitializeButton (i);
			controles.Clear ();
		}

		void deinitializeButton (int index)
		{
			var bt = controles [index];
			bt.Enabled = false;
			bt.AlClick -= clickOnAButton;
		}

		void initializeButton (int index)
		{
			var bt = controles [index];
			bt.Enabled = true;
			bt.AlClick += clickOnAButton;
		}

		/// <summary>
		/// Elimina un botón dado.
		/// </summary>
		/// <param name="control">Botón a eliminar.</param>
		[Obsolete ("Usar RemoveAt")]
		public void Remove (Botón control)
		{
			throw new NotImplementedException ();
			//controles.Remove (control);
		}

		/// <summary>
		/// Elimina el botón en un índice dado
		/// </summary>
		/// <param name="i">Índice base cero.</param>
		public void RemoveAt (int i)
		{
			deinitializeButton (i);
			controles.RemoveAt (i);
		}

		/// <summary>
		/// Devuelve el número de botones.
		/// </summary>
		public int Count
		{
			get
			{
				return controles.Count;
			}
		}

		#endregion

		#region Evento

		void clickOnAButton (object sender, MouseEventArgs e)
		{
			var index = controles.IndexOf (sender as InternalBotón);
			AlActivarBotón?.Invoke (
				this,
				new ContenedorBotónIndexEventArgs (
					index,
					controles [index],
					e));
		}

		/// <summary>
		/// Ocurre cuando un botón es activado
		/// </summary>
		public event EventHandler< ContenedorBotónIndexEventArgs> AlActivarBotón;

		#endregion

		#region Memoria

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.ContenedorBotón"/> object.
		/// Libera a este control y a cada uno de sus botones.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			texturaFondo = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void LoadContent (ContentManager manager)
		{
			texturaFondo = manager.Load<Texture2D> (TextureFondo);
		}

		#endregion

		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public ContenedorBotón (IScreen screen)
			: base (screen)
		{
			controles = new List<InternalBotón> ();
		}

		#endregion

		/// <summary>
		/// </summary>
		public class ContenedorBotónIndexEventArgs : EventArgs
		{
			/// <summary>
			/// Gets the index of the pressed button
			/// </summary>
			/// <value>The index.</value>
			public int Index { get; }

			/// <summary>
			/// Gets the pressed button;
			/// </summary>
			public IBotón Botón { get; }

			/// <summary>
			/// The mouse event args
			/// </summary>
			public MouseEventArgs Mouse;

			internal ContenedorBotónIndexEventArgs (int index,
			                                        IBotón bt,
			                                        MouseEventArgs m)
			{
				Index = index;
				Botón = bt;
				Mouse = m;
			}
		}

		/// <summary>
		/// Tipo de orden lexicográfico para los botones.
		/// </summary>

	}

	public class InternalBotón : Botón
	{
		public InternalBotón (IScreen screen, RectangleF bounds)
			: base (screen, bounds)
		{
		}
	}
	*/
}
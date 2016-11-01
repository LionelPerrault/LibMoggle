using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

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
		public void AddContent (BibliotecaContenido manager)
		{
			manager.AddContent (TextureName);
			//Texture = manager.Load<Texture2D> (TextureName);
		}

		/// <summary>
		/// Carga el contenido gráfico.
		/// </summary>
		/// <param name="manager">Manager.</param>
		void IComponent.InitializeContent (BibliotecaContenido manager)
		{
			Texture = manager.GetContent<Texture2D> (TextureName);
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
		void IGameComponent.Initialize ()
		{
		}

		/// <summary>
		/// Ocurre cuando se activa.
		/// </summary>
		public event EventHandler AlActivar;

		void IActivable.Activar ()
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
}
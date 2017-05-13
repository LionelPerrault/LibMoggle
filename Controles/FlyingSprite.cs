using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
		void IComponent.LoadContent (ContentManager manager)
		{
			Texture = Texture ?? manager.Load<Texture2D> (TextureName);
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

		bool IActivable.Activar ()
		{
			AlActivar?.Invoke (this, EventArgs.Empty);
			return true;
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

		ContentManager Manager { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FlyingSprite"/> class.
		/// </summary>
		/// <param name="manager">Manejador de contenido donde se suscribe esta clase</param>
		public FlyingSprite (ContentManager manager)
		{
			if (manager == null)
				throw new ArgumentNullException (nameof (manager));
			Manager = manager;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FlyingSprite"/> class.
		/// </summary>
		/// <param name="texture">Textura</param>
		public FlyingSprite (Texture2D texture)
		{
			if (texture == null)
				throw new ArgumentNullException (nameof (texture));
			Texture = texture;
		}
	}
}
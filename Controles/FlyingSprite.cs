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
		public void AddContent ()
		{
			Manager.AddContent (TextureName);
		}

		/// <summary>
		/// Carga el contenido gráfico.
		/// </summary>
		void IComponent.InitializeContent ()
		{
			Texture = Manager.GetContent<Texture2D> (TextureName);
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
		/// Devuelve el manejador de contenidos
		/// </summary>
		public BibliotecaContenido Manager { get; }

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

		/// <summary>
		/// Initializes a new instance of the <see cref="Moggle.Controles.FlyingSprite"/> class.
		/// </summary>
		/// <param name="manager">Manejador de contenido donde se suscribe esta clase</param>
		public FlyingSprite (BibliotecaContenido manager)
		{
			Manager = manager;
		}
	}
}
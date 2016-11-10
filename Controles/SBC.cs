using System;
using Moggle.Screens;
using MonoGame.Extended.Shapes;
using Inputs = MonoGame.Extended.InputListeners;

namespace Moggle.Controles
{
	/// <summary>
	/// Single buffered control
	/// </summary>
	public abstract class SBC : IControl
	{
		/// <summary>
		/// Pantalla del control
		/// </summary>
		/// <value>The screen.</value>
		public IScreen Screen { get { return this.GetScreen (); } }

		/// <summary>
		/// Gets the game.
		/// </summary>
		/// <value>The game.</value>
		public Game Game { get { return this.GetGame (); } }

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>The container.</value>
		public IComponentContainerComponent<IControl> Container { get; }

		/// <summary>
		/// </summary>
		/// <param name="cont">Container</param>
		protected SBC (IComponentContainerComponent<IControl> cont)
		{
			Container = cont;
			Container.AddComponent (this);
		}

		/// <summary>
		/// Loads the content.
		/// </summary>
		protected virtual void AddContent ()
		{
		}

		/// <summary>
		/// Vincula el contenido a campos de clase
		/// </summary>
		protected virtual void InitializeContent ()
		{
		}

		void IComponent.InitializeContent ()
		{
			InitializeContent ();
		}

		void IComponent.AddContent ()
		{
			AddContent ();
		}

		/// <summary>
		/// Prioridad de dibujo;
		/// Mayor prioridad se dibuja en la cima
		/// </summary>
		[Obsolete]
		public int Prioridad { get; set; }

		/// <summary>
		/// Gets a value indicating whether this control is initialized.
		/// </summary>
		/// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
		public bool IsInitialized { get; private set; }

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores.
		/// No invoca LoadContent por lo que es seguro agregar componentes
		/// </summary>
		public virtual void Initialize ()
		{
			IsInitialized = true;
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		protected abstract IShapeF GetBounds ();

		/// <summary>
		/// Determina si el apuntador del ratón está sobre este control.
		/// </summary>
		[Obsolete ("Eventualmente dejará de ser obsoleto.")]
		public bool MouseOver
		{
			get
			{
				var state = Microsoft.Xna.Framework.Input.Mouse.GetState ();
				return GetBounds ().Contains (state.Position.ToVector2 ());
			}
		}

		/// <summary>
		/// Devuelve el tiempo en el que el apuntador ha estado sobre este control.
		/// </summary>
		[Obsolete ("Eventualmente dejará de ser obsoleto.")]
		public TimeSpan TiempoMouseOver { get; private set; }
	}
}
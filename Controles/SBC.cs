using System;
using Moggle.Screens;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;
using Inputs = MonoGame.Extended.InputListeners;
using Microsoft.Xna.Framework;

namespace Moggle.Controles
{
	/// <summary>
	/// Single buffered control
	/// </summary>
	public abstract class SBC : IComponent
	{
		/// <summary>
		/// Pantalla del control
		/// </summary>
		/// <value>The screen.</value>
		public IScreen Screen { get { return this.GetScreen (); } }

		public Game Game { get { return this.GetGame (); } }


		public IComponentContainerComponent<IGameComponent> Container { get; }

		/// <summary>
		/// </summary>
		/// <param name="cont">Container</param>
		protected SBC (IComponentContainerComponent<IGameComponent> cont)
		{
			Container = cont;
		}

		protected virtual void LoadContent ()
		{
		}

		protected virtual void UnloadContent ()
		{
		}

		void IComponent.LoadContent ()
		{
			LoadContent ();
		}

		void IComponent.UnloadContent ()
		{
			UnloadContent ();
		}

		/// <summary>
		/// Prioridad de dibujo;
		/// Mayor prioridad se dibuja en la cima
		/// </summary>
		[Obsolete]
		public int Prioridad { get; set; }

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores
		/// </summary>
		public virtual void Initialize ()
		{
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		public abstract IShapeF GetBounds ();

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

		/// <summary>
		/// Shuts down the component.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected virtual void Dispose (bool disposing)
		{
		}
	}
}
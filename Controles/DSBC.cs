using System;
using Inputs = MonoGame.Extended.InputListeners;
using Microsoft.Xna.Framework;
using MonoGame.Extended.InputListeners;

namespace Moggle.Controles
{
	/// <summary>
	/// Un <see cref="SBC"/> que puede ser dibujado y actualizado.
	/// </summary>
	public abstract class DSBC : SBC, IDrawable, IUpdateable
	{
		int _drawOrder;
		int _updateOrder;
		bool _visible;
		bool _enabled;

		/// <summary>
		/// Dibuja el control.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public abstract void Draw (GameTime gameTime);

		/// <summary>
		/// El orden de dibujo
		/// </summary>
		/// <value>The draw order.</value>
		public int DrawOrder
		{
			get{ return _drawOrder; }
			set
			{
				if (_drawOrder != value)
					DrawOrderChanged?.Invoke (this, EventArgs.Empty);
				_drawOrder = value;
			}
		}

		/// <summary>
		/// El orden de update.
		/// </summary>
		/// <value>The update order.</value>
		public int UpdateOrder
		{
			get
			{
				return _updateOrder;
			}
			set
			{
				if (_updateOrder != value)
					UpdateOrderChanged?.Invoke (this, EventArgs.Empty);
				_updateOrder = value;
			}
		}

		/// <summary>
		/// Determina si el control es visible
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
		public bool Visible
		{
			get
			{
				return _visible;
			}
			set
			{
				if (_visible != value)
					VisibleChanged?.Invoke (this, EventArgs.Empty);
				_visible = value;
			}
		}

		/// <summary>
		/// Determina si el control está habilitado.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				if (_enabled != value)
					EnabledChanged?.Invoke (this, EventArgs.Empty);
				_enabled = value;
			}
		}

		/// <summary>
		/// Update lógico
		/// </summary>
		public abstract void Update (GameTime gameTime);

		/// <summary>
		/// Ocurre al cambiar el orden de dibujo.
		/// </summary>
		public event EventHandler<EventArgs> DrawOrderChanged;

		/// <summary>
		/// Ocurre al cambiar visibilidad
		/// </summary>
		public event EventHandler<EventArgs> VisibleChanged;

		/// <summary>
		/// Ocurre al (des)habilidar.
		/// </summary>
		public event EventHandler<EventArgs> EnabledChanged;

		/// <summary>
		/// Ocurre al cambiar orden de update
		/// </summary>
		public event EventHandler<EventArgs> UpdateOrderChanged;

		/// <summary>
		/// </summary>
		/// <param name="cont">Contenedor</param>
		public DSBC (IComponentContainerComponent<IGameComponent> cont)
			: base (cont)
		{
			Visible = true;
			Enabled = true;
		}

		/// <summary>
		/// Inicializa esta instancia
		/// </summary>
		public override void Initialize ()
		{
			base.Initialize ();
			Game.MouseListener.MouseClicked += check_click;
			Game.MouseListener.MouseDoubleClicked += check_2click;
		}

		void check_2click (object sender, MouseEventArgs e)
		{
			if (Enabled && GetBounds ().Contains (e.Position.ToVector2 ()))
				OnDoubleClick (e);
		}

		void check_click (object sender, MouseEventArgs e)
		{
			if (Enabled && GetBounds ().Contains (e.Position.ToVector2 ()))
				OnClick (e);
		}

		/// <summary>
		/// This control was clicked.
		/// </summary>
		protected virtual void OnClick (MouseEventArgs args)
		{
		}

		/// <summary>
		/// This control was double clicked.
		/// </summary>
		protected virtual void OnDoubleClick (MouseEventArgs args)
		{
		}

		/// <summary>
		/// Shuts down the component.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose (bool disposing)
		{
			Game.MouseListener.MouseClicked -= check_click;
			Game.MouseListener.MouseDoubleClicked -= check_2click;
		}
	}
	
}
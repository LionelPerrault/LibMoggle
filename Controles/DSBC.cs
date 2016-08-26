using System;
using Inputs = MonoGame.Extended.InputListeners;
using Microsoft.Xna.Framework;
using MonoGame.Extended.InputListeners;

namespace Moggle.Controles
{
	public abstract class DSBC : SBC, IDrawable, IUpdateable
	{
		public abstract void Draw (GameTime gameTime);

		public virtual int DrawOrder { get { return 0; } }

		public virtual bool Visible { get; }

		public abstract void Update (GameTime gameTime);

		public virtual bool Enabled { get; }

		public virtual int UpdateOrder { get { return 0; } }

		public event EventHandler<EventArgs> DrawOrderChanged;

		public event EventHandler<EventArgs> VisibleChanged;

		public event EventHandler<EventArgs> EnabledChanged;

		public event EventHandler<EventArgs> UpdateOrderChanged;

		public DSBC (IComponentContainerComponent<IGameComponent> cont)
			: base (cont)
		{
			Visible = true;
			Enabled = true;
		}

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

		protected override void Dispose (bool disposing)
		{
			Game.MouseListener.MouseClicked -= check_click;
			Game.MouseListener.MouseDoubleClicked -= check_2click;
		}
	}
	
}
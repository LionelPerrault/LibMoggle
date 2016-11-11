using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Moggle.Controles
{
	public class MouseObserver : IUpdateable
	{
		readonly List<ObservationState> observedObjects;

		public void Update (GameTime gameTime)
		{
			var mouseState = Mouse.GetState ();
			foreach (var obj in observedObjects)
			{
				var shape = obj.ObservedObject.GetBounds ();
				if (shape.GetBoundingRectangle ().Contains (mouseState.Position) &&
				    shape.Contains (mouseState.Position.ToVector2 ()))
				{
					obj.AcumularTiempo (gameTime.ElapsedGameTime);
					if (!obj.IsMouseOn)
						OnRatónEncima (obj.ObservedObject);
				}
				else
				{
					// El ratón no está sobre el objeto
					if (obj.IsMouseOn)
					{
						// El ratón estaba sobre el objeto
						// Observe que se invoca el evento antes que el reset, 
						// es para poder enviar el tiempo que estuvo encima
						OnRatónSeFue (obj.ObservedObject);
						obj.Reset ();
					}
				}
			}
		}

		public bool Enabled
		{
			get
			{
				throw new NotImplementedException ();
			}
		}

		public int UpdateOrder
		{
			get
			{
				throw new NotImplementedException ();
			}
		}

		/// <summary>
		/// Raises the ratón encima event.
		/// </summary>
		/// <param name="e">E.</param>
		protected virtual void OnRatónEncima (ISpaceable e)
		{
			RatónEncima?.Invoke (this, e);
		}

		/// <summary>
		/// Raises the ratón se fue event.
		/// </summary>
		/// <param name="e">E.</param>
		protected virtual void OnRatónSeFue (ISpaceable e)
		{
			RatónSeFue?.Invoke (this, e);
		}

		event EventHandler<EventArgs> IUpdateable.EnabledChanged
		{
			add{}remove{}
		}

		event EventHandler<EventArgs> IUpdateable.UpdateOrderChanged
		{
			add{}remove{}
		}

		/// <summary>
		/// Ocurre cuando el ratón pasa por encima del objeto
		/// </summary>
		public event EventHandler<ISpaceable> RatónEncima;
		/// <summary>
		/// Ocurre cuando el ratón ya no está encima del objeto
		/// </summary>
		public event EventHandler<ISpaceable> RatónSeFue;

		public MouseObserver ()
		{
			observedObjects = new List<ObservationState> ();
		}

		public class ObservationState
		{
			public readonly ISpaceable ObservedObject;

			public TimeSpan TimeOnObject { get; private set; }

			public bool IsMouseOn { get { return TimeSpan.Zero != TimeOnObject; } }

			internal void AcumularTiempo (TimeSpan time)
			{
				TimeOnObject += time;
			}

			internal void Reset ()
			{
				TimeOnObject = TimeSpan.Zero;
			}

			public ObservationState (ISpaceable observedObject)
			{
				ObservedObject = observedObject;
			}
		}
	}
}
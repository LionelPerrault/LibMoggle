using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Moggle.Controles
{
	public class MouseObserver : IUpdateable
	{
		readonly List<ObservationState> observedObjects;

		public void ObserveObject (ISpaceable obj)
		{
			if (IsBeingObserved (obj))
				throw new InvalidOperationException ("Object already being observed.");
			var state = new ObservationState (obj);
			observedObjects.Add (state);
		}

		public void UnobserveObject (ISpaceable obj)
		{
			observedObjects.RemoveAll (z => z.ObservedObject.Equals (obj));
		}

		public bool IsBeingObserved (ISpaceable obj)
		{
			return observedObjects.Exists (z => z.ObservedObject.Equals (obj));
		}


		public ObservationState GetState (ISpaceable obj)
		{
			// Recordar que nunca, los objetos observados, son nulos
			foreach (var x in observedObjects)
				if (object.Equals (x.ObservedObject, obj))
					return x;
			throw new InvalidOperationException (
				string.Format (
					"Object {0} is not being observed.",
					obj));
		}

		public void Update (GameTime gameTime)
		{
			var mouseState = Mouse.GetState ();
			foreach (var obj in observedObjects)
			{
				if (obj.ObservedObject.GetBounds ().Contains (mouseState.Position.ToVector2 ()))
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

		public bool Enabled { get; set; }

		int IUpdateable.UpdateOrder { get { return 0; } }

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
				if (observedObject == null)
					throw new ArgumentNullException ("observedObject");
				ObservedObject = observedObject;
			}
		}
	}
}
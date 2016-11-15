using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace Moggle.Screens
{
	/// <summary>
	/// Maneja un conjunto de <see cref="ScreenThread"/>
	/// </summary>
	public class ScreenThreadManager
	{
		readonly List<ScreenThread> _screens;

		/// <summary>
		/// Devuelve el número de hilos registrados
		/// </summary>
		/// <value>The count.</value>
		public int Count { get { return _screens.Count; } }

		int _currentThreadIndex;

		/// <summary>
		/// Devuelve o establece el hilo activo
		/// </summary>
		public ScreenThread ActiveThread
		{
			get
			{
				if (_currentThreadIndex == -1)
					throw new InvalidOperationException ("Cannot get the current thread.");
				return _screens [_currentThreadIndex];
			}
			set
			{
				SetActiveThread (value);
			}
		}

		/// <summary>
		/// Cambia el hilo activo
		/// </summary>
		public void SetActiveThread (ScreenThread scrTh)
		{
			var i = _screens.IndexOf (scrTh);
			if (i < 0)
				throw new InvalidOperationException (
					string.Format (
						"Cannot set thread {0} as active, it is not actually managed.",
						scrTh)
				);
			_currentThreadIndex = i;
		}

		/// <summary>
		/// Registra un hilo
		/// </summary>
		/// <param name="scrTh">Hilo a registrar</param>
		public void AddThread (ScreenThread scrTh)
		{
			if (_screens.Contains (scrTh))
				throw new InvalidOperationException ("Duplicated screen thread.");
			_screens.Add (scrTh);
			if (_currentThreadIndex < 0)
				_currentThreadIndex = 0;
		}

		/// <summary>
		/// Contruye y registra un nuevo hilo
		/// </summary>
		public ScreenThread AddNewThread ()
		{
			var ret = new ScreenThread ();
			AddThread (ret);
			return ret;
		}

		/// <summary>
		/// Des-registra un hilo
		/// </summary>
		public bool RemoveThread (ScreenThread scrTh)
		{
			return _screens.Remove (scrTh);
		}

		/// <summary>
		/// Desregistra y libera un hilo
		/// </summary>
		public void RemoveAndDispose (ScreenThread scrTh)
		{
			if (_screens.Remove (scrTh))
				scrTh.Dispose ();
			else
				throw new InvalidOperationException ("Cannot remove non-existent thread.");
		}

		/// <summary>
		/// Actualiza el hilo activo
		/// </summary>
		public void UpdateActive (GameTime gameTime)
		{
			ActiveThread.Update (gameTime);
		}

		/// <summary>
		/// Dibuja el hilo activo
		/// </summary>
		public void DrawActive ()
		{
			ActiveThread.Draw ();
		}

		internal ScreenThreadManager ()
		{
			_currentThreadIndex = -1;
			_screens = new List<ScreenThread> ();
		}
	}
}
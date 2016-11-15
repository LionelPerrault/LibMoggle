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

		public int Count { get { return _screens.Count; } }

		int _currentThreadIndex;

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

		public void AddThread (ScreenThread scrTh)
		{
			if (_screens.Contains (scrTh))
				throw new InvalidOperationException ("Duplicated screen thread.");
			_screens.Add (scrTh);
			if (_currentThreadIndex < 0)
				_currentThreadIndex = 0;
		}

		public ScreenThread AddNewThread ()
		{
			var ret = new ScreenThread ();
			// Remark: no need to duplicate check on this one.
			_screens.Add (ret);
			if (_currentThreadIndex < 0)
				_currentThreadIndex = 0;
			return ret;
		}

		public bool RemoveThread (ScreenThread scrTh)
		{
			return _screens.Remove (scrTh);
		}

		public void RemoveAndDispose (ScreenThread scrTh)
		{
			if (_screens.Remove (scrTh))
				scrTh.Dispose ();
			else
				throw new InvalidOperationException ("Cannot remove non-existent thread.");
		}

		public void UpdateActive (GameTime gameTime)
		{
			ActiveThread.Update (gameTime);
		}

		public void DrawActive (GameTime gameTime)
		{
			ActiveThread.Draw (gameTime);
		}

		public ScreenThreadManager ()
		{
			_currentThreadIndex = -1;
			_screens = new List<ScreenThread> ();
		}
	}
}
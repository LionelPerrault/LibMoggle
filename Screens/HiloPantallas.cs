using System.Collections.Generic;

namespace Moggle.Screens
{
	/// <summary>
	/// Representa una serie de invocaciones de <see cref="IScreen"/>
	/// </summary>
	public class HiloPantallas
	{
		readonly List<IScreen> InvocationStack;
		readonly List<ScreenStackOptions> Options;

		public int Count { get { return InvocationStack.Count; } }

		public IScreen this [int index]
		{
			get
			{
				return InvocationStack [Count - index - 1];
			}
		}

		ScreenStackOptions getOptionsFromNewIndex (int index)
		{
			return Options [Count - index - 1];
		}

		public void Stack (IScreen scr, ScreenStackOptions opt)
		{
			InvocationStack.Add (scr);
			Options.Add (opt);
		}

		public IScreen Current{ get { return this [0]; } }

		public void Stack (IScreen scr)
		{
			Stack (scr, ScreenStackOptions.Default);
		}


		public HiloPantallas ()
		{
			InvocationStack = new List<IScreen> ();
			Options = new List<ScreenStackOptions> ();
		}

		public struct ScreenStackOptions
		{
			public bool DibujaBase;

			public static ScreenStackOptions Default;

			static ScreenStackOptions ()
			{
				Default = new ScreenStackOptions
				{
					DibujaBase = false 
				};
			}
		}
	}
}
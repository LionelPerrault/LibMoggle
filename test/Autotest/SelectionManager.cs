using NUnit.Framework;
using System.Collections.Generic;
using System;
using Moggle.Controles;

namespace Test
{
	[TestFixture]
	public class SelectionManagerTest
	{
		public List<int> Selectable;
		public Random r = new Random ();
		public SelectionManager<int> Selector;

		[TestFixtureSetUp]
		public void Setup ()
		{
			var i = r.Next (4, 1000);
			Selectable = new List<int> (i);
			for (int j = 0; j < i; j++)
			{
				Selectable.Add (j);
			}

			Selector = new SelectionManager<int> (Selectable);
		}

		[Test]
		public void TestSelect ()
		{
			Selector.AllowEmpty = false;
			Selector.AllowMultiple = false;
			Assert.AreEqual (1, Selector.GetSelection ().Count);
			Selector.Select (0);
			Selector.Select (1);
			Assert.True (Selector.IsSelected (1));
			Assert.AreEqual (1, Selector.GetSelection ().Count);

			Selector.AllowEmpty = true;
			Selector.AllowMultiple = false;
			Selector.ClearSelection ();
			Assert.AreEqual (0, Selector.GetSelection ().Count);
			Selector.Select (0);
			Selector.Select (1);
			Assert.True (Selector.IsSelected (1));
			Assert.AreEqual (1, Selector.GetSelection ().Count);

			Selector.AllowEmpty = false;
			Selector.AllowMultiple = true;
			Selector.ClearSelection ();
			Assert.AreEqual (1, Selector.GetSelection ().Count);
			Selector.Select (0);
			Selector.Select (1);
			Assert.True (Selector.IsSelected (1));
			Assert.AreEqual (2, Selector.GetSelection ().Count);

			Selector.AllowEmpty = true;
			Selector.AllowMultiple = true;
			Selector.ClearSelection ();
			Assert.AreEqual (0, Selector.GetSelection ().Count);
			Selector.Select (0);
			Selector.Select (1);
			Assert.True (Selector.IsSelected (1));
			Assert.AreEqual (2, Selector.GetSelection ().Count);
		}
	}
}
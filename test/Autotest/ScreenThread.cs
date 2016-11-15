using NUnit.Framework;
using Moggle.Screens;
using Moggle;

namespace Test
{
	[TestFixture]
	public class ScreenThreadTest
	{
		Game Game;

		[TestFixtureSetUp]
		public void Init ()
		{
			Game = new Game ();
			Game.ScreenManager.AddNewThread ();
		}

		[Test]
		public void ScreenThreadBasics ()
		{
			var z = new ScreenThread ();
			z.Stack (null);
			z.Stack (null);

			Assert.AreEqual (2, z.Count);
		}

		[Test]
		public void TestManInit ()
		{
		}
	}
}
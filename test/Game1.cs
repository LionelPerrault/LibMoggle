using Microsoft.Xna.Framework;
using Moggle.Controles;

namespace Test
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Moggle.Game
	{
		protected override void Initialize ()
		{
			ScreenManager.AddNewThread ().Stack (new Scr (this));
			Graphics.IsFullScreen = false;
			//CurrentScreen.Initialize ();
			var ms = new Ratón (this);
			ms.OffSet = new Point (-10, -10);
			ms.ArchivoTextura = @"cont/void";
			base.Initialize ();
		}
	}
}
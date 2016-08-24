#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Test
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Moggle.Game
	{
		public Game1 ()
		{
			Graphics.IsFullScreen = false;
			CurrentScreen = new Scr (this);

			Mouse.ArchivoTextura = @"cont/void";
		}
	}
}
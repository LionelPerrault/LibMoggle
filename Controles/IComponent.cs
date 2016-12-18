using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Moggle.Screens;

namespace Moggle.Controles
{
	/// <summary>
	/// Represents an objetcs that can be initialized and load external content
	/// </summary>
	public interface IComponent : IGameComponent
	{
		/// <summary>
		/// Loads the content using a given manager
		/// </summary>
		void LoadContent (ContentManager manager);
	}

	/// <summary>
	/// Component ext.
	/// </summary>
	static class ComponentExt
	{
		/// <summary>
		/// Devuelve el <see cref="IScreen"/> que contiene este control.
		/// </summary>
		/// <returns>The screen.</returns>
		public static IScreen GetScreen (this IControl comp)
		{
			var container = comp.Container;
			if (container == null)
				throw new Exception ();
			var scr = container as IScreen;
			return scr ?? (container as IControl).GetScreen ();
		}

		/// <summary>
		/// Devuelve el Game de este control
		/// </summary>
		/// <returns>The game.</returns>
		public static Game GetGame (this IControl comp)
		{
			var container = comp.Container;
			if (container == null)
				throw new Exception ();
			var gm = container as Game;
			return gm ?? (container as IControl).GetGame ();
		}
	}
}
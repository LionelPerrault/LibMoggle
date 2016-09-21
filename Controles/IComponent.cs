using System;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using Microsoft.Xna.Framework.Content;

namespace Moggle.Controles
{

	/// <summary>
	/// Puede ejecutar 
	/// <see cref="LoadContent"/>
	/// y 
	/// <see cref="UnloadContent"/>
	/// </summary>
	public interface IComponent : IGameComponent, IDisposable
	{
		/// <summary>
		/// Carga el contenido gráfico.
		/// </summary>
		void LoadContent (ContentManager manager);

		/// <summary>
		/// Desarga el contenido gráfico.
		/// </summary>
		void UnloadContent ();

	}

	/// <summary>
	/// Component ext.
	/// </summary>
	public static class ComponentExt
	{
		/// <summary>
		/// Devuelve el <see cref="IScreen"/> que contiene este control.
		/// </summary>
		/// <returns>The screen.</returns>
		public static IScreen GetScreen (this IComponent comp)
		{
			var container = comp.Container;
			if (container == null)
				throw new Exception ();
			var scr = container as IScreen;
			return scr ?? container.GetScreen ();
		}

		/// <summary>
		/// Devuelve el Game de este control
		/// </summary>
		/// <returns>The game.</returns>
		public static Game GetGame (this IComponent comp)
		{
			var container = comp.Container;
			if (container == null)
				throw new Exception ();
			var gm = container as Game;
			return gm ?? container.GetGame ();
		}
	}
}
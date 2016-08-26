using Microsoft.Xna.Framework;
using System.Security.Policy;
using Moggle.Screens;
using System;

namespace Moggle.Controles
{

	/// <summary>
	/// Puede ejecutar 
	/// <see cref="LoadContent"/>
	/// y 
	/// <see cref="UnloadContent"/>
	/// </summary>
	public interface IComponent : IGameComponent
	{
		/// <summary>
		/// Carga el contenido gráfico.
		/// </summary>
		void LoadContent ();

		/// <summary>
		/// Desarga el contenido gráfico.
		/// </summary>
		void UnloadContent ();

		IComponentContainerComponent<IGameComponent> Container { get; }
		//Matrix GetContainerViewMatrix ();
	}

	public static class ComponentExt
	{
		public static IScreen GetScreen (this IComponent comp)
		{
			var container = comp.Container;
			if (container == null)
				throw new Exception ();
			var scr = container as IScreen;
			return scr ?? container.Container.GetScreen ();
		}

		public static Game GetGame (this IComponent comp)
		{
			var container = comp.Container;
			if (container == null)
				throw new Exception ();
			var scr = container as Game;
			return scr ?? container.Container.GetGame ();
		}
	}
}
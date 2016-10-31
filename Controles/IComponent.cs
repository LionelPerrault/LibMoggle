using System;
using Microsoft.Xna.Framework;
using Moggle.Screens;

namespace Moggle.Controles
{

	/// <summary>
	/// Representa un objeto de juego que necesita cargar contenido de una <see cref="BibliotecaContenido"/>
	/// </summary>
	public interface IComponent : IGameComponent, IDisposable
	{
		/// <summary>
		/// Carga el contenido gráfico.
		/// </summary>
		void AddContent (BibliotecaContenido manager);

		/// <summary>
		/// Aquí se debe de asignar a variables de clase el contenido de manager
		/// </summary>
		/// <param name="manager">Biblioteca de contenido</param>
		void InitializeContent (BibliotecaContenido manager);
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
		public static IScreen GetScreen (this IControl comp)
		{
			var container = comp.Container;
			if (container == null)
				throw new Exception ();
			var scr = container as IScreen;
			// TODO: nullcheck
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
			// TODO: nullcheck
			return gm ?? (container as IControl).GetGame ();
		}
	}
}
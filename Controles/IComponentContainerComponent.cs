using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un objeto que contiene Componentes de algún tipo.
	/// </summary>
	public interface IComponentContainerComponent<TComp> : IComponent
		where TComp : IGameComponent
	{
		/// <summary>
		/// Agrega un componente
		/// </summary>
		void Add (TComp component);

		/// <summary>
		/// Elimina un componente
		/// </summary>
		/// <returns><c>true</c> si se eliminó el componente especificado.</returns>
		bool Remove (TComp component);

		/// <summary>
		/// Enumera las componentes.
		/// </summary>
		IEnumerable<TComp> Components { get; }
	}
}
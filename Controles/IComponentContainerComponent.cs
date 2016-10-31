using System.Collections.Generic;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un objeto que contiene Componentes de algún tipo.
	/// </summary>
	public interface IComponentContainerComponent<TComp> : IComponent
	{
		/// <summary>
		/// Agrega un componente
		/// </summary>
		void AddComponent (TComp component);

		/// <summary>
		/// Elimina un componente
		/// </summary>
		/// <returns><c>true</c> si se eliminó el componente especificado.</returns>
		bool RemoveComponent (TComp component);

		/// <summary>
		/// Enumera las componentes.
		/// </summary>
		IEnumerable<TComp> Components { get; }
	}
}
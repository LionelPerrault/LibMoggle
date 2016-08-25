using System;
using Moggle.Screens;

namespace Moggle.Controles
{
	/// <summary>
	/// Un control en un <see cref="IScreen"/>
	/// </summary>
	public interface IControl : IDisposable, IComponent
	{
		/// <summary>
		/// Pantalla a la que pertenece este control.
		/// </summary>
		/// <value>The screen.</value>
		IScreen Screen { get; }
	}
}
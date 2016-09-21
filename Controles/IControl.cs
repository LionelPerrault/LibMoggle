using Moggle.Screens;

namespace Moggle.Controles
{
	/// <summary>
	/// Un control en un <see cref="IScreen"/>
	/// </summary>
	public interface IControl : IComponent
	{
		/*
		/// <summary>
		/// Pantalla a la que pertenece este control.
		/// </summary>
		/// <value>The screen.</value>
		IScreen Screen { get; }
		*/
		/// <summary>
		/// Gets the container of the control.
		/// This could be the Screen or Game itself.
		/// </summary>
		IComponentContainerComponent<IControl> Container { get; }
	}
}

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un componente que puede ser encajado en un contenedor
	/// </summary>
	public interface IControl : IComponent
	{
		/// <summary>
		/// Gets the container of the control.
		/// This could be the Screen or Game itself.
		/// </summary>
		IComponentContainerComponent<IControl> Container { get; }
	}
}
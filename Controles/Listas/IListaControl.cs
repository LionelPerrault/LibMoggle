using Moggle.Controles;

namespace Moggle.Controles.Listas
{
	/// <summary>
	/// Representa un control con forma de lista
	/// </summary>
	public interface IListaControl : IControl
	{
		object Seleccionado { get; }

		void SeleccionaSiguiente ();

		void SeleccionaAnterior ();
	}

	/// <summary>
	/// Representa un control con forma de lista
	/// </summary>
	public interface IListaControl<T> : IListaControl
	{
		new T Seleccionado { get; }
	}
}
using Moggle.Controles;

namespace Moggle.Controles.Listas
{
	/// <summary>
	/// Representa un control con forma de lista
	/// </summary>
	public interface IListaControl : IControl
	{
		/// <summary>
		/// Devuelve el objeto seleccionado.
		/// </summary>
		object Seleccionado { get; }

		/// <summary>
		/// Establece el cursor en la posición siguiente.
		/// </summary>
		void SeleccionaSiguiente ();

		/// <summary>
		/// Establece el cursor en la posición anterior.
		/// </summary>
		void SeleccionaAnterior ();
	}

	/// <summary>
	/// Representa un control con forma de lista
	/// </summary>
	public interface IListaControl<T> : IListaControl
	{
		/// <summary>
		/// Devuelve el objeto seleccionado.
		/// </summary>
		new T Seleccionado { get; }
	}
}

namespace Moggle.Controles
{
	/// <summary>
	/// Representa el orden en el que se enlistan los objetos en un <see cref="Contenedor{T}"/>
	/// </summary>
	public enum TipoOrdenEnum
	{
		/// <summary>
		/// Llena las columnas antes que las filas.
		/// </summary>
		ColumnaPrimero,
		/// <summary>
		/// Llena las filas antes que las columnas.
		/// </summary>
		FilaPrimero
	}
}
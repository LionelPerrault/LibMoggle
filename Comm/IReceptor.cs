
namespace Moggle.Comm
{
	/// <summary>
	/// Puede recibir una señal de tecla.
	/// </summary>
	public interface IReceptor<T>
	{
		/// <summary>
		/// Rebice señal de un tipo dado
		/// </summary>
		/// <returns><c>true</c>, si la señal fue aceptada, <c>false</c> otherwise.</returns>
		/// <param name="signal">Señal recibida</param>
		bool RecibirSeñal (T signal);
	}
}
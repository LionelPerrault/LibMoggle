using MonoGame.Extended.InputListeners;

namespace Moggle.Comm
{
	/// <summary>
	/// Puede recibir una señal de tecla.
	/// </summary>
	public interface IReceptorTeclado
	{
		/// <summary>
		/// Rebice señal del teclado
		/// </summary>
		/// <returns><c>true</c>, si la señal fue aceptada, <c>false</c> otherwise.</returns>
		/// <param name="key">Señal tecla</param>
		bool RecibirSeñal (KeyboardEventArgs key);
	}
}
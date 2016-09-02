using MonoGame.Extended.InputListeners;

namespace Moggle.Comm
{

	/// <summary>
	///puede mandar una señal.
	/// </summary>
	public interface IEmisorTeclado
	{
		/// <summary>
		/// Manda señal de tecla presionada a esta pantalla
		/// </summary>
		/// <param name="key">Tecla de la señal</param>
		void MandarSeñal (KeyboardEventArgs key);
	}
}
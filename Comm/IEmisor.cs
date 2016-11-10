
namespace Moggle.Comm
{

	/// <summary>
	/// puede mandar una señal a sus
	/// </summary>
	public interface IEmisor<T>
	{
		/// <summary>
		/// Manda señal a sus escuchas
		/// </summary>
		/// <param name="key">Señal</param>
		void MandarSeñal (T key);
	}
}
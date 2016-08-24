using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using MonoGame.Extended.InputListeners;

namespace Moggle.Controles
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

	/// <summary>
	/// Puede ejecutar 
	/// <see cref="LoadContent"/>
	/// y 
	/// <see cref="UnloadContent"/>
	/// </summary>
	public interface IComponent : IGameComponent
	{
		/// <summary>
		/// Carga el contenido gráfico.
		/// </summary>
		void LoadContent ();

		/// <summary>
		/// Desarga el contenido gráfico.
		/// </summary>
		void UnloadContent ();
	}

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
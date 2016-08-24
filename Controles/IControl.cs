using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using MonoGame.Extended.InputListeners;
using Microsoft.Xna.Framework.Content;

namespace Moggle.Controles
{
	/// <summary>
	/// Puede recibir una señal de tecla.
	/// </summary>
	public interface IKeyCatcher
	{
		/// <summary>
		/// Rebice señal del teclado
		/// </summary>
		/// <returns><c>true</c>, si la señal fue aceptada, <c>false</c> otherwise.</returns>
		/// <param name="key">Señal tecla</param>
		bool RecibirSeñal (KeyboardEventArgs key);
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
		void LoadContent (ContentManager manager);

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

		/// <summary>
		/// Prioridad de dibujo;
		/// Mayor prioridad se dibuja en la cima
		/// </summary>
		int Prioridad { get; }

		/// <summary>
		/// Dibuja el control
		/// </summary>
		void Dibujar (GameTime gameTime);

		/// <summary>
		/// Ciclo de la lógica
		/// </summary>
		void Update (GameTime gameTime);

		/// <summary>
		/// Esta función establece el comportamiento de este control cuando el jugador presiona una tecla dada.
		/// </summary>
		/// <param name="key">Tecla presionada por el usuario.</param>
		void CatchKey (KeyboardEventArgs key);
	}
}
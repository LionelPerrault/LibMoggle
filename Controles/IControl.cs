using System;
using Moggle.Screens;
using OpenTK.Input;
using Microsoft.Xna.Framework;

namespace Moggle.Controles
{
	/// <summary>
	/// Un control en un <see cref="IScreen"/>
	/// </summary>
	public interface IControl : IDisposable
	{
		/// <summary>
		/// Pantalla a la que pertenece este control.
		/// </summary>
		/// <value>The screen.</value>
		IScreen Screen { get; }

		/// <summary>
		/// Incluir este control en su pantalla
		/// </summary>
		void Include ();

		/// <summary>
		/// Excluir este control de su pantalla
		/// </summary>
		void Exclude ();

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
		/// Cargar contenido
		/// </summary>
		void LoadContent ();

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores
		/// </summary>
		void Inicializar ();

		/// <summary>
		/// Esta función establece el comportamiento de este control cuando el jugador presiona una tecla dada.
		/// </summary>
		/// <param name="key">Tecla presionada por el usuario.</param>
		void CatchKey (Key key);
	}
}
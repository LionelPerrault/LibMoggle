using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using OpenTK.Input;

namespace Moggle.Controles
{
	public interface IControl: IDisposable
	{
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
		void Dibujar (GameTime  gameTime);

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

		void CatchKey (Key key);
	}
}
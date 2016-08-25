using System;
using Moggle.Screens;
using Microsoft.Xna.Framework;
using MonoGame.Extended.InputListeners;

namespace Moggle.Controles
{

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
	
}
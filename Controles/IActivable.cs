﻿using System;

namespace Moggle.Controles
{
	/// <summary>
	/// El objeto puede ser activado
	/// </summary>
	public interface IActivable
	{
		/// <summary>
		/// Manda señal de activación
		/// </summary>
		void Activar ();

		/// <summary>
		/// Ocurre cuando se activa.
		/// </summary>
		event EventHandler AlActivar;
	}
}
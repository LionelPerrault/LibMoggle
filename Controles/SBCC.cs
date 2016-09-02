using System;
using Moggle.Screens;

namespace Moggle.Controles
{

	/// <summary>
	/// Control cronometrado
	/// </summary>
	public abstract class SBCC : DSBC
	{
		#region Comportamiento

		/// <summary>
		/// Devuelve o establece la frecuencia de invocación del control.
		/// </summary>
		/// <seealso cref="AlTick"/>
		/// <value>The tiempo entre cambios.</value>
		public TimeSpan TiempoEntreCambios { get; set; }

		/// <summary>
		/// Tiempo acumulado desde el último cambio
		/// </summary>
		/// <value>The tiempo acumulado.</value>
		protected TimeSpan TiempoAcumulado { get; private set; }

		/// <summary>
		/// Ciclo de la lógica
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{
			TiempoAcumulado += gameTime.ElapsedGameTime;
			if (TiempoAcumulado > TiempoEntreCambios)
			{
				TiempoAcumulado = TimeSpan.Zero;
				OnChrono ();
			}
		}

		/// <summary>
		/// Se llama cuando ocurre el tick cronometrizado
		/// </summary>
		protected virtual void OnChrono ()
		{
			AlTick?.Invoke ();
		}

		#endregion

		#region Eventos

		/// <summary>
		/// Ocurre cuando cada periodo de invocación.
		/// </summary>
		/// <seealso cref="TiempoEntreCambios"/>
		public event Action AlTick;

		#endregion

		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		protected SBCC (IScreen screen)
			: base (screen)
		{
		}

		#endregion
	}
}
using Moggle.Screens;
using Microsoft.Xna.Framework;
using Moggle.Controles;
using System;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un SBC, con posición determinada por un VisualPortManager
	/// </summary>
	[ObsoleteAttribute ("Usar SBC y una Camera")]
	public abstract class SBCRel : SBC
	{
		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		protected SBCRel (IScreen screen)
			: base (screen)
		{
		}

		/// <summary>
		/// Puerto visual.
		/// </summary>
		public VisualPortManager Port;

		/// <summary>
		/// Dibuja el control
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Draw (GameTime gameTime)
		{
			throw new NotImplementedException ();
			//var rect = Port.UniversoAVentana (GetBounds ().GetBoundingRectangle ());
			//DibujarEn (gameTime, rect);
		}

		/// <summary>
		/// Determina si el apuntador del ratón está sobre este control.
		/// </summary>
		/// <value><c>true</c> if mouse over; otherwise, <c>false</c>.</value>
		public new bool MouseOver
		{
			get
			{
				throw new NotImplementedException ();
				//var state = Microsoft.Xna.Framework.Input.Mouse.GetState ();
				//return GetBounds ().Contains (Port.UniversoAVentana (state.Position));
			}
		}

		/// <summary>
		/// Dibujar el objeto en un rectángulo
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		/// <param name="rect">Rectángulo target</param>
		protected abstract void DibujarEn (GameTime gameTime, Rectangle rect);
	}
}
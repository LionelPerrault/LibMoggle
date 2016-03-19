using Moggle.Screens;
using Microsoft.Xna.Framework;
using Moggle.Controles;

namespace Moggle.Controles
{
	/// <summary>
	/// Representa un SBC, con posición determinada por un VisualPortManager
	/// </summary>
	public abstract class SBCRel : SBC
	{
		protected SBCRel (IScreen screen)
			: base (screen)
		{
		}

		public VisualPortManager Port;

		public override void Dibujar (GameTime gameTime)
		{
			var rect = Port.UniversoAVentana (GetBounds ().GetContainingRectangle ());
			DibujarEn (gameTime, rect);
		}

		public new bool MouseOver
		{
			get
			{
				var state = Microsoft.Xna.Framework.Input.Mouse.GetState ();
				return GetBounds ().Contains (Port.UniversoAVentana (state.Position));
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
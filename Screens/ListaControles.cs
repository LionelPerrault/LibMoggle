using System;
using System.Collections.Generic;
using Moggle.Controles;

namespace Moggle.Screens
{
	/// <summary>
	/// Ona lista ordenada de controles según prioridad.
	/// </summary>
	[Obsolete]
	public class ListaControl : SortedSet<IControl>
	{
		/// <summary>
		/// Clona esta lista como un <see cref="System.Collections.Generic.List{IControl}"/>
		/// </summary>
		public List<IControl> Clonar ()
		{
			return new List<IControl> (this);
		}
	}
}
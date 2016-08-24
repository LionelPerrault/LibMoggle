using System.Collections.Generic;
using Moggle.Controles;
using System;

namespace Moggle.Screens
{
	/// <summary>
	/// Ona lista ordenada de controles según prioridad.
	/// </summary>
	[ObsoleteAttribute]
	public class ListaControl : SortedSet<IControl>
	{
		/// <summary>
		/// </summary>
		public ListaControl ()
		{
		}

		/// <summary>
		/// Clona esta lista como un <see cref="System.Collections.Generic.List{IControl}"/>
		/// </summary>
		public List<IControl> Clonar ()
		{
			return new List<IControl> (this);
		}
	}
}
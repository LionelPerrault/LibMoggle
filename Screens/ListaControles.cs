using System.Collections.Generic;
using Moggle.Controles;

namespace Moggle.Screens
{
	/// <summary>
	/// Ona lista ordenada de controles según prioridad.
	/// </summary>
	public class ListaControl : SortedSet<IControl>
	{
		/// <summary>
		/// </summary>
		public ListaControl ()
			: base (Comparer<IControl>.Create ((x, y) => x.Prioridad < y.Prioridad ? -1 : 1))
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
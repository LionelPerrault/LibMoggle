using System.Collections.Generic;
using System;

namespace Moggle.Controles
{
	/// <summary>
	/// Maneja la selección de un control
	/// </summary>
	public class SelectionManager<T>
	{
		readonly List<T> _selectedItems;
		readonly ICollection<T> _completeItems;

		/// <summary>
		/// Gets the current selection
		/// </summary>
		public List<T> GetSelection ()
		{
			return new List<T> (_selectedItems);
		}

		/// <summary>
		/// Select an specified item
		/// </summary>
		public void Select (T item)
		{
			if (!_completeItems.Contains (item))
				throw new InvalidOperationException ("No se puede agregar un objeto no existente.");
			_selectedItems.Add (item);
		}

		/// <summary>
		/// Deselects an specified item
		/// </summary>
		public void Deselect (T item)
		{
			_selectedItems.Remove (item);
		}

		public bool IsSelected (T item)
		{
			return _selectedItems.Contains (item);
		}

		/// <summary>
		/// </summary>
		/// <param name="items">A collection to link this manager</param>
		public SelectionManager (ICollection<T> items)
		{
			_selectedItems = new List<T> ();
			_completeItems = items;
		}
	}
}
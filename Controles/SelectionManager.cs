using System.Collections.Generic;
using System;
using System.Linq;

namespace Moggle.Controles
{
	/// <summary>
	/// Maneja la selección de un control
	/// </summary>
	public class SelectionManager<T>
	{
		readonly List<T> _selectedItems;
		readonly ICollection<T> _completeItems;

		bool _allowMultiple;

		/// <summary>
		/// Gets or set a value indicating if multiple selectin is possible
		/// </summary>
		public bool AllowMultiple
		{
			get{ return _allowMultiple; }
			set
			{
				_allowMultiple = value;
				if (!value)
					setDefaultSelection ();
			}
		}

		bool _allowEmptySelection;

		/// <summary>
		/// Gets or set a value indicating if empty selection is possible 
		/// </summary>
		/// <value><c>true</c> if allow empty; otherwise, <c>false</c>.</value>
		public bool AllowEmpty
		{
			get{ return _allowEmptySelection; }
			set
			{
				_allowEmptySelection = value;
				if (!value)
					setDefaultSelection ();
			}
		}

		void setDefaultSelection ()
		{
			// TODO: throws error if the selectable objects is empty.
			_selectedItems.Clear ();
			if (!AllowEmpty)
				Select (_completeItems.First ());
		}

		/// <summary>
		/// Gets the current selection
		/// </summary>
		public List<T> GetSelection ()
		{
			return new List<T> (_selectedItems);
		}

		/// <summary>
		/// Sets the selection to the default state 
		/// (empty if it is possible, or first-element selection if not)
		/// </summary>
		public void ClearSelection ()
		{
			setDefaultSelection ();
		}

		/// <summary>
		/// Select an specified item
		/// </summary>
		public void Select (T item)
		{
			if (!_completeItems.Contains (item))
				throw new InvalidOperationException ("No se puede agregar un objeto no existente.");

			if (IsSelected (item))
				return;
			
			if (!AllowMultiple)
				_selectedItems.Clear ();
			
			_selectedItems.Add (item);
		}

		/// <summary>
		/// Deselects an specified item
		/// </summary>
		public void Deselect (T item)
		{
			// Do not remove if this is the only selected item
			// and it is not allowed to be empty
			if (_selectedItems.Count == 0)
				return;
			if (_selectedItems.Count == 1 && !AllowEmpty && item.Equals (_selectedItems [0]))
				return;
			
			_selectedItems.Remove (item);
		}

		/// <summary>
		/// Devuelve un valor que determina si un objeto dado está seleccionado
		/// </summary>
		/// <param name="item">Item.</param>
		public bool IsSelected (T item)
		{
			return _selectedItems.Contains (item);
		}

		/// <summary>
		/// Alterna la selección de un objeto dado
		/// </summary>
		/// <param name="item">Item.</param>
		public void ToggleSelection (T item)
		{
			if (IsSelected (item))
				Deselect (item);
			else
				Select (item);
		}

		/// <summary>
		/// </summary>
		/// <param name="items">A collection to link this manager</param>
		public SelectionManager (ICollection<T> items)
		{
			_selectedItems = new List<T> ();
			_completeItems = items;
			_allowMultiple = true;
		}
	}
}
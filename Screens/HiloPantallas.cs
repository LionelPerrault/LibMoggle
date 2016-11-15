using System.Collections.Generic;
using System;

namespace Moggle.Screens
{
	/// <summary>
	/// Representa una serie de invocaciones de <see cref="IScreen"/>
	/// </summary>
	public class HiloPantallas
	{
		readonly List<IScreen> _invocationStack;
		readonly List<ScreenStackOptions> _options;

		/// <summary>
		/// Devuelve el número de pantallas la pila
		/// </summary>
		/// <value>The count.</value>
		public int Count { get { return _invocationStack.Count; } }

		/// <summary>
		/// Devuelve la panalla correspondiente a un índice.
		/// Hace corresponer a la pantalla actual con el índice cero
		/// </summary>
		/// <param name="index">Índice de pantalla basado en cero.</param>
		public IScreen this [int index]
		{
			// No hay necesidad de exception check. Va a devolver el error de fuera de índice, y ése corresponde
			get	{ return _invocationStack [Count - index - 1]; }
		}

		ScreenStackOptions getOptionsFromNewIndex (int index)
		{
			return _options [Count - index - 1];
		}

		/// <summary>
		/// Devuelve la pantalla actual
		/// </summary>
		/// <value>The current.</value>
		public IScreen Current
		{ 
			get
			{
				if (Count == 0)
					throw new InvalidOperationException ("This thread has no screens.");
				return this [0]; 
			} 
		}

		/// <summary>
		/// Añade una nueva pantalla a la pila, haciendo ésta la pantalla actual
		/// </summary>
		/// <param name="scr">Nueva pantalla</param>
		/// <param name="opt">Opciones</param>
		public void Stack (IScreen scr, ScreenStackOptions opt)
		{
			_invocationStack.Add (scr);
			_options.Add (opt);
		}

		/// <summary>
		/// Añade una nueva pantalla a la pila, haciendo ésta la pantalla actual
		/// </summary>
		/// <param name="scr">Nueva pantalla</param>
		public void Stack (IScreen scr)
		{
			Stack (scr, ScreenStackOptions.Default);
		}

		/// <summary>
		/// Devuelve un nuevo <see cref="System.Collections.Generic.Stack{IScreen}"/> con las invocaciones de pantallas
		/// </summary>
		public Stack<IScreen> AsStack ()
		{
			return new Stack<IScreen> (_invocationStack);
		}

		/// <summary>
		/// </summary>
		public HiloPantallas ()
		{
			_invocationStack = new List<IScreen> ();
			_options = new List<ScreenStackOptions> ();
		}

		/// <summary>
		/// Options for each screen in the stack
		/// </summary>
		public struct ScreenStackOptions
		{
			/// <summary>
			/// Determina si la pantalla anterior debe dibujarse
			/// </summary>
			public bool DibujaBase;

			/// <summary>
			/// Determina si la pantalla anterior debe actualizarse
			/// </summary>
			public bool ActualizaBase;

			/// <summary>
			/// Devuelve el <see cref="ScreenStackOptions"/> predeterminado
			/// </summary>
			public static ScreenStackOptions Default;

			static ScreenStackOptions ()
			{
				Default = new ScreenStackOptions
				{
					DibujaBase = false 
				};
			}
		}
	}
}
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Moggle.Screens
{
	/// <summary>
	/// Representa una serie de invocaciones de <see cref="IScreen"/>
	/// </summary>
	public class ScreenThread : IUpdate
	{
		readonly List<IScreen> _invocationStack;
		readonly List<ScreenStackOptions> _options;

		/// <summary>
		/// Devuelve el número de pantallas la pila
		/// </summary>
		/// <value>The count.</value>
		public int Count { get { return _invocationStack.Count; } }

		/// <summary>
		/// Devuelve el color de fondo pedido por la pila
		/// </summary>
		public Color? BgColor
		{
			get
			{
				for (int i = Count - 1; i >= 0; i--)
				{
					var retClr = _invocationStack [i].BgColor;
					if (retClr != null)
						return retClr;
				}
				return null;
			}
		}

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
			var lastCurr = Count == 0 ? null : Current;
			_invocationStack.Add (scr);
			_options.Add (opt);
			if (lastCurr != null)
				LostPreference?.Invoke (this, lastCurr);
			GotPreference?.Invoke (this, Current);
			if (lastCurr != null)
				GotChild?.Invoke (this, lastCurr);
		}

		/// <summary>
		/// Devuelve la pantalla más próxima de un tipo dado
		/// </summary>
		public IScreen ClosestOfType <T> ()
			where T : IScreen
		{
			for (int i = 0; i < Count; i++)
			{
				var iterScr = this [i];
				if (iterScr is T)
					return iterScr;
			}
			throw new Exception ("There is not screen of the given type.");
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
		/// Termina el último elemento del stack
		/// </summary>
		public void TerminateLast ()
		{
			var lastCurr = Current;

			RemoveAt (Count - 1);

			Terminated?.Invoke (this, lastCurr);
			LostChild?.Invoke (this, Current);
			LostPreference?.Invoke (this, lastCurr);
			GotPreference?.Invoke (this, Current);

			lastCurr.Dispose ();
		}

		void RemoveAt (int i)
		{
			_invocationStack.RemoveAt (i);
			_options.RemoveAt (i);

		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Screens.ScreenThread"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Moggle.Screens.ScreenThread"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Moggle.Screens.ScreenThread"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="Moggle.Screens.ScreenThread"/> so
		/// the garbage collector can reclaim the memory that the <see cref="Moggle.Screens.ScreenThread"/> was occupying.</remarks>
		public void Dispose ()
		{
			for (int i = 0; i < Count; i++)
				this [i].Dispose ();
			_invocationStack.Clear ();
			_options.Clear ();
		}

		/// <summary>
		/// Actualizacińo lógica
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void Update (GameTime gameTime)
		{
			for (int i = Count - 1; i >= 0; i--)
			{
				_invocationStack [i].Update (gameTime, this);
				if (!_options [i].ActualizaBase)
					return;
			}
		}

		/// <summary>
		/// Dibuja
		/// </summary>
		public void Draw (GameTime gameTime)
		{
			for (int i = Count - 1; i >= 0; i--)
			{
				_invocationStack [i].Draw (gameTime);
				if (!_options [i].DibujaBase)
					return;
			}
		}

		#region Events

		/// <summary>
		/// Ocurre cuando un screen ya no es la actual.
		/// </summary>
		public event EventHandler<IScreen> LostPreference;
		/// <summary>
		/// Ocurre cuando un screen ahora es la actual
		/// </summary>
		public event EventHandler<IScreen> GotPreference;

		/// <summary>
		/// Ocurre cuando su child es terminado
		/// </summary>
		public event EventHandler<IScreen> LostChild;
		/// <summary>
		/// Ocurre cuando tiene un nuevo child
		/// </summary>
		public event EventHandler<IScreen> GotChild;

		/// <summary>
		/// Ocurre cuando es terminado
		/// </summary>
		public event EventHandler<IScreen> Terminated;

		#endregion

		/// <summary>
		/// Devuelve un nuevo <see cref="System.Collections.Generic.Stack{IScreen}"/> con las invocaciones de pantallas
		/// </summary>
		public Stack<IScreen> AsStack ()
		{
			return new Stack<IScreen> (_invocationStack);
		}

		/// <summary>
		/// </summary>
		public ScreenThread ()
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
					DibujaBase = false,
					ActualizaBase = false
				};
			}
		}
	}
}
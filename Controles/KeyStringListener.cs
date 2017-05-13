using Moggle.Comm;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System;

namespace Moggle.Controles
{
	/// <summary>
	/// Listens the keyboard and returns a string
	/// </summary>
	public class KeyStringListener : IReceptor<KeyboardEventArgs>, IDisposable
	{
		/// <summary>
		/// Gets or set the current string
		/// </summary>
		public string CurrentString {
			get { return outputBuilder.ToString (); }
			set { outputBuilder = new StringBuilder (value); }
		}

		StringBuilder outputBuilder { get; set; }

		KeyboardListener listener { get; }

		bool hasListener { get { return listener != null; } }

		/// <summary>
		/// Esta función establece el comportamiento de este control cuando el jugador presiona una tecla dada.
		/// </summary>
		/// <param name="key">Tecla presionada por el usuario.</param>
		bool IReceptor<KeyboardEventArgs>.RecibirSeñal (KeyboardEventArgs key)
		{
			return RecibirSeñal (key);
		}

		/// <summary>
		/// Rebice señal del teclado
		/// </summary>
		/// <returns><c>true</c>, si la señal fue aceptada, <c>false</c> otherwise.</returns>
		/// <param name="key">Señal tecla</param>
		public virtual bool RecibirSeñal (KeyboardEventArgs key)
		{
			if (key.Key == Keys.Back) {
				if (outputBuilder.Length > 0)
					outputBuilder.Remove (outputBuilder.Length - 1, 1);
				return true;
			}

			outputBuilder.Append (useMayús (key) ? char.ToUpper (key.Character.Value) : char.ToLower (key.Character.Value));
			return true;
		}

		static bool useMayús (KeyboardEventArgs key)
		{
			return key.Modifiers.HasFlag (KeyboardModifiers.Shift);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.KeyStringListener"/> object.
		/// </summary>
		public void Dispose ()
		{
			listener.KeyTyped -= event_key_type;
		}

		void event_key_type (object sender, KeyboardEventArgs e)
		{
			RecibirSeñal (e);
		}

		/// <summary>
		/// </summary>
		public KeyStringListener ()
		{
			outputBuilder = new StringBuilder ();
		}

		/// <summary>
		/// </summary>
		/// <param name="listen">Keyboard listener</param>
		public KeyStringListener (KeyboardListener listen)
		{
			if (listen == null)
				throw new ArgumentNullException ("listen");
			outputBuilder = new StringBuilder ();
			listener = listen;

			listen.KeyTyped += event_key_type;
		}
	}
}
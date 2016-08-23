using System;
using OpenTK.Input;

namespace Moggle.IO
{
	/// <summary>
	/// Un controlador que maneja entrada: Teclado y ratón
	/// Es mejor usarlo por medio de eventos, que capturando el estado actual.
	/// </summary>
	[Obsolete]
	public static class InputManager
	{
		/// <summary>
		/// Devuelve el estado actual del teclado.
		/// </summary>
		public static KeyboardState EstadoActualTeclado { get; private set; }

		/// <summary>
		/// Devuelve el estado tel teclado del tick anterior.
		/// </summary>
		public static KeyboardState EstadoAnteriorTeclado { get; private set; }

		/// <summary>
		/// Devuelve el estado actual del ratón
		/// </summary>
		/// <value>The estado actual mouse.</value>
		public static MouseState EstadoActualMouse { get; private set; }

		/// <summary>
		/// Devuelve el estado anterior del ratón
		/// </summary>
		/// <value>The estado anterior mouse.</value>
		public static MouseState EstadoAnteriorMouse { get; private set; }

		/// <summary>
		/// Actualiza el estado de los dispositivos.
		/// </summary>
		/// <remarks>
		/// No es necesario ejecutar esta función.
		/// El Update de <see cref="Moggle.Game"/> ya lo ejecuta automáticamente.
		/// </remarks>
		/// <param name="time">Tiempo transcurrido desde la última actualización</param>
		public static void  Update (TimeSpan time)
		{
			EstadoAnteriorTeclado = EstadoActualTeclado;
			EstadoActualTeclado = Keyboard.GetState ();

			EstadoAnteriorMouse = EstadoActualMouse;
			EstadoActualMouse = Mouse.GetState ();

			foreach (Key x in Enum.GetValues(typeof(Key)))
			{
				if (FuePresionado (x))
				{
					onSerPresionado (x);
					break;
				}
			}

			if (PermitirRepeticiones)
				revisar_repetición (time);
		}

		#region Teclado

		/// <summary>
		/// Tiempo de espera para empezar repetición de teclas
		/// </summary>
		public static TimeSpan TiempoRepeticiónInicial = TimeSpan.FromMilliseconds (300);
		/// <summary>
		/// Tiempo entre repeticiones de teclas cuando está en modo repetir.
		/// </summary>
		public static TimeSpan TiempoReiteración = TimeSpan.FromMilliseconds (80);

		static TimeSpan contadorActual { get; set; }

		/// <summary>
		/// Devuelve la tecla que está en modo repetir.
		/// </summary>
		/// <value>The tecla repitiendo.</value>
		public static Key TeclaRepitiendo { get; private set; }

		static bool estáReiterando { get; set; }

		/// <summary>
		/// Devuelve un valor que determina si permite entrar a modo repetición.
		/// </summary>
		public static bool PermitirRepeticiones
		{
			get
			{
				return TiempoRepeticiónInicial != TimeSpan.Zero;
			}
		}

		static void revisar_repetición (TimeSpan time)
		{
			if (EstáPresionado (TeclaRepitiendo))
			{
				contadorActual += time;
				if (estáReiterando)
				{
					if (contadorActual > TiempoReiteración)
					{
						contadorActual = TimeSpan.Zero;
						AlSerActivado?.Invoke (TeclaRepitiendo);
					}
				}
				else
				{
					if (contadorActual > TiempoRepeticiónInicial)
					{
						contadorActual = TimeSpan.Zero;
						estáReiterando = true;
					}
				}
			}
			else
				estáReiterando = false;
		}

		static void onSerPresionado (Key key)
		{
			TeclaRepitiendo = key;
			contadorActual = TimeSpan.Zero;
			AlSerPresionado?.Invoke (key);
			AlSerActivado?.Invoke (key);
		}

		/// <summary>
		/// evisa si una tecla está en este momento presionada
		/// </summary>
		public static bool EstáPresionado (Key tecla)
		{
			return EstadoActualTeclado.IsKeyDown (tecla);
		}

		/// <summary>
		/// Resiva si una tecla fue presionada en este instante (módulo Update ())
		/// </summary>
		public static bool FuePresionado (Key tecla)
		{
			return EstáPresionado (tecla) && !EstadoAnteriorTeclado.IsKeyDown (tecla);
		}

		/// <summary>
		/// Se ejecuta cuando el botón es presionado
		/// </summary>
		public static event Action<Key> AlSerPresionado;

		/// <summary>
		/// Ocurre cuando un botón es presionado o repetido
		/// </summary>
		public static event Action<Key> AlSerActivado;

		#endregion

		#region Mouse

		/// <summary>
		/// Determina si un botón del ratón está presionado
		/// </summary>
		/// <param name="botón">Botón.</param>
		public static bool EstáPresionado (MouseButton botón)
		{
			return EstadoActualMouse.IsButtonDown (botón);
		}

		/// <summary>
		/// Determina si un botón del ratón acaba de ser presionado.
		/// </summary>
		/// <param name="botón">Botón.</param>
		public static bool FuePresionado (MouseButton botón)
		{
			return EstáPresionado (botón) && !EstadoAnteriorMouse.IsButtonDown (botón);
		}

		#endregion
	}
}
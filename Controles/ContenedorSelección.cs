using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Moggle.Comm;

namespace Moggle.Controles
{
	/// <summary>
	/// Un contenedor que puede seleccionar un objeto usando el teclado y ratón
	/// </summary>
	public class ContenedorSelección<T> : Contenedor<T>, IReceptorTeclado
		where T : IDibujable
	{
		#region Selección y enfoque

		int _focusIndex;

		/// <summary>
		/// Devuelve o establece el índice (zero-based) del objeto seleccionado
		/// </summary>
		public int FocusedIndex
		{
			get
			{
				return _focusIndex;
			}
			set
			{
				_focusIndex = Math.Min (Math.Max (0, value), Count - 1);
			}
		}

		/// <summary>
		/// Devuelve el objeto enfocado
		/// </summary>
		/// <value>The focused item.</value>
		public T FocusedItem { get { return Objetos [FocusedIndex]; } }

		/// <summary>
		/// Devuelve o establece el color que se usará para resaltar objeto enfocado
		/// </summary>
		/// <value>The color of the selected.</value>
		public Color FocusedColor { get; set; }

		/// <summary>
		/// Devuelve la selección
		/// </summary>
		public SelectionManager<T> Selection { get; }

		/// <summary>
		/// Gets or set a value indicating the color of the selection
		/// </summary>
		public Color SelectionColor { get; set; }

		#endregion

		#region Dibujo

		/// <summary>
		/// Dibuja el objeto de un índice dado
		/// </summary>
		/// <param name="bat">Sprite batch</param>
		/// <param name="index">Índice del objeto a dibujar</param>
		protected override void DrawObject (SpriteBatch bat, int index)
		{
			if (index == FocusedIndex)
			{
				var rect = CalcularPosición (index);
				bat.Draw (TexturaFondo, rect, FocusedColor);
			}
			if (Selection.IsSelected (Objetos [index]))
			{
				var rect = CalcularPosición (index);
				bat.Draw (TexturaFondo, rect, SelectionColor);
			}
			base.DrawObject (bat, index);
		}

		#endregion

		#region Teclas y teclado

		/// <summary>
		/// Tecla enfoque arriba
		/// </summary>
		public Keys UpKey = Keys.Up;
		/// <summary>
		/// Tecla enfoque abajo
		/// </summary>
		public Keys DownKey = Keys.Down;
		/// <summary>
		/// Tecla enfoque izquierda
		/// </summary>
		public Keys LeftKey = Keys.Left;
		/// <summary>
		/// Tecla enfoque derecha
		/// </summary>
		public Keys RightKey = Keys.Right;
		/// <summary>
		/// Tecla activación
		/// </summary>
		public Keys EnterKey = Keys.Enter;
		/// <summary>
		/// Tecla para alternar selección
		/// </summary>
		public Keys SelectKey = Keys.Space;

		/// <summary>
		/// Devuelve o establece el tiempo de repetición de tecla presionada
		/// </summary>
		/// <value>The initial cooldown.</value>
		public TimeSpan InitialCooldown { get; set; }

		TimeSpan cooldown;

		bool isCoolingDown { get { return cooldown > TimeSpan.Zero; } }

		bool IReceptorTeclado.RecibirSeñal (MonoGame.Extended.InputListeners.KeyboardEventArgs key)
		{
			if (isCoolingDown)
				return false;
			cooldown = InitialCooldown;
			Debug.WriteLine (key.Key);
			if (key.Key == UpKey)
			{
				if (TipoOrden == TipoOrdenEnum.ColumnaPrimero)
					FocusedIndex--;
				else
					FocusedIndex -= Columnas;
				return true;
			}
			if (key.Key == DownKey)
			{
				if (TipoOrden == TipoOrdenEnum.ColumnaPrimero)
					FocusedIndex++;
				else
					FocusedIndex += Columnas;
				return true;
			}
			if (key.Key == LeftKey)
			{
				if (TipoOrden == TipoOrdenEnum.FilaPrimero)
					FocusedIndex--;
				else
					FocusedIndex -= Columnas;
				return true;
			}
			if (key.Key == RightKey)
			{
				if (TipoOrden == TipoOrdenEnum.FilaPrimero)
					FocusedIndex++;
				else
					FocusedIndex += Columnas;
				return true;
			}
			if (key.Key == EnterKey)
			{
				Activar (key);
				return true;
			}
			if (SelectionEnabled && key.Key == SelectKey)
			{
				Selection.ToggleSelection (FocusedItem);
			}
			return false;
		}

		#endregion

		#region Comportamiento

		/// <summary>
		/// Ejecuta el evento <see cref="Activado"/>
		/// </summary>
		/// <param name="args">Argumentos del evento</param>
		protected void Activar (EventArgs args)
		{
			Debug.WriteLine ("Contenedor activado, con índice {0}", FocusedIndex);
			Activado?.Invoke (this, args);
		}

		/// <summary>
		/// Update lógico
		/// </summary>
		public override void Update (GameTime gameTime)
		{
			if (isCoolingDown)
			{
				cooldown -= gameTime.ElapsedGameTime;
				Debug.WriteLineIf (!isCoolingDown, "Keyboard cooldown over.");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating if selection system is enabled
		/// </summary>
		public bool SelectionEnabled { get; set; }

		#endregion

		#region Eventos

		/// <summary>
		/// Ocurre cuando un control es activado
		/// </summary>
		public event EventHandler Activado;

		#endregion

		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="cont">Container</param>
		public ContenedorSelección (IComponentContainerComponent<IControl> cont)
			: base (cont)
		{
			FocusedColor = Color.Yellow * 0.7f;
			SelectionColor = Color.Red * 0.65f;
			InitialCooldown = TimeSpan.FromMilliseconds (100);
			Selection = new SelectionManager<T> (Objetos);
		}

		#endregion
	}
}
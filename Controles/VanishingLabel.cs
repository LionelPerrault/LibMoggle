﻿using System;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;

namespace Moggle.Controles
{
	/// <summary>
	/// Vanishing string.
	/// </summary>
	public class VanishingLabel : DSBC
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VanishingLabel"/> class.
		/// </summary>
		/// <param name="screen">Screen.</param>
		/// <param name="texto">Texto.</param>
		/// <param name="duración">Duración.</param>
		public VanishingLabel (IScreen screen, string texto, TimeSpan duración)
			: base (screen)
		{
			_texto = texto;
			Restante = duración;
			TiempoInicial = duración;
			ColorFinal = Color.Transparent;
			Velocidad = new Vector2 (0, -20);
		}

		BitmapFont Font;
		string _texto;
		Point topLeft;

		string _fontName;

		/// <summary>
		/// Gets or sets the name of the font.
		/// </summary>
		public string FontName
		{
			get
			{
				return _fontName;
			}
			set
			{
				if (Font != null)
					throw new InvalidOperationException ("FontName cannot be changed after initialization.");
				_fontName = value;
			}
		}

		/// <summary>
		/// Velocidad de este control.
		/// </summary>
		public Vector2 Velocidad { get; set; }

		/// <summary>
		/// Devuelve el tiempo restante para desaparecer y liberarse
		/// </summary>
		/// <value>The restante.</value>
		public TimeSpan Restante { get; private set; }

		/// <summary>
		/// Gets the total duration of the label
		/// </summary>
		/// <value>The tiempo inicial.</value>
		public TimeSpan TiempoInicial { get; }

		/// <summary>
		/// Gets or sets the text of the label
		/// </summary>
		/// <value>The texto.</value>
		public string Texto
		{
			get
			{
				return _texto;
			}
			set
			{
				_texto = value;
				calcularBounds ();
			}
		}

		/// <summary>
		/// Recalculates the value of <see cref="Bounds"/> field
		/// </summary>
		void calcularBounds ()
		{
			if (Font != null)
				Bounds = Font.GetStringRectangle (Texto, TopLeft);
		}

		/// <summary>
		/// Gets or sets the top left corner's coordinates
		/// </summary>
		/// <value>The top left.</value>
		public Point TopLeft
		{
			get
			{
				return topLeft;
			}
			set
			{
				topLeft = value;
				calcularBounds ();
			}
		}

		/// <summary>
		/// Gets or sets the center of the label
		/// </summary>
		/// <value>The centro.</value>
		public Point Centro
		{
			get
			{
				return Bounds.Center;
			}
			set
			{
				var altura = Bounds.Height;
				var grosor = Bounds.Width;
				topLeft = value - new Point (grosor / 2, altura / 2);
				calcularBounds ();
			}
		}

		/// <summary>
		/// Gets the bounds of the label
		/// </summary>
		public Rectangle Bounds { get; private set; }

		/// <summary>
		/// Gets the bounds of the label
		/// </summary>
		protected override Rectangle GetBounds ()
		{
			return Bounds;
		}

		/// <summary>
		/// Gets or sets the starting color
		/// </summary>
		public Color ColorInicial { get; set; }

		/// <summary>
		/// Gets or sets the end color
		/// </summary>
		/// <value>The color final.</value>
		public Color ColorFinal { get; set; }

		/// <summary>
		/// Gets the current color
		/// </summary>
		public Color ColorActual
		{
			get
			{
				var t = escalarColor;
				var ret = new Color (
					          (int)(ColorInicial.R * t + ColorFinal.R * (1 - t)),
					          (int)(ColorInicial.G * t + ColorFinal.G * (1 - t)),
					          (int)(ColorInicial.B * t + ColorFinal.B * (1 - t)),
					          (int)(ColorInicial.A * t + ColorFinal.A * (1 - t))
				          );
				return ret;
			}
		}



		/// <summary>
		/// Devuelve valor en [0, 1] depende de dónde en el tiempo es el estado actual de este control (lineal)
		/// 0 si está en el punto de terminación
		/// 1 si está en el punto de inicio
		/// </summary>
		float escalarColor
		{
			get
			{
				return (float)Restante.Ticks / TiempoInicial.Ticks;
			}
		}

		/// <summary>
		/// Dibuja el control.
		/// </summary>
		protected override void Draw ()
		{
			Screen.Batch.DrawString (Font, Texto, TopLeft.ToVector2 (), ColorActual);
		}

		/// <summary>
		/// Loads the content using a given manager
		/// </summary>
		protected override void LoadContent (Microsoft.Xna.Framework.Content.ContentManager manager)
		{
			Font = manager.Load<BitmapFont> (FontName);
		}

		/// <summary>
		/// Update lógico
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (GameTime gameTime)
		{
			Restante -= gameTime.ElapsedGameTime;
			if (Restante < TimeSpan.Zero)
				OnTerminar ();
			else
				TopLeft += (Velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds).ToPoint ();
		}

		/// <summary>
		/// terminates the control
		/// </summary>
		protected void OnTerminar ()
		{
			Finished?.Invoke (this, EventArgs.Empty);
			Finished = null;
			Screen.RemoveComponent (this);
			Dispose ();
		}

		/// <summary>
		/// Se ejecuta antes del ciclo, pero después de saber un poco sobre los controladores.
		/// No invoca LoadContent por lo que es seguro agregar componentes
		/// </summary>
		protected override void Initialize ()
		{
			base.Initialize ();
			calcularBounds ();
		}

		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="VanishingLabel"/>.
		/// </summary>
		public override string ToString ()
		{
			return string.Format (
				"[VanishingString: _texto={0}, Restante={1}, TiempoInicial={2}]",
				_texto,
				Restante,
				TiempoInicial);
		}

		/// <summary>
		/// Ocurrs when the label is about to be disposed
		/// </summary>
		public event EventHandler Finished;
	}
}
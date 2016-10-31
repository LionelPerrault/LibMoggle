using System.Linq;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Moggle
{
	/// <summary>
	/// Provee métodos para cargar y descargar contenido en una forma organizada
	/// </summary>
	public class BibliotecaContenido
	{
		class EntradaContenido
		{
			public object Contenido;

			public readonly string NombreInvocación;

			public readonly string NombreArchivo;

			public List<string> Tags;

			public EntradaContenido (string nombreInvocación,
			                         string nombreArchivo)
			{
				NombreInvocación = nombreInvocación;
				NombreArchivo = nombreArchivo;
				Tags = new List<string> ();
			}
		}

		/// <summary>
		/// Gets the content manager
		/// </summary>
		/// <value>The manager.</value>
		public ContentManager Manager { get; }

		/// <summary>
		/// Diccionario nombre-
		/// </summary>
		readonly List <EntradaContenido> _contenido;

		/// <summary>
		/// Agrega contenido al sistema
		/// </summary>
		/// <param name="name">Nombre del archivo de contenido equiv nombre del contenido</param>
		public void AddContent (string name)
		{
			AddContent (name, name);
		}

		/// <summary>
		/// Agrega contenido al sistema
		/// </summary>
		/// <param name="name">Nombre único del contenido</param>
		/// <param name="file">Nombre del archivo del contenido</param>
		protected void AddContent (string name,
		                           string file)
		{
			_contenido.Add (new EntradaContenido (name, file));
		}

		EntradaContenido PrimerContenidoCargadoONulo (string file)
		{
			foreach (var z in _contenido)
				if (z.NombreArchivo == file && z.Contenido != null)
					return z;
			return null;
		}

		void Load (EntradaContenido entry)
		{
			var loadedContent = PrimerContenidoCargadoONulo (entry.NombreArchivo);
			if (loadedContent != null)
				entry.Contenido = loadedContent.Contenido;
			else
			{
				Manager.Load<object> (entry.NombreArchivo);
			}
		}

		/// <summary>
		/// Carga todo el contenido agregado
		/// </summary>
		public void Load ()
		{
			foreach (var x in _contenido)
				Load (x);
		}

		/// <summary>
		/// Obtiene el contenido
		/// </summary>
		/// <returns>The content.</returns>
		/// <param name="nombre">Nombre único del contenido</param>
		/// <typeparam name="T">Tipo de contenido</typeparam>
		public T GetContent<T> (string nombre)
		where T : class
		{
			var content = _contenido.First (z => z.NombreInvocación == nombre);
			return content.Contenido as T;
		}

		/// <summary>
		/// Obtiene el contenido
		/// </summary>
		/// <returns>The content.</returns>
		/// <param name="nombre">Nombre único del contenido</param>
		public object GetContent (string nombre)
		{
			var content = _contenido.First (z => z.NombreInvocación == nombre);
			return content.Contenido;
		}

		public void ClearAll ()
		{
			_contenido.Clear ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Moggle.BibliotecaContenido"/> class.*/
		/// </summary>
		/// <param name="manager">Manager.</param>
		internal BibliotecaContenido (ContentManager manager)
		{
			Manager = manager;
			_contenido = new List<EntradaContenido> ();
		}
	}
}
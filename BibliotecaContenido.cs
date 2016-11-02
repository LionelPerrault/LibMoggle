using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

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
		readonly Dictionary <string, EntradaContenido> _contenido;

		/// <summary>
		/// Agrega contenido al sistema
		/// </summary>
		/// <param name="name">Nombre del archivo de contenido equiv nombre del contenido</param>
		public void AddContent (string name)
		{
			tryAddContent (name, name);
		}

		/// <summary>
		/// Agrega contenido al sistema
		/// </summary>
		/// <param name="name">Nombre único del contenido</param>
		/// <param name="file">Nombre del archivo del contenido</param>
		EntradaContenido tryAddContent (string name,
		                                string file)
		{
			EntradaContenido ret;
			if (_contenido.TryGetValue (name, out ret))
				return ret;
			ret = new EntradaContenido (name, file);
			_contenido.Add (name, ret);
			return ret;
		}

		/// <summary>
		/// Agrega contenido a la biblioteca
		/// </summary>
		/// <param name="name">Nombre único del contenido</param>
		/// <param name="content">Objeto de contenido</param>
		public void AddContent (string name, object content)
		{
			var newCont = tryAddContent (name, null);
			newCont.Contenido = content;
		}

		EntradaContenido PrimerContenidoCargadoONulo (string file)
		{
			foreach (var z in _contenido.Values)
				if (z.NombreArchivo == file && z.Contenido != null)
					return z;
			return null;
		}

		void Load (EntradaContenido entry)
		{
			// No cargar contenido si ya está cargado
			if (entry.Contenido != null)
				return;
			
			var loadedContent = PrimerContenidoCargadoONulo (entry.NombreArchivo);
			entry.Contenido = loadedContent != null ? 
				loadedContent.Contenido : 
				Manager.Load<object> (entry.NombreArchivo);
		}

		/// <summary>
		/// Carga todo el contenido agregado
		/// </summary>
		public void Load ()
		{
			foreach (var x in _contenido)
				Load (x.Value);
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
			var content = _contenido [nombre];
			return content.Contenido as T;
		}

		/// <summary>
		/// Obtiene el contenido
		/// </summary>
		/// <returns>The content.</returns>
		/// <param name="nombre">Nombre único del contenido</param>
		public object GetContent (string nombre)
		{
			var content = _contenido [nombre];
			return content.Contenido;
		}

		/// <summary>
		/// Elimina todo contenido de esta biblioteca, pero no lo libera de memoria
		/// </summary>
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
			_contenido = new Dictionary<string, EntradaContenido> ();
		}
	}
}
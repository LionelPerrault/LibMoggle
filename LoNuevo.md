Lo nuevo
========
0.12
----
  + `BibliotecaContenido` queda eliminado. Se usa la versión actual de `ContentManager`.
  + `EtiquetaMultiLínea` construye una nueva textura de fondo cada vez que su texto o sus campos son cambiados.
  + Agregar evento de selección cambiada a `ContenedorSelección`.
  + Ya no tira exception cuando `EtiquetaMultiLínea` ya no tira `Exception` cuando `Texto` es `null`, en lugar se trata como `String.Empty`.
  + Agregar el control `VanishingLabel`, que es un texto que se autodestruye después de un tiempo dado.
  + `SBC` y todos sus implementaciones evitan múltiles invocaciones a su inicializador. Además marca como protegido este método `Initialize`.
0.11.1
----
  + Correcciones a `MultiEtiqueta`.
0.11
----
  + Se agregan opciones constantes en `ScreenStackOptions`.
  + Nuevo sistema de invocación de `IScreen` en serie; con respuesta en un evento.
  + Nuevo control `EtiquetaMultiLínea` es una etiqueta que corta líneas cuando el texto se sale de un grosor máximo.

0.10
----
  + Nuevo sistema de hilos de pantallas
    + La clase `ScreenThread` Maneja una línea de invocaciones de pantallas (hace a `ScreenDial` obsoleto y eliminado) que recuerda y maneja su línea de llamadas.
    + La clase `ScreenThreadManager` maneja un conjunto de `ScreenThread` mediante una lista. No puede construirse, así que sólo existe comoun miembro de `Game`. Decide quén es el `ScreenThread` activo (sólo uno), maneja los `Draw`, `Update` y recepcion de señal de sus pantallas, mandando a ellos cuál `ScreenThread` es quien lo invoco (esto hace que un `IScreen` pueda pertenecer a distintos `ScreenThread`.
  + DSBC.Draw (y por lo tanto sus hijos) no requieren argumento `GameTime`.

0.9
---
  + Clase `Raton` Tiene propiedad `Offset`, devuelve o establece el desface entre la posición del cursor del ratón y la posición de la textura (topleft)
  + Corregir la visibilidad inical del ratón
  + `AddContent` y `InitializeContent` no tiene `BibliotecaContenido` como argumento
  + `IComponent` No hereda `IDisposable`, pero `DSBC` sí lo implementa
  + Eliminar la propiedad `Moggle.Game.Device` era repetida
  + Las clases de emisión y recepción ahora son templates, de único argumento el tipo de señal que manda/recibe. p.e. `EmisorTeclado` es ahora `Emisor<KeyboardEventArgs>`
  + Screen tiene una propiedad `MouseObserver MouseObserver` que vigila y genera eventos de cuándo el ratón interactúa con controles (más bien con `ISpaceable`) determinados
  + Se actualizó *MonoGame.Extended*

En 0.8.1
--------
+ Agregar Métodos de carga inmediata de contenido

En 0.8
------
+ ContenedorSelección: corregir bugs gráficos
+ Se agrega clase BibliotecaContenido; maneja contenido que se carga de archivos. Cambia bastante el esquema de inicialización, así que puede perder compatibilidad con versiones anteriores.
+ Se agrega una clase de texturas definibles: Sólido, Alternado, Contorno

En 0.7
------
+ Selección en ContenedorSelección es opcional
+ Agregar espacio interno en Contenedor
+ No construir nuevo Batch en cada iteración de Screen.Draw; almacenar el mismo u usarlo de sólo-lectura

En 0.6
------
+ Evento de cambio de selección en SelectorManager

En 0.5
------
+ Lista, como IList, queda totalmente implementado
+ IComponent no requiere devolver un objeto contenedor, en cambio IControl sí; y IComponentContainerComponent requiere un IControl como dependencia SBC ahora implemente IControl en lugar de IComponent
+ IDibujable es un objeto que puede ser dibujado dado un rectángulo salida y un SpriteBatch
+ IActivable ahora tiene una función Activar
+ Contenedor es un control que acomoda IDibujable's en una región de un tamaño igual y modificable. Además intectactúa con el mouse si éstos son IActivable
+ FlyingSprite representa un IDibujable con una textura fina dada
+ ContenedorImg es un Contenedor donde automáticamente agrega FlyingSprite's
+ ContenedorSelección es un contenedor en la que se pueden seleccionar elementos con el teclado mediante un SelectionManager
+ SelectionManager provee métodos para manejar selección de un subconjunto de una colección


En 0.4
------
+ KeyStringListener:
	Un control que, con ayuda de un KeyboardListener convierte las teclas presionadas por el usuario y las convierte en un 'string'
* EntradaTexto: ahora usa KeyStringListener
* IComponent:
	LoadContent ahora requiere el ContentManager como argumento.
	Screen.LoadContent usa el ContentManager que le provee Juego

En 0.6
======
+ Evento de cambio de selección en SelectorManager

En 0.5
======
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
======
+ KeyStringListener:
	Un control que, con ayuda de un KeyboardListener convierte las teclas presionadas por el usuario y las convierte en un 'string'
* EntradaTexto: ahora usa KeyStringListener
* IComponent:
	LoadContent ahora requiere el ContentManager como argumento.
	Screen.LoadContent usa el ContentManager que le provee Juego

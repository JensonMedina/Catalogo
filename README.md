# Catalogo
Un catalogo de articulos

Este es el trabajo practico final del curso de c# nivel 2. Es un catalogo de articulos con conexion a base de datos en sql server, se puede agregar, modificar, eliminar y ver un articulo.
Hice una barra de menu en la que se deberia poder acceder a otras partes de la aplicacion, sin embargo lo hice solo en forma de ejemplo, y el boton de inicio y de contactos no llevan a ningun lado.
Hay un filtro rapido que filta por nombre, catgoria y marca, el filtro funciona una vez que se ingresan 3 o mas carcateres.
Tambien hay un filtro avanzado que filtra por precio, nombre, categoria y marca, este filtro hace consultas directamente hacia la base de datos. El filtro no funciona
si no se cargan todos los campos correspondientes. Al lado del filtro avanzado hay un boton de reiniciar el cual carga todos los articulos de nuevo ya que al terminar de usar el filtro avanzado la grilla
no se reinicia automaticamente, esto es asi para evitar incidentes con el filtro.
las ventanas de agregar y modificar son la misma ventana, pero se comportan de manera diferente segun se este agregando o modificando un articulo, en esta ventana se muestra una vista previa
del articulo que se esta cargando, esta la posibilidad de agregar una imagen desde internet mediante el url o tambien de manera local, si se usa la opcion de elegir una imagen local y se elige una imagen ya existente no la va a guardar, sino que va a usar la imagen que ya existe.
suponiendo el caso de que tengo 100 articulos que son iguales, no voy a tener que cargar 100 imagenes diferentes sino que se puede usar la misma imagen y solo se va a guardar una sola vez.
Si se esta modificando un articulo, una vez que se guarda el articulo modificado te devuelve a la ventana principal, sin embargo si se esta agregando un articulo nuevo una vez que se guarda el articulo
no te devuelve a la ventana principal, esto es asi suponiendo el caso de que se quieran agregar varios articulos a la vez.
Al eliminar un articulo se elimina directamente de la base de datos, es por eso que antes de eliminarlo la aplicacion te pregunta si esta seguro de querer eliminar el articulo.
Cada articulo cuenta con un codigo, nombre, descripcion, imagen, categoria, marca y precio, el precio se muestra en pesos argentinos y en la ventana principal esta la opcion
de seleccionar un articulo y verlo.

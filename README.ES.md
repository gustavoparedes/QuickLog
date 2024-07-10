# QuickLog


![FiltrosBásicos4](https://github.com/gustavoparedes/QuickLog/assets/61228478/affe8b14-62a3-480d-b86a-0bcea6698e0b)


# Descargar version compilada

Puede descargarla [aqui](https://github.com/gustavoparedes/QuickLog/releases/download/v0.1/Compiled.Version.QuickLog.v0.1.rar) 



Quick Log es una herramienta simple para visualizar logs de Windows en formato EVTX, organizada según este trabajo: 
https://cybersecuritynews.com/windows-event-log-analysis/ y pensada para los cursos de forense digital con herramientas 
de código abierto dictados en Internet Solutions S.A.S, Bogotá, Colombia. Requiere Windows 10 de 64 bits y una resolución de 1920x1080. 

Los logs se organizan en workspaces.


# Workspace

Un workspace es un "contenedor" de logs que puede contener uno o más archivos .evtx de una o varias máquinas que ejecutan Windows. Antes de poder empezar a visualizar los logs, debes crear un nuevo workspace o abrir uno previamente creado. Por defecto, un workspace recién creado no contiene archivos de logs; debes agregar logs después de crear o abrir el workspace. Siempre se pueden agregar logs adicionales. Un workspace también puede ser abierto para continuar revisando logs y puede ser cerrado cuando sea necesario.

# Adquisición de Logs

Durante la adquisición de logs, se leen los logs de Windows y los campos más relevantes se almacenan en una base de datos SQLite. Una vez que se completa el proceso de lectura y almacenamiento, los archivos de logs originales ya no son necesarios, ya que se utilizará la base de datos. Cada entrada de log es un registro en la base de datos dentro de la tabla de logs, y cada registro contiene los siguientes campos con nombres descriptivos:

TimeCreated, UserID, EventID, Machine, Level, LogName, EventMessage, EventMessageXML y ActivityID.

**TimeCreated:**

La hora en la que se creó el evento, almacenada en UTC. Al procesar los logs, la hora se ajustará a la zona horaria de la máquina local. Ten esto en cuenta y asegúrate de ajustar a la zona horaria correcta extrayéndola del registro. Usa la zona horaria de la evidencia para establecer la hora real. Por conveniencia, podrías, por ejemplo, cambiar la zona horaria de la máquina para que coincida con la de la evidencia durante el procesamiento de los logs.

**UserID:**

El descriptor de seguridad del usuario cuyo contexto se utiliza para publicar el evento. Para información detallada sobre este tema, consulta aquí:
[https://learn.microsoft.com/en-us/previous-versions/windows/it-pro/windows-server-2012-r2-and-2012/dn743661(v=ws.11)](https://learn.microsoft.com/en-us/previous-versions/windows/it-pro/windows-server-2012-r2-and-2012/dn743661(v=ws.11))

**EventID:**

El identificador del evento.

**Machine:**

El nombre de la máquina donde se registró este evento.

**Level:**

El nivel del evento. El nivel indica la gravedad del evento.

**LogName:**

El nombre del registro de eventos donde se registra este evento.

**EventMessage:**

El mensaje del evento en la configuración regional actual.

**EventMessageXML:**

Representación XML del evento. Todas las propiedades del evento están representadas en el XML del evento.

**ActivityID:**

Un identificador único global (GUID) para la actividad en curso con la que se asocia el evento.

# La Interfaz:


![Interfaz](https://github.com/gustavoparedes/QuickLog/assets/61228478/c2bec1e0-254a-44b4-9fe3-163e60e94938)

# 1. Adquisición y Filtros Básicos:

Los primeros tres elementos son para:

- Previsualizar
- Adquirir uno o más archivos de logs
- Adquirir todos los archivos .evtx dentro de una carpeta o ruta, permitiendo agregar múltiples logs de varias máquinas organizadas en subcarpetas dentro de una carpeta principal, por ejemplo.

![Filtros Básicos](https://github.com/gustavoparedes/QuickLog/assets/61228478/3cb35628-7da3-4189-98fd-27b4a405e85d)

A partir del cuarto elemento, los eventos se categorizan en áreas de interés basadas en el trabajo mostrado [aquí](https://cybersecuritynews.com/windows-event-log-analysis/) con créditos de autor a [Forward Defence](https://forwarddefense.com/).

![Filtros Básicos1](https://github.com/gustavoparedes/QuickLog/assets/61228478/92e4ae0f-6f00-449d-86af-9017ac03bfb9)
![Filtros Básicos2](https://github.com/gustavoparedes/QuickLog/assets/61228478/6a929ce0-ad10-4ab7-a1b2-6d6dfee3c1af)

# 2. Tabla de Logs:

Muestra los logs según la categoría seleccionada en los Filtros Básicos.

![Tabla1](https://github.com/gustavoparedes/QuickLog/assets/61228478/a947b4bd-3c81-40ed-a756-6c757dc6609c)

Puedes navegar de celda en celda, y el contenido de cada celda se mostrará en el cuadro de texto a medida que te muevas.

Visualización de EventMessage

![Tabla2](https://github.com/gustavoparedes/QuickLog/assets/61228478/1f31e4f9-57f0-4b17-9d04-e6314e40a9b7)

Visualización de EventMessageXML

![Tabla3](https://github.com/gustavoparedes/QuickLog/assets/61228478/d7a79259-4931-4b17-8dc9-5376a5c565ba)

# 3. Cuadro de Texto:

Muestra el contenido de la celda seleccionada usando las flechas del teclado o el mouse. Permite ver los resultados de búsqueda resaltados y leer cómodamente el contenido de los logs.

![Tabla4](https://github.com/gustavoparedes/QuickLog/assets/61228478/873c2d83-3b91-4a2e-8d88-00061a5e8c5d)

# 4. Etiquetas y Comentarios:

Opciones para crear, borrar y asignar etiquetas, así como para crear, actualizar y borrar comentarios.

![Labels and comments](https://github.com/gustavoparedes/QuickLog/assets/61228478/d3ef32fc-600c-42f7-81a3-238cf8f2a3ab)

Antes de poder usar etiquetas, debe crearlas usando el Gestor de Etiquetas.

![Label Manager1](https://github.com/gustavoparedes/QuickLog/assets/61228478/14312224-cf85-46ad-96b8-c46a94a199a6)

Ahora, simplemente haga clic en la celda en blanco en la columna "Nombre".

![Label Manager2](https://github.com/gustavoparedes/QuickLog/assets/61228478/91fa5f2e-2f75-4e07-a10a-ad88d930a84b)

Seleccione un color en la columna "Color".

![Label Manager3](https://github.com/gustavoparedes/QuickLog/assets/61228478/ad6d52b5-6646-433e-ae1a-2e9ed5f3ac5d)

Y luego haga clic en "Guardar".

![Label Manager4](https://github.com/gustavoparedes/QuickLog/assets/61228478/413ac50f-c6ef-42d3-98ea-47426081559e)

![Label Manager5](https://github.com/gustavoparedes/QuickLog/assets/61228478/45f6f4f7-f117-4afd-89ed-a5d5e16ab8f9)

Ahora puede cerrar la ventana del Gestor de Etiquetas y volver a ella cuando necesite crear o eliminar etiquetas.

Para aplicar las etiquetas, debe seleccionar el log o los logs a los que desea aplicar la etiqueta. Seleccionando logs:

![SelectLog1](https://github.com/gustavoparedes/QuickLog/assets/61228478/73f813ec-8a75-4a64-861e-637c329e6842)

Una vez seleccionado, lo verá así:

![SelectLog2](https://github.com/gustavoparedes/QuickLog/assets/61228478/8558a636-9621-4642-a90d-26bf281ab919)

Puede seleccionar varios logs en fila presionando Shift.

![SelectLog3](https://github.com/gustavoparedes/QuickLog/assets/61228478/0a7a3aef-77d3-49f4-a839-53ecf249c830)

O seleccionar a su discreción manteniendo presionada la tecla Ctrl, como en el Explorador de Windows.

![SelectLog4](https://github.com/gustavoparedes/QuickLog/assets/61228478/9dec77e6-9a73-424c-b035-0100afbb4e96)

Ahora que tiene seleccionado el log o los logs, simplemente haga clic en "Agregar Etiqueta".

![AddLabel1](https://github.com/gustavoparedes/QuickLog/assets/61228478/e89efebd-fe3f-4bf3-a5de-6b4cdcf83754)

Verá una ventana con las etiquetas creadas en el Gestor de Etiquetas:

![AddLabel2](https://github.com/gustavoparedes/QuickLog/assets/61228478/180423b4-7446-4138-a2d9-3e59fd5c3285)

Simplemente seleccione la etiqueta que desea aplicar usando el mismo método de selección que para los logs, y haga clic en "Establecer Etiqueta".

![AddLabel3](https://github.com/gustavoparedes/QuickLog/assets/61228478/5713289c-490b-4a14-87bd-5ee8ed76c1f3)

Una vez que la etiqueta se aplica, se verá así:

![AddLabel4](https://github.com/gustavoparedes/QuickLog/assets/61228478/5a2a1277-7a69-47e5-a043-dddb61c0306f)

Para añadir comentarios, seleccione el log (solo uno) al que desea agregar el comentario y haga clic en "Agregar Comentario".

![AddComment1](https://github.com/gustavoparedes/QuickLog/assets/61228478/544c6fc3-3b08-4d52-ba26-446e8d59121b)

Use el cuadro de texto para ingresar el comentario que necesita.

![AddComment2](https://github.com/gustavoparedes/QuickLog/assets/61228478/572b1821-6085-4732-8976-e668fddca600)


Asegurese de dar click en "Save Comment".

![AddComment3](https://github.com/gustavoparedes/QuickLog/assets/61228478/8755c012-9ecd-4e73-9a06-b322c48bd8c1)

![AddComment4](https://github.com/gustavoparedes/QuickLog/assets/61228478/72516cb0-3fde-48a9-aa8b-e70dbaff7b28)


# 5. Guardar en:

Opciones para exportar los logs que se muestran actualmente en la tabla de logs a PDF o CSV.

![GuardarEn](https://github.com/gustavoparedes/QuickLog/assets/61228478/48892f2d-f599-4d7f-b28f-b57f8b738148)

# 6. Filtros Relacionados con el Tiempo:

![FiltrosTiempo](https://github.com/gustavoparedes/QuickLog/assets/61228478/a99c0939-c0e4-4b64-968d-9ec532fdb755)

Permite generar un filtro basado en el tiempo de inicio de un log y el tiempo de finalización de otro, como los tiempos de inicio y fin de sesión de un usuario.

![RangoTiempo](https://github.com/gustavoparedes/QuickLog/assets/61228478/6b2ef126-1b13-4fca-9755-74619f9ae6c7)

También puedes crear un filtro de tiempo para un número específico de minutos alrededor del tiempo de un evento. Por ejemplo, si un evento ocurrió a las 14:01:31 y usas la opción "Minutos alrededor" con un minuto, filtrará todos los eventos entre un minuto antes y un minuto después, es decir, entre las 14:00:31 y las 14:02:31.

# 7. Consola de Logs:

Muestra mensajes de operación.

# 8. Filtros Personalizados:

Permite realizar filtros granulares por cualquiera de los campos en cada log. Recuerda que los filtros básicos solo muestran eventos categorizados. Se pueden crear filtros personalizados básicos que incluyan opciones de búsqueda de texto; este texto se buscará en los campos EventMessage y EventMessageXML.

![FiltroPersonalizado](https://github.com/gustavoparedes/QuickLog/assets/61228478/2fc087d0-5a5b-4c53-834c-0ed10be8b9ce)

Los filtros se pueden aplicar a todos los campos de los logs. La lógica de búsqueda entre diferentes campos es una operación AND, lo que significa que el filtro se aplica de la siguiente manera:

Primero, debe estar dentro del rango de tiempo como condición primaria, Y debe coincidir con el UserID, Y EventID, Y Machine Name, Y Level, Y LogName, Y Label, Y los términos de búsqueda dentro de los campos EventMessage o EventMessageXML.

**Término de Búsqueda:** Buscará dentro de los campos EventMessage o EventMessageXML y puede usar los operadores lógicos AND y OR.

Por ejemplo, puedes buscar: -1001

![Buscar1](https://github.com/gustavoparedes/QuickLog/assets/61228478/4e918d94-a504-4cd5-b3f2-8c3c9b9c8618)

O buscar: -1001 AND logontype'>2<

![Buscar2](https://github.com/gustavoparedes/QuickLog/assets/61228478/30698eb3-4a62-4620-8aa5-1b8a7ddcbeb9)

Encontrará coincidencias de búsqueda ya sean condiciones AND u OR dentro de los campos EventMessage o EventMessageXML.

# 9. Barra de Progreso:

La barra de progreso muestra el avance de los logs que se cargan en la base de datos así como el procesamiento de los logs.

![Procesando2](https://github.com/gustavoparedes/QuickLog/assets/61228478/91ecb5a7-3a78-42ce-b0f3-be907d4bfb8a)


# El Flujo de Trabajo:

Básicamente, procesar uno o varios (usualmente todos) logs de una o varias máquinas y luego comenzar a buscar logs relacionados con actividades de interés, poner etiquetas y comentarios, y finalmente crear una línea de tiempo con las sesiones o eventos relevantes que fueron registrados en orden cronológico como una línea de tiempo.

![Línea de Tiempo1](https://github.com/gustavoparedes/QuickLog/assets/61228478/d68134d1-a69a-4c2c-95ba-ceae46f6b200)

Lo primero que hay que hacer es crear un workspace.

# Crear / Abrir / Cerrar un Workspace:

![Espacios de Trabajo](https://github.com/gustavoparedes/QuickLog/assets/61228478/1f3b0da8-bea2-4ee2-9b18-7d947ec7f59c)

Después, agrega logs usando la opción "Adquirir Logs" para uno o múltiples archivos o "Procesar Carpeta de Logs" para procesar todos los archivos .evtx dentro de una carpeta. Los logs se almacenarán en la base de datos y se clasificarán según las categorías predefinidas.

Filtros Básicos:

![FiltrosBásicos3](https://github.com/gustavoparedes/QuickLog/assets/61228478/ea292296-9407-4188-8f0d-e96d53af7b08)
![FiltrosBásicos4](https://github.com/gustavoparedes/QuickLog/assets/61228478/affe8b14-62a3-480d-b86a-0bcea6698e0b)

Al final del procesamiento, verás todos los logs clasificados y se mostrarán los usuarios encontrados en los logs.

![Final1](https://github.com/gustavoparedes/QuickLog/assets/61228478/aab04536-60c5-4f0f-a90e-fcceeb8bfd60)

El programa compilado se puede ejecutar desde una unidad USB, disco externo o carpeta de red sin necesidad de instalación.

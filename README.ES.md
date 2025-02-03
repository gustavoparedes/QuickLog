# QuickLog

![1](https://github.com/user-attachments/assets/2e0cef5a-84c7-4e21-a0e5-d9ef05678238)


# Descargar version compilada

Puede descargarla [aqui](https://github.com/gustavoparedes/QuickLog/releases/download/v0.2/QuickLogv0.2.rar) 



**Quick Log** es una herramienta simple para visualizar logs de Windows en formato EVTX, organizada según este trabajo: 
https://cybersecuritynews.com/windows-event-log-analysis/ y pensada para los cursos de forense digital con herramientas 
de código abierto dictados en Internet Solutions S.A.S, Bogotá, Colombia. Requiere Windows 10 de 64 bits y una resolución de 1920x1080. 

Los logs se organizan en workspaces.


# Workspace

Un workspace es un "contenedor" de logs que puede contener uno o más archivos .evtx de una o varias máquinas que ejecutan Windows. Antes de poder empezar a visualizar los logs, debe crear un nuevo workspace o abrir uno previamente creado. Por defecto, un workspace recién creado no contiene archivos de logs; debe agregar logs después de crear el workspace. Siempre se pueden agregar logs adicionales. Un workspace también puede ser abierto para continuar revisando logs y puede ser cerrado cuando sea necesario.

# Adquisición de Logs

Durante la adquisición de logs, se leen los logs de Windows y los campos más relevantes se almacenan en una base de datos SQLite. Una vez que se completa el proceso de lectura y almacenamiento, los archivos de logs originales ya no son necesarios, ya que se utilizará la base de datos. Cada entrada de log es un registro en la base de datos dentro de la tabla de logs, y cada registro contiene los siguientes campos con nombres descriptivos:

TimeCreated, UserID, EventID, Machine, Level, LogName, EventMessage, EventMessageXML y ActivityID.

**TimeCreated:**

La hora en la que se creó el evento, almacenada en UTC, por lo cual se debe ajustar a la zona horaria correcta extrayéndola del registro y usar la zona horaria de la evidencia para establecer la hora real.

![UTC](https://github.com/user-attachments/assets/b9e54019-1823-4a36-9c11-52c2e9e84b50)


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
Al dar click sobre cualquier fila la informacion completa se mostrara en el cuadro de texto de la derecha

![Tabladelogs](https://github.com/user-attachments/assets/4b7abb4a-6307-4dba-8e17-9659b94b655c)


# 3. Cuadro de Texto:

Muestra el contenido de la fila seleccionada y permite ver los resultados de búsqueda resaltados y leer cómodamente el contenido de los logs.

![CuadrodeTexto](https://github.com/user-attachments/assets/2e0caeec-8cf9-4f03-b09e-c02585e821f6)


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

Para aplicar las etiquetas, debe seleccionar el log o los logs a los que desea aplicar la etiqueta. 


Seleccionando logs:

Solo dé clic sobre cada fila o log para seleccionarlo. Use las teclas Ctrl o Shift para seleccionar varios a la vez, como se hace en el Explorador de Windows.

![Seleccionando Logs](https://github.com/user-attachments/assets/73180d75-88f5-4c2a-b7f7-15c673005143)


Seleccionar varios logs en fila presionando Shift.

![SelectLog3](https://github.com/gustavoparedes/QuickLog/assets/61228478/0a7a3aef-77d3-49f4-a839-53ecf249c830)

O  presionando la tecla Ctrl, como en el Explorador de Windows.

![SelectLog4](https://github.com/gustavoparedes/QuickLog/assets/61228478/9dec77e6-9a73-424c-b035-0100afbb4e96)

Ahora que tiene seleccionado el log o los logs, simplemente haga clic en "Agregar Etiqueta".

![AddLabel1](https://github.com/gustavoparedes/QuickLog/assets/61228478/e89efebd-fe3f-4bf3-a5de-6b4cdcf83754)

Verá una ventana con las etiquetas creadas en el Gestor de Etiquetas:

![AddLabel2](https://github.com/gustavoparedes/QuickLog/assets/61228478/180423b4-7446-4138-a2d9-3e59fd5c3285)

Simplemente seleccione la etiqueta que desea aplicar usando el mismo método de selección que para los logs, y haga clic en "Set Label".

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

Hay que tener en cuenta que el archivo separado por comas o CSV puede dar problemas al momento de importarlo a una herramienta como libre office o Excel, esto debido a que dentro de los campos EventMessage y EventMessageXML pueden existir comas lo que puede conducir a la separacion erronea de los campos.

![Separacion por comas](https://github.com/user-attachments/assets/ec84f92a-c268-4055-ac5a-27056107c7ba)


Por este motivo al exportar los logs a CSV la separacion esta dadta por tres caracteres seguidos asi: "***"

# 6. Filtros Relacionados con el Tiempo:

![FiltrosTiempo](https://github.com/gustavoparedes/QuickLog/assets/61228478/a99c0939-c0e4-4b64-968d-9ec532fdb755)

Permite generar un filtro basado en la hora de dos registros, tomando la hora menor o más antigua como el límite inferior y la hora mayor o más reciente como el límite superior.
Por ejemplo, podríamos ver todos los logs generados durante la sesión de un usuario.

**Primero seleccione los dos logs que quiere usar para hacer el filtro de rango de tiempo. Luego, haga clic en el botón "Time Range".**

![RangoTiempo](https://github.com/gustavoparedes/QuickLog/assets/61228478/6b2ef126-1b13-4fca-9755-74619f9ae6c7)

También puede crear un filtro de tiempo para un número específico de minutos alrededor del tiempo de un evento. Por ejemplo, si un evento ocurrió a las 14:01:31 y usamos la opción "Minutes Aroud" con un minuto, filtrará todos los eventos entre un minuto antes y un minuto después, es decir, entre las 14:00:31 y las 14:02:31.

# 7. Consola de Logs:

Muestra mensajes de operación.

# 8. Filtros Personalizados:

Permite realizar filtros granulares por cualquiera de los campos en cada log. Recuerde que los filtros básicos solo muestran eventos categorizados. Se pueden crear filtros personalizados básicos que incluyan opciones de búsqueda de texto; este texto se buscará en los campos EventMessage y EventMessageXML.

![Filter](https://github.com/user-attachments/assets/0d71436b-2063-4d37-9e60-6d30f82a0b64)


Los filtros se pueden aplicar a todos los campos de los logs. La lógica de búsqueda entre diferentes campos es una operación AND, y dentro de un mismo campo, una OR.

Por ejemplo, para buscar todos los logs que tengan el **EventID 5615**, sin importar ninguna otra condición, la consulta sería:

![BarraFiltros1](https://github.com/user-attachments/assets/831e6770-020c-4ad4-b256-eb5d12f5ff8a)

Si añadiéramos otra condición, por ejemplo, el usuario **S-1-5-18**, la búsqueda combinaría ambas condiciones con un operador AND.

![BarraFiltros2](https://github.com/user-attachments/assets/f4aa5224-ff27-48b6-8d89-63894bb3eeb4)

Esto es todos los logs del usuario **S-1-5-18** Y **EventID**.

Ahora, supongamos que queremos obtener todos los logs del usuario **S-1-5-18** y que tengan **EventID 5615 o 5617**.

![BarraFiltros3](https://github.com/user-attachments/assets/8b9eec53-8831-48b5-a355-3298b7ddc936)

Agregando una condicion mas,en este caso que contenga la palabra **Management**

![BarraFiltros4](https://github.com/user-attachments/assets/1cc34bcb-d50f-4407-a1d5-7816834dbcc3)


De esta forma, se puede personalizar el filtro para hacerlo más granular y específico.


**Término de Búsqueda:** Buscará dentro de los campos EventMessage o EventMessageXML y puede usar los operadores lógicos AND y OR

Por ejemplo, puedes buscar: -1001

![Busqueda1](https://github.com/user-attachments/assets/b2d9018f-10ea-40e2-b92f-1936e72d8793)


O buscar: -1001 AND logontype'>2<

![Busqueda2](https://github.com/user-attachments/assets/20760c99-9d54-4631-8a13-934aaa2316bf)


Encontrará coincidencias de búsqueda ya sean condiciones AND u OR dentro de los campos EventMessage o EventMessageXML.

Puede usar expresiones regulares para la búsqueda activando la opción Regexp.

![Regexp](https://github.com/user-attachments/assets/7402fdac-4f09-4fb2-986c-6b907391612d)

En el ejemplo anterior, **-100[12].*?LogonType'>2<**, buscamos -1001 o -1002, seguido de cualquier carácter (.), cualquier número de veces (*), que podría estar o no (?), y después LogonType'>2<.

Esto nos permite encontrar todos los logins interactivos de los usuarios 1001 y 1002.




# 9. Barra de Progreso:

La barra de progreso muestra el avance de los logs que se cargan en la base de datos así como el procesamiento de los logs.

![Procesando2](https://github.com/gustavoparedes/QuickLog/assets/61228478/91ecb5a7-3a78-42ce-b0f3-be907d4bfb8a)


# El Flujo de Trabajo:

Básicamente, procesar uno o varios (usualmente todos) logs de una o varias máquinas y luego comenzar a buscar logs relacionados con actividades de interés, poner etiquetas y comentarios, y finalmente crear una línea de tiempo con las sesiones o eventos relevantes que fueron registrados en orden cronológico como una línea de tiempo.

![Línea de Tiempo1](https://github.com/gustavoparedes/QuickLog/assets/61228478/d68134d1-a69a-4c2c-95ba-ceae46f6b200)

Lo primero que hay que hacer es crear un workspace.

# Crear / Abrir / Cerrar un Workspace:

![Espacios de Trabajo](https://github.com/gustavoparedes/QuickLog/assets/61228478/1f3b0da8-bea2-4ee2-9b18-7d947ec7f59c)

Después, agregar logs usando la opción "Acquire Logs" para uno o múltiples archivos o "Porcess Log Folder" para procesar todos los archivos .evtx dentro de una carpeta. Los logs se almacenarán en la base de datos y se clasificarán según las categorías predefinidas.

Filtros Básicos:

![FiltrosBásicos3](https://github.com/gustavoparedes/QuickLog/assets/61228478/ea292296-9407-4188-8f0d-e96d53af7b08)
![FiltrosBásicos4](https://github.com/gustavoparedes/QuickLog/assets/61228478/affe8b14-62a3-480d-b86a-0bcea6698e0b)

Al final del procesamiento, verás todos los logs clasificados y se mostrarán los usuarios encontrados en los logs.

![Final1](https://github.com/gustavoparedes/QuickLog/assets/61228478/aab04536-60c5-4f0f-a90e-fcceeb8bfd60)

El programa compilado se puede ejecutar desde una unidad USB, disco externo o carpeta de red sin necesidad de instalación.


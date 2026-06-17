# Enrutamiento MVC de ASP.NET

## ¿Qué es el Enrutamiento MVC?

El **Enrutamiento (Routing)** es el mecanismo que utiliza **ASP.NET Core MVC** para dirigir las solicitudes realizadas por el navegador hacia el controlador y la acción correspondientes.

Cuando un usuario escribe una dirección URL en el navegador, el framework analiza la ruta solicitada y determina automáticamente qué controlador debe ejecutarse, qué método debe invocarse y, si existe, cuál es el valor del parámetro `id`.

El enrutamiento es uno de los componentes más importantes de una aplicación MVC porque permite separar la lógica de navegación de la lógica del negocio, facilitando el desarrollo y mantenimiento del sistema.

---

## Configuración del Enrutamiento

En ASP.NET Core MVC el enrutamiento se configura en el archivo **Program.cs**.

En el ejemplo desarrollado durante la sesión se utilizó la siguiente configuración:

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

Esta configuración establece la ruta predeterminada de toda la aplicación.

Los parámetros de la ruta tienen el siguiente significado:

| Parámetro | Descripción |
|-----------|-------------|
| controller | Controlador que atenderá la solicitud. |
| action | Método del controlador que será ejecutado. |
| id | Parámetro opcional que puede enviarse mediante la URL. |

---

## Funcionamiento del Enrutamiento

Cuando un usuario realiza una petición desde el navegador, ASP.NET Core analiza la URL y la divide en tres partes principales.

Por ejemplo, la siguiente dirección:

```text
/Home/Index/3
```

es interpretada por el framework como:

| Elemento | Valor |
|----------|-------|
| Controller | HomeController |
| Action | Index() |
| id | 3 |

Esto significa que ASP.NET ejecutará el siguiente método:

```csharp
public IActionResult Index(int id)
{
    ...
}
```

donde el parámetro `id` tendrá el valor **3**.

---

## Valores Predeterminados

La ruta configurada en el archivo Program.cs también define valores por defecto.

```csharp
pattern: "{controller=Home}/{action=Index}/{id?}"
```

Esto significa:

- Si no se especifica un controlador, se utilizará **Home**.
- Si no se especifica una acción, se utilizará **Index**.
- El parámetro **id** es opcional.

Por ejemplo:

### URL

```text
/
```

Equivale a:

```text
HomeController

↓

Index()
```

---

### URL

```text
/Home
```

Equivale a:

```text
HomeController

↓

Index()
```

---

### URL

```text
/Home/Index
```

Equivale a:

```text
HomeController

↓

Index()
```

---

### URL

```text
/Home/Index/15
```

Equivale a:

```text
HomeController

↓

Index(15)
```

---

## Ejemplos de Enrutamiento

| URL | Controlador | Acción | id |
|-----|-------------|---------|----|
| / | HomeController | Index | Ninguno |
| /Home | HomeController | Index | Ninguno |
| /Home/Index | HomeController | Index | Ninguno |
| /Home/Index/10 | HomeController | Index | 10 |
| /Estudiante/Historial/20260123 | EstudianteController | Historial | 20260123 |
| /Asignacion/Detalle/10 | AsignacionController | Detalle | 10 |

---

## Flujo del Enrutamiento

El proceso que sigue ASP.NET Core MVC puede representarse de la siguiente manera:

```text
Usuario

↓

Escribe una URL

↓

ASP.NET Routing

↓

Busca el Controller

↓

Busca la Action

↓

Obtiene el parámetro id (si existe)

↓

Ejecuta el método correspondiente
```

Este mecanismo permite que el desarrollador únicamente deba crear los controladores y sus métodos, mientras que el framework se encarga automáticamente de dirigir cada solicitud.

---

# Controladores en ASP.NET MVC

## ¿Qué es un Controlador?

Un **Controlador (Controller)** es una clase encargada de recibir las solicitudes HTTP realizadas por los usuarios.

Su principal responsabilidad consiste en:

- Procesar la solicitud.
- Comunicarse con los modelos.
- Obtener la información necesaria.
- Enviar los datos a una vista o devolver una respuesta.

Cada controlador debe heredar de la clase:

```csharp
Controller
```

Ejemplo:

```csharp
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```

---

## Convención de nombres

Todos los controladores deben finalizar con la palabra:

```text
Controller
```

Ejemplos válidos:

- HomeController
- EstudianteController
- ApiController
- SateliteController

Ejemplo incorrecto:

```text
Home
```

ya que ASP.NET MVC no podrá reconocerlo como un controlador.

---

## Acciones (Actions)

Las **acciones** son métodos públicos que pertenecen a un controlador.

Cada acción responde a una solicitud específica realizada por el usuario.

Ejemplo:

```csharp
public IActionResult Index()
{
    return View();
}
```

Cuando un usuario accede a:

```text
/Home/Index
```

el framework ejecutará automáticamente este método.

Otro ejemplo:

```csharp
public IActionResult Historial(int id)
{
    return View();
}
```

puede invocarse mediante:

```text
/Estudiante/Historial/20260123
```

donde el parámetro **id** recibirá el valor:

```text
20260123
```

---

## Responsabilidades de un Controlador

Un controlador puede realizar tareas como:

- Mostrar vistas.
- Validar información.
- Procesar formularios.
- Consultar modelos.
- Registrar información.
- Redireccionar a otras páginas.
- Consumir servicios.
- Devolver respuestas JSON.

No es recomendable colocar toda la lógica del sistema dentro del controlador, ya que esto dificulta el mantenimiento de la aplicación.

---

## Controladores utilizados durante la práctica

En el proyecto desarrollado durante la sesión se implementaron los siguientes controladores:

| Controlador | Función |
|-------------|----------|
| HomeController | Mostrar el Dashboard principal. |
| SateliteController | Administrar la Matriz Dispersa. |
| XmlController | Procesar archivos XML. |
| ApiController | Exponer servicios REST en formato JSON. |
| HttpClienteController | Consumir APIs externas. |
| LogsController | Administrar la bitácora del sistema. |

Cada uno tiene una responsabilidad específica, respetando el patrón MVC y favoreciendo una mejor organización del proyecto.

# Resultados de Acción (ActionResult)

## ¿Qué es un ActionResult?

En ASP.NET Core MVC, una **acción** de un controlador devuelve un objeto denominado **ActionResult** o **IActionResult**.

Este objeto representa la respuesta que el servidor enviará al navegador después de procesar una solicitud.

Dependiendo de la acción ejecutada, el controlador puede devolver una vista, un archivo, una respuesta JSON, una redirección o simplemente texto.

En ASP.NET Core se utiliza principalmente la interfaz:

```csharp
IActionResult
```

Ejemplo:

```csharp
public IActionResult Index()
{
    return View();
}
```

En este caso, la acción devuelve una vista HTML al navegador.

---

## Tipos de Resultados de Acción

ASP.NET MVC permite devolver diferentes tipos de respuestas dependiendo de las necesidades de la aplicación.

| Resultado | Descripción |
|-----------|-------------|
| View() | Devuelve una vista HTML al usuario. |
| Json() | Devuelve información en formato JSON. |
| Redirect() | Redirecciona hacia otra dirección URL. |
| RedirectToAction() | Redirecciona hacia otra acción del controlador. |
| Content() | Devuelve texto plano. |
| File() | Permite descargar archivos. |
| NotFound() | Devuelve un error HTTP 404. |
| Unauthorized() | Devuelve un error HTTP 401. |

---

## Ejemplos

### Retornar una Vista

```csharp
public IActionResult Index()
{
    return View();
}
```

---

### Retornar un JSON

```csharp
public IActionResult ObtenerDatos()
{
    return Json(datos);
}
```

---

### Redireccionar a otra Acción

```csharp
public IActionResult Guardar()
{
    return RedirectToAction("Index");
}
```

---

### Devolver Texto

```csharp
public IActionResult Estado()
{
    return Content("Servidor activo.");
}
```

---

## Resultados de Acción utilizados durante la práctica

En el proyecto desarrollado durante la sesión se utilizaron varios tipos de resultados.

Por ejemplo:

En **HomeController**:

```csharp
return View(ObtenerViewModel());
```

Se devuelve una vista junto con un modelo.

En **ApiController**:

```csharp
return Json(dtos);
```

Se devuelve un arreglo de objetos en formato JSON.

En **XmlController**:

```csharp
return RedirectToAction("Index", "Home");
```

Después de procesar el archivo XML el usuario es redireccionado nuevamente al Dashboard.

---

# Almacenamiento en Caché (Output Cache)

## ¿Qué es el almacenamiento en caché?

El almacenamiento en caché consiste en guardar temporalmente el resultado de una página para evitar generar nuevamente el mismo contenido en cada solicitud.

Cuando una página se encuentra almacenada en memoria, el servidor responde más rápidamente y reduce el consumo de recursos.

Esto mejora considerablemente el rendimiento de una aplicación web.

---

## Funcionamiento

Sin caché:

```text
Usuario

↓

Solicita página

↓

Servidor genera nuevamente la página

↓

Servidor responde
```

Con caché:

```text
Usuario

↓

Solicita página

↓

Servidor obtiene la página desde memoria

↓

Servidor responde inmediatamente
```

---

## Ventajas del Caché

- Reduce el tiempo de respuesta.
- Disminuye el consumo de CPU.
- Reduce consultas repetidas a la base de datos.
- Mejora la experiencia del usuario.
- Permite soportar una mayor cantidad de usuarios.

---

## Contenido Dinámico

Aunque una página esté almacenada en caché, es posible mostrar información dinámica utilizando mecanismos como **WriteSubstitution()**.

Esto permite actualizar únicamente ciertas partes de la página, por ejemplo:

- Noticias.
- Publicidad.
- Fecha y hora.
- Información personalizada del usuario.

Mientras el resto de la página permanece almacenado en memoria.

---

## Ejemplo

```csharp
[OutputCache(Duration = 60)]
public IActionResult Index()
{
    return View();
}
```

La página permanecerá almacenada durante **60 segundos**.

---

# Acceso Seguro a Bases de Datos

## Importancia

Toda aplicación web debe proteger la información almacenada en sus bases de datos.

Un acceso inseguro puede provocar:

- Robo de información.
- Modificación de datos.
- Eliminación de registros.
- Accesos no autorizados.
- Ataques de inyección SQL.

Por esta razón existen diversas recomendaciones de seguridad propuestas por OWASP.

---

## Consultas Seguras

Las consultas hacia la base de datos deben evitar que el usuario pueda modificar el comando SQL mediante datos ingresados desde formularios.

Para ello se recomienda utilizar consultas parametrizadas.

Ejemplo conceptual:

```csharp
SELECT * FROM Usuarios
WHERE Usuario = @usuario
```

En lugar de concatenar cadenas de texto.

---

## Validación de Entradas

Antes de ejecutar cualquier consulta es necesario validar la información recibida.

Algunas validaciones comunes son:

- Campos obligatorios.
- Longitud máxima.
- Formato correcto.
- Expresiones regulares.
- Tipos de datos.

Si la validación falla, la consulta no debe ejecutarse.

---

## Configuración Segura

Las aplicaciones también deben cumplir con buenas prácticas como:

- Utilizar el mínimo nivel de privilegios.
- Cerrar las conexiones después de utilizarlas.
- Deshabilitar funciones innecesarias.
- Eliminar cuentas predeterminadas.
- Eliminar contenido de prueba.

---

## Autenticación Segura

Las credenciales utilizadas para acceder a la base de datos deben protegerse adecuadamente.

Se recomienda:

- Cambiar contraseñas predeterminadas.
- Utilizar credenciales diferentes para cada tipo de usuario.
- No almacenar contraseñas dentro del código fuente.
- Cifrar las cadenas de conexión.

---

# Buenas Prácticas OWASP

OWASP propone diversas recomendaciones para mejorar la seguridad de las aplicaciones web.

Entre las principales se encuentran:

- Validar todas las entradas del usuario.
- Manejar correctamente las excepciones.
- Utilizar consultas parametrizadas.
- Aplicar autenticación segura.
- Proteger la información sensible.
- Utilizar el principio de mínimo privilegio.
- Validar todas las solicitudes recibidas.
- Registrar eventos importantes en una bitácora.

Estas recomendaciones ayudan a disminuir vulnerabilidades y mejorar la seguridad de una aplicación.

---

# Relación entre la teoría y la práctica

Durante la sesión 12 varios de estos conceptos fueron implementados dentro del proyecto.

Por ejemplo:

| Concepto investigado | Implementación en el proyecto |
|----------------------|-------------------------------|
| Enrutamiento MVC | Program.cs |
| Controladores | HomeController, ApiController, XmlController |
| ActionResult | IActionResult |
| JSON | ApiController |
| RedirectToAction | XmlController y SateliteController |
| Validación | Expresiones Regulares (Regex) |
| Seguridad | BasicAuthorizeAttribute |
| HTTP Basic | ApiController |
| DTO | SateliteDto |
| Servicios | GraphvizCompilador |
| MVC | Toda la arquitectura del proyecto |

La práctica permitió observar cómo los conceptos estudiados durante la investigación son utilizados en una aplicación real desarrollada con ASP.NET Core MVC.

# Parte 2: Modelado del Ciclo de Vida y Enrutamiento Semántico

## 1. Mapeo Analítico de URLs

ASP.NET Core utiliza por defecto la siguiente plantilla de enrutamiento convencional:

```text
{controller=Home}/{action=Index}/{id?}
```

Con base en esta plantilla, el framework identifica automáticamente el controlador, la acción y el parámetro `id` que deben ejecutarse.

| URL Entrante del Cliente | Clase Controladora Buscada por el Framework | Método (Acción) Ejecutado | Parámetro `id` Inyectado |
|---------------------------|---------------------------------------------|---------------------------|--------------------------|
| https://ingenieria.usac.edu.gt/ControlAcademico/Login | ControlAcademicoController | Login | *(Ninguno / Opcional)* |
| https://ingenieria.usac.edu.gt/Estudiante/Historial/20260123 | EstudianteController | Historial | 20260123 |
| https://ingenieria.usac.edu.gt/Asignacion/Detalle/10 | AsignacionController | Detalle | 10 |
| https://ingenieria.usac.edu.gt/Home | HomeController | Index | *(Ninguno / Opcional)* |

---

## 2. Diagramación del Flujo Interactivo

El recorrido que realiza una petición HTTP dentro del patrón MVC es el siguiente:

### Paso 1

El usuario interactúa con la aplicación haciendo clic en un botón o escribiendo una URL en el navegador. El navegador envía una solicitud HTTP hacia el servidor donde se encuentra la aplicación ASP.NET Core.

### Paso 2

El sistema de **Routing** analiza la URL utilizando la plantilla de enrutamiento:

```text
{controller=Home}/{action=Index}/{id?}
```

Con base en esta información identifica qué **Controller** y qué **Action** deben ejecutarse.

### Paso 3

El **Controller** recibe la solicitud HTTP, obtiene el parámetro `id` si existe y solicita la información necesaria al **Modelo (Model)** para procesarla.

### Paso 4

El **Modelo (Model)** ejecuta la lógica de negocio, consulta o modifica los datos necesarios y devuelve el resultado al controlador.

### Paso 5

El **Controller** envía la información obtenida a la **Vista (View)**. La vista genera dinámicamente el código HTML, el servidor responde la petición y el navegador muestra la página al usuario.

# Conclusiones

- ASP.NET Core MVC organiza las aplicaciones separando claramente los modelos, las vistas y los controladores.
- El sistema de enrutamiento permite dirigir automáticamente las solicitudes HTTP hacia el controlador y la acción correspondientes.
- La utilización de APIs REST facilita el intercambio de información mediante formato JSON.
- La autenticación HTTP Basic constituye un mecanismo sencillo para proteger recursos restringidos.
- La implementación de estructuras de datos como la Matriz Dispersa y el Árbol AVL permitió aplicar conocimientos de programación avanzada dentro de una aplicación web.
- La validación de datos y las recomendaciones de OWASP contribuyen a desarrollar aplicaciones más seguras y confiables.

---

# Referencias

- Microsoft Learn. *ASP.NET Core MVC Documentation*. https://learn.microsoft.com/aspnet/core/mvc/
- Microsoft Learn. *Routing in ASP.NET Core*. https://learn.microsoft.com/aspnet/core/fundamentals/routing
- Microsoft Learn. *Controllers in ASP.NET Core MVC*. https://learn.microsoft.com/aspnet/core/mvc/controllers/actions
- Microsoft Learn. *Action Results in ASP.NET Core MVC*. https://learn.microsoft.com/aspnet/core/mvc/controllers/actions#action-results
- Microsoft Learn. *Output Caching in ASP.NET*. https://learn.microsoft.com/
- OWASP Foundation. *OWASP Top 10*. https://owasp.org/www-project-top-ten/
- OWASP Foundation. *Proactive Controls*. https://owasp.org/www-project-proactive-controls/

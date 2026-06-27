# Actividad de Laboratorio: Interoperabilidad y Carga Masiva de Datos

## Nombre:
**Estudiante:** _______________________

**Curso:** Introducción a la Programación y Computación 2

**Fecha:** 26 de junio de 2026

---

# Parte 1. Evaluación Conceptual y Buenas Prácticas

## Formatos de Intercambio

| Formato | Ventajas | Desventajas |
|----------|----------|-------------|
| CSV | Es un formato ligero, sencillo de generar y leer, además de ser ampliamente compatible con diferentes aplicaciones. | No permite representar estructuras jerárquicas ni almacenar información sobre los tipos de datos. |
| XML | Permite representar datos complejos y jerárquicos, además de ser extensible y autodescriptivo. | Genera archivos más grandes y su procesamiento suele ser más lento que otros formatos como CSV o JSON. |

---

## 1. Diferencia entre Serialización y Deserialización

La **serialización** es el proceso mediante el cual un objeto de C# se convierte en un formato de intercambio de datos, como JSON, utilizando la clase `JsonSerializer.Serialize()` de la librería **System.Text.Json**.

La **deserialización** es el proceso inverso. Consiste en tomar un documento JSON y convertirlo nuevamente en un objeto de C#, utilizando el método `JsonSerializer.Deserialize<T>()`.

En otras palabras:

- **Serializar:** Objeto → JSON.
- **Deserializar:** JSON → Objeto.

---

## 2. Antipatrón de Rendimiento "N+1"

El problema **N+1** ocurre cuando durante la lectura de un archivo masivo se realiza una operación hacia la base de datos por cada registro leído.

Por ejemplo, si un archivo contiene 5,000 registros, se ejecutan 5,000 inserciones independientes, provocando un alto consumo de recursos y aumentando considerablemente el tiempo de procesamiento.

La solución consiste en aplicar una estrategia de **Batching**, donde primero se almacenan todos los objetos en una colección temporal y posteriormente se realiza una única inserción mediante `AddRange()` seguida de una sola llamada a `SaveChangesAsync()`. Esto reduce significativamente la cantidad de operaciones realizadas sobre la base de datos y mejora el rendimiento.

---

# Parte 2. Implementación Práctica en C#

## Desafío 1: Consumo de Endpoints y Deserialización

```csharp
using System.Text.Json;

public class AlumnoService
{
    private readonly HttpClient _httpClient;

    public AlumnoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Alumno>?> ObtenerAlumnosAsync()
    {
        try
        {
            HttpResponseMessage respuesta =
                await _httpClient.GetAsync("https://api.usac.edu/v1/alumnos");

            respuesta.EnsureSuccessStatusCode();

            string json = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Alumno>>(json, opciones);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}
```

---

## Clase Alumno

```csharp
public class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Carrera { get; set; } = string.Empty;
}
```

---

# Desafío 2: Endpoint para Carga Masiva CSV

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlumnosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("cargar-csv")]
    public async Task<IActionResult> CargarCsv(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
        {
            return BadRequest("Debe seleccionar un archivo.");
        }

        List<Alumno> alumnos = new List<Alumno>();

        using var stream = archivo.OpenReadStream();
        using var reader = new StreamReader(stream);

        await reader.ReadLineAsync();

        string? linea;

        while ((linea = await reader.ReadLineAsync()) != null)
        {
            string[] datos = linea.Split(',');

            Alumno alumno = new Alumno
            {
                Id = int.Parse(datos[0]),
                Nombre = datos[1],
                Carrera = datos[2]
            };

            alumnos.Add(alumno);
        }

        await _context.Alumnos.AddRangeAsync(alumnos);

        await _context.SaveChangesAsync();

        return Ok("Carga masiva realizada correctamente.");
    }
}
```

---

# Parte 3. Referencia Bibliográfica

Facultad de Ingeniería, Universidad de San Carlos de Guatemala. (2026). *Sesión 20: Integración de Datos. Consumo de APIs Externas y Carga Masiva (CSV/XML).* Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.

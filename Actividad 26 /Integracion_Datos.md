# Actividad de Laboratorio: Interoperabilidad y Carga Masiva de Datos

**Universidad:** Universidad de San Carlos de Guatemala

**Facultad:** Facultad de Ingeniería

**Curso:** Introducción a la Programación y Computación 2

**Laboratorio:** Sesión 20 - Integración de Datos

**Estudiante:** Edgar Daniel Cabrera

**Carné:** 202500007

**Fecha:** 26 de junio de 2026

---

# Parte 1. Evaluación Conceptual y Buenas Prácticas

## 1. Formatos de Intercambio

| Formato | Ventajas | Desventajas |
|----------|----------|-------------|
| **CSV** | Es un formato sencillo, ligero y fácil de procesar. Es compatible con una gran cantidad de aplicaciones y facilita el intercambio de datos tabulares. | No soporta estructuras jerárquicas, no almacena información sobre los tipos de datos y requiere un formato consistente para evitar errores durante la lectura. |
| **XML** | Permite representar estructuras de datos complejas y jerárquicas. Es un formato autodescriptivo, extensible y ampliamente utilizado para el intercambio de información entre sistemas. | Produce archivos más grandes, requiere mayor procesamiento para su lectura y escritura, y su sintaxis es más extensa que otros formatos como CSV o JSON. |

---

## 2. Diferencia entre Serialización y Deserialización

La **serialización** consiste en convertir un objeto de C# en un formato de intercambio de datos, como JSON, utilizando la clase `JsonSerializer.Serialize()` perteneciente a la librería **System.Text.Json**.

La **deserialización** es el proceso contrario. Consiste en convertir un documento JSON nuevamente en un objeto de C#, utilizando el método `JsonSerializer.Deserialize<T>()`.

En resumen:

- **Serialización:** Objeto → JSON.
- **Deserialización:** JSON → Objeto.

---

## 3. Antipatrón de Rendimiento N+1

El problema de rendimiento conocido como **N+1** ocurre cuando se realiza una operación independiente hacia la base de datos por cada registro leído de un archivo masivo.

Por ejemplo, si un archivo contiene 5,000 registros y cada uno se inserta individualmente, el sistema realizará 5,000 operaciones de escritura, aumentando considerablemente el tiempo de procesamiento y el consumo de recursos.

La estrategia recomendada para solucionar este problema consiste en aplicar **Batching**, almacenando todos los objetos en una colección temporal para posteriormente realizar una única inserción mediante `AddRange()` y una sola llamada a `SaveChangesAsync()`. De esta manera se reduce el número de accesos a la base de datos y se mejora significativamente el rendimiento.

---

# Parte 2. Implementación Práctica en C#

## Desafío 1. Consumo de Endpoints y Deserialización

### Clase Alumno

```csharp
public class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Carrera { get; set; } = string.Empty;
}
```

---

### Servicio para consumir la API

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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

            string contenidoJson =
                await respuesta.Content.ReadAsStringAsync();

            JsonSerializerOptions opciones =
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

            List<Alumno>? alumnos =
                JsonSerializer.Deserialize<List<Alumno>>
                (
                    contenidoJson,
                    opciones
                );

            return alumnos;
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

## Desafío 2. Endpoint para Carga Masiva CSV

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Saltar encabezado
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

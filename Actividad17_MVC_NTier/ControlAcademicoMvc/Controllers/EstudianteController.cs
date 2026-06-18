using Microsoft.AspNetCore.Mvc;
using ControlAcademicoMvc.Models;

namespace ControlAcademicoMvc.Controllers
{
    public class EstudianteController : Controller
    {
        
        public IActionResult Listar()
        {
            Estudiante[] estudiantes = MemoriaEstudiantes.ObtenerTodos();
            return View(estudiantes);
        }

       
        public IActionResult Historial(int id)
        {
            Estudiante? estudiante = MemoriaEstudiantes.BuscarPorCarne(id);

            if (estudiante == null)
            {
                return NotFound($"No se encontró estudiante con carné {id}");
            }

            return View(estudiante);
        }

        
        [HttpPost]
        public IActionResult Registrar([FromBody] Estudiante nuevoEstudiante)
        {
            if (nuevoEstudiante.Carne <= 0 || string.IsNullOrWhiteSpace(nuevoEstudiante.Nombre))
            {
                return BadRequest(new { mensaje = "Datos del estudiante inválidos." });
            }

            bool guardado = MemoriaEstudiantes.Registrar(nuevoEstudiante);

            if (!guardado)
            {
                return BadRequest(new { mensaje = "La memoria de estudiantes está llena." });
            }

            return Created($"/Estudiante/Historial/{nuevoEstudiante.Carne}", nuevoEstudiante);
        }
    }
}
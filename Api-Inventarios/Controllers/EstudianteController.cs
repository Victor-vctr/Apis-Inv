using Api_Inventarios.Models;
using Api_Inventarios.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Inventarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository _repo;

        public EstudianteController(IEstudianteRepository repo)
        {
            _repo = repo;
        }

        // GET: api/estudiante
        [HttpGet]
        public async Task<ActionResult<List<Estudiante>>> ObtenerTodos()
        {
            var estudiantes = await _repo.ObtenerTodosAsync();
            return Ok(estudiantes);
        }

        // GET: api/estudiante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> ObtenerPorId(int id)
        {
            var estudiante = await _repo.ObtenerPorIdAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return Ok(estudiante);
        }

        // POST: api/estudiante
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Estudiante estudiante)
        {
            if (estudiante == null)
            {
                return BadRequest();
            }

            await _repo.CrearAsync(estudiante);

            // Aquí puedes retornar un código de estado 201 Created y el URI de la nueva entidad creada
            return CreatedAtAction(nameof(ObtenerPorId), new { id = estudiante.Id }, estudiante);
        }

        // PUT: api/estudiante/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return BadRequest();
            }

            var estudianteExistente = await _repo.ObtenerPorIdAsync(id);
            if (estudianteExistente == null)
            {
                return NotFound();
            }

            await _repo.ActualizarAsync(estudiante);
            return NoContent(); // Código 204 indica que la actualización fue exitosa pero no hay contenido para devolver
        }

        // DELETE: api/estudiante/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var estudianteExistente = await _repo.ObtenerPorIdAsync(id);
            if (estudianteExistente == null)
            {
                return NotFound();
            }

            await _repo.EliminarAsync(id);
            return NoContent(); // Código 204 para indicar que la eliminación fue exitosa
        }
    }
}

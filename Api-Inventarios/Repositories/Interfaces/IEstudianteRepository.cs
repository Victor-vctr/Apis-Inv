using Api_Inventarios.Models;

namespace Api_Inventarios.Repositories.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<List<Estudiante>> ObtenerTodosAsync();
        Task<Estudiante?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Estudiante estudiante);
        Task ActualizarAsync(Estudiante estudiante);
        Task EliminarAsync(int id);
    }
}

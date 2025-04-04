using Api_Inventarios.Conexion;
using Api_Inventarios.Models;
using Api_Inventarios.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace Api_Inventarios.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly string _connectionString;

        public EstudianteRepository(ConnectionStrings conn)
        {
            _connectionString = conn.DefaultConnection;
        }

        // Obtener todos los estudiantes
        public async Task<List<Estudiante>> ObtenerTodosAsync()
        {
            var estudiantes = new List<Estudiante>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("ObtenerTodosEstudiantes", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                estudiantes.Add(new Estudiante
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Email = reader.GetString("Email")
                });
            }

            return estudiantes;
        }

        // Obtener estudiante por ID
        public async Task<Estudiante?> ObtenerPorIdAsync(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("ObtenerEstudiantePorId", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@p_Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Estudiante
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Email = reader.GetString("Email")
                };
            }

            return null;
        }

        // Crear un nuevo estudiante
        public async Task CrearAsync(Estudiante estudiante)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("InsertarEstudiante", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@p_Nombre", estudiante.Nombre);
            cmd.Parameters.AddWithValue("@p_Email", estudiante.Email);

            await cmd.ExecuteNonQueryAsync();
        }

        // Actualizar un estudiante
        public async Task ActualizarAsync(Estudiante estudiante)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("ActualizarEstudiante", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@p_Id", estudiante.Id);
            cmd.Parameters.AddWithValue("@p_Nombre", estudiante.Nombre);
            cmd.Parameters.AddWithValue("@p_Email", estudiante.Email);

            await cmd.ExecuteNonQueryAsync();
        }

        // Eliminar un estudiante
        public async Task EliminarAsync(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("EliminarEstudiante", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@p_Id", id);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}

namespace Api_Inventarios.Conexion
{
    public class ConnectionStrings
    {
        private readonly IConfiguration _configuration;
        public ConnectionStrings(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string DefaultConnection => _configuration.GetConnectionString("MySqlConnection");
    }
}

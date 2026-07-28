using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration; 

namespace WebBiblioteca.Data 
{
    public class MySqlContext 
    {
        private readonly string _connectionString;

        public MySqlContext(IConfiguration configuration ) 
        {
            _connectionString = configuration.GetConnectionString("MySql");
        }

        public MySqlConnection GetConnection() 
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
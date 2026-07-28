using MySqlConnector;
//using Microsoft.Extensions.Configuration; 

namespace WebBiblioteca.Data  
{
    public class MySqlConnectionFactory 
    {
        private readonly IConfiguration _configuration;

        public MySqlConnectionFactory(IConfiguration configuration ) 
        {
            _configuration = configuration;
        }

        public MySqlConnection CreateConnection() {
            return new MySqlConnection(
                _configuration.GetConnectionString("MySql")
                );
        }
    }
}
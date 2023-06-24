using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Repositories
{
    internal class DatabaseInitializer
    {
        private readonly IConfiguration _configuration;

        public DatabaseInitializer(IConfiguration configuration) => _configuration = configuration;

        public void InitializeDatabase()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            foreach (var scriptFile in Directory.GetFiles("path/to/your/scripts/folder", "*.sql").OrderBy(f => f))
            {
                string script = File.ReadAllText(scriptFile);

                using SqlCommand command = new SqlCommand(script, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}

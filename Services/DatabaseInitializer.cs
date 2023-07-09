using Domain;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using Services.Contracts;

namespace Services
{
    public class DatabaseInitializer : IDatabaseInitializerService
    {
        // private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        private static readonly string _scriptsFolderPath = @"C:\Users\Assembly\source\repos\roo-k13\Assembly Recipes\Data\Scripts";

        public DatabaseInitializer(IUserService userService, IConfiguration configuration)
        {
            // _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }

        public void Initialize()
        {
            ExecuteScripts();
            LogFirstFiveUsers();
        }

        private void ExecuteScripts()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            foreach (var scriptFile in Directory.GetFiles(_scriptsFolderPath, "*.sql").OrderBy(f => f))
            {
                var script = File.ReadAllText(scriptFile);
                using var command = new SqlCommand(script, connection);
                command.ExecuteNonQuery();
            }
        }

        private void LogFirstFiveUsers()
        {
            List<User> users = _userService.Find(5); // Retrieve the first 5 users

            foreach (User user in users)
            {
                // _logger.LogInformation("Username: {Username}, Password: {Password}", user.Username, user.Password);
            }
        }
    }
}
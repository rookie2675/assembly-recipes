using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using Services.Users;

namespace Services
{
    public class DatabaseInitializer : IDatabaseInitializerService
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        private static readonly string _scriptsFolderPath = @"C:\Users\Assembly\source\repos\roo-k13\Assembly Recipes\Data\Scripts";

        public DatabaseInitializer(ILogger<DatabaseInitializer> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
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
            var users = _userService.GetPage(5, 1);

            foreach (var user in users)
            {
                _logger.LogInformation($"Username: {user.Username}, Password: {user.Password}");
            }
        }

    }
}
using DataAccess.Contracts;
using DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ConfigureConnectionString(serviceCollection, configuration);
            ConfigureSqlQueryExecutor(serviceCollection);

        }

        private static void ConfigureConnectionString(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing or null.");

            serviceCollection.AddSingleton(connectionString);
        }

        private static void ConfigureSqlQueryExecutor(this IServiceCollection serviceCollection) => serviceCollection.AddScoped<ISqlQueryExecutor, SqlQueryExecutor>();
    }
}

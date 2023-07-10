using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Contracts;

namespace IoC
{
    public static class ConfigureServices
    {
        public static void ConfigureWebAppServices(this IServiceCollection serviceCollection) => serviceCollection.ConfigureCommon();

        private static void ConfigureCommon(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IRecipeService, RecipeService>();
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddScoped<IDatabaseInitializerService, DatabaseInitializer>();
            serviceCollection.AddScoped<DatabaseHelper>();

            ConfigureSession(serviceCollection);
        }

        private static void ConfigureSession(this IServiceCollection serviceCollection) 
        {
            serviceCollection.AddSession(options =>
            {
                options.Cookie.Name = "YourSessionCookieName";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }
    }
}
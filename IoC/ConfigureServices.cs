using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services;
using Services.Contracts;

namespace IoC
{
    public static class ConfigureServices
    {
        public static void ConfigureWebAppServices(this IServiceCollection serviceCollection) => serviceCollection.ConfigureCommon();

        private static void ConfigureCommon(this IServiceCollection serviceCollection)
        {
            ConfigureLogging(serviceCollection);

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IRecipeService, RecipeService>();
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddScoped<IDatabaseInitializerService, DatabaseInitializer>();

            ConfigureSession(serviceCollection);
        }

        private static void ConfigureLogging(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(builder =>
                builder.AddConsole());
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
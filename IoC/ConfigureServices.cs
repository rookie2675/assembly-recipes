using DataAccess;
using DataAccess.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Contracts;
using Services.Users;

namespace IoC
{
    public static class ConfigureServices
    {
        public static void ConfigureWebAppServices(this IServiceCollection serviceCollection) => serviceCollection.ConfigureCommon();

        private static void ConfigureCommon(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDatabaseHelper, DatabaseHelper>();

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IRecipeService, RecipeService>();
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
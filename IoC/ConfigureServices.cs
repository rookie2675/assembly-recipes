using Microsoft.Extensions.DependencyInjection;

using Services;
using Services.Contracts;

namespace IoC
{
    public static class ConfigureServices
    {
        public static void ConfigureWebAppServices(this IServiceCollection serviceCollection) => serviceCollection.ConfigureCommon();

        public static void ConfigureCommon(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IRecipeService, RecipeService>();
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        } 
    }
}
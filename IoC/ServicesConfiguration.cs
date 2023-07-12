using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Contracts;
using Services.Users;

namespace IoC
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IRecipeService, RecipeService>();
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
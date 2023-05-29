using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;

namespace IoC
{
    public static class ConfigureServices
    {
        public static void ConfigureWebApp(this IServiceCollection serviceCollection)
        {
            
        }

        public static void ConfigureCommon(this IServiceCollection serviceCollection)
        {
            // serviceCollection.AddScoped<IRecipeService, RecipeService>();
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Recipes;
using Repositories.Recipes.Ingredients;
using Repositories.Recipes.Steps;
using Repositories.Users;

namespace IoC
{
    public static class ConfigureRepositories
    {
        public static void ConfigureWebAppRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing or null.");

            serviceCollection.AddSingleton(connectionString);
            serviceCollection.ConfigureCommonRepositories();
        }

        private static void ConfigureCommonRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IStepsRepository, StepsRepository>();
            serviceCollection.AddScoped<IRecipeRepository, RecipeRepository>();
            serviceCollection.AddScoped<IIngredientRepository, RecipesIngredientsRepository>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Repositories.Recipes;
using Repositories.Recipes.Items.Ingredients;
using Repositories.Recipes.Items.Steps;
using Repositories.Users;

namespace IoC
{
    public static class RepositoriesConfiguration
    {
        public static void ConfigureRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IStepsRepository, StepsRepository>();
            serviceCollection.AddScoped<IRecipeRepository, RecipeRepository>();
            serviceCollection.AddScoped<IIngredientRepository, IngredientsRepository>();
        }
    }
}
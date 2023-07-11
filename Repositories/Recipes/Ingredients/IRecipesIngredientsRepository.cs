using Domain;
using Repositories.Contracts;

namespace Repositories.Recipes.Ingredients
{
    public interface IIngredientRepository : IOneToManyRepository<string, Recipe> { }
}

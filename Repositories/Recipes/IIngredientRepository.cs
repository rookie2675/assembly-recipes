using Domain;
using Repositories.Contracts;

namespace Repositories.Recipes
{
    public interface IIngredientRepository : IOneToManyRepository<string, Recipe> { }
}

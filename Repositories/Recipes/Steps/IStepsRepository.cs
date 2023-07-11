using Domain;
using Repositories.Contracts;

namespace Repositories.Recipes.Steps
{
    public interface IStepsRepository : IManyToOneRepository<RecipeStep, Recipe> { }
}
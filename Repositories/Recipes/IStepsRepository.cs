using Domain;
using Repositories.Contracts;

namespace Repositories.Recipes
{
    public interface IStepsRepository : IManyToOneRepository<string, Recipe> { }
}
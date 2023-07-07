using Domain;

namespace Repositories.Contracts
{
    public interface IRecipeStepRepository : IManyToManyRepository<string, Recipe> { }
}
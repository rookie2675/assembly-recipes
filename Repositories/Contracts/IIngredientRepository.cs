using Domain;

namespace Repositories.Contracts
{
    public interface IIngredientRepository : IManyToManyRepository<string, Recipe> { }
}

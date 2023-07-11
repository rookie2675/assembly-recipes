using Domain;

namespace Services.Contracts
{
    public interface IRecipeService : IEntityService<Recipe>
    {
        int GetTotalCount();
    }
}

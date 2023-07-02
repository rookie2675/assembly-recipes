namespace Repositories.Contracts
{
    public interface IRecipeStepRepository
    {
        List<string> Find(long id);
    }
}
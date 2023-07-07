using Domain;

namespace Repositories.Contracts
{
    public interface IManyToManyRepository<T1, T2>
    {
        IEnumerable<string> Find(Recipe recipe);
    }
}
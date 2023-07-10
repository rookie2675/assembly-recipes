using Domain;

namespace Repositories.Contracts
{
    public interface IOneToManyRepository<T1, T2>
    {
        IEnumerable<string> Find(T2 t2);
    }
}
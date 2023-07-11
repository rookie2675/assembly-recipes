using Domain;

namespace Repositories.Contracts
{
    public interface IManyToOneRepository<T1, T2>
    {
        List<string> Find(T2 t2);
    }
}

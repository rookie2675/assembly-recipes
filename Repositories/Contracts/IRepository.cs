namespace Repositories.Contracts
{
    public interface IRepository<E>
    {
        E? FindById(long id);
        List<E> FindAll();
        E Add(E entity);
        E Update(E entity);
        E Delete(long id);
    }
}
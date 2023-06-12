namespace Repositories.Contracts
{
    public interface IRepository<E>
    {
        E? Find(long id);
        List<E> Find();
        E Add(E entity);
        E Update(E entity);
        E Delete(long id);
    }
}
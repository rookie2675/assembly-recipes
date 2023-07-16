using Domain;

namespace Services.Contracts
{
    public interface IEntityService<T>
    {
        T? GetById(long id);

        List<T> GetAll();

        IEnumerable<T> GetPage(int page, int size);

        T Add(T entity);

        T Update(T entity);

        T Delete(long id);

        int GetTotalCount();
    }
}
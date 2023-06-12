namespace Services.Contracts
{
    public interface IEntityService<T>
    {
        T Find(long id);
        List<T> Find();
        T Add(T entity);
        T Update(T entity);
        T Delete(long id);
    }
}
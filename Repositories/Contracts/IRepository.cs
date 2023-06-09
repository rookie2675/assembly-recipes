namespace Repositories.Contracts
{
    public interface IRepository<TEntity>
    {
        TEntity Find(long id);
        List<TEntity> Find();
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(long id);
    }
}

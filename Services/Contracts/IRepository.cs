namespace Services.Contracts
{
    internal interface IRepository<TEntity>
    {
        List<TEntity> GetAll();
        TEntity Get(int id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(int id);
    }
}

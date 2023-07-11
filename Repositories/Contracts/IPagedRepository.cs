namespace Repositories.Contracts
{
    public interface IPagedRepository<T>
    {
        IEnumerable<T> FindPage(int page, int pageSize);

        int GetTotalCount();
    }
}
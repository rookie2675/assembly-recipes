using Domain;
using Repositories.Contracts;

namespace Repositories.Users
{
    public interface IUserRepository : IRepository<User>, IPagedRepository<User>
    {
        User? FindByUsernameAndPassword(string username, string password);
    }
}

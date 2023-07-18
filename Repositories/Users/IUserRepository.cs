using Domain;
using Repositories.Contracts;

namespace Repositories.Users
{
    public interface IUserRepository : IRepository<User>, IPagedRepository<User>
    {
        bool DoesUsernameExist(string username);

        User? FindByUsernameAndPassword(string username, string password);
    }
}

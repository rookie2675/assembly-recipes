using Domain;

namespace Repositories.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        User? Find(string username, string password);

        List<User> Find(int amount);
    }
}

using Domain;
using Repositories.Users;

namespace Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public User? GetById(long id) => _userRepository.FindById(id);

        public List<User> GetAll() => _userRepository.FindAll();

        public IEnumerable<User> GetPage(int page, int size) => _userRepository.FindPage(page, size);

        public User Add(User user) => _userRepository.Add(user);

        public User Update(User user) => _userRepository.Update(user);

        public User Delete(long id) => _userRepository.Delete(id);
    }
}
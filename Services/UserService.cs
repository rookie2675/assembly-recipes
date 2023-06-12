using Domain;

using Repositories.Contracts;

using Services.Contracts;


namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public User Find(long id) => _userRepository.Find(id);

        public List<User> Find() => _userRepository.Find();

        public User Add(User user) => _userRepository.Add(user);

        public User Update(User user) => _userRepository.Update(user);

        public User Delete(long id) => _userRepository.Delete(id);
    }
}

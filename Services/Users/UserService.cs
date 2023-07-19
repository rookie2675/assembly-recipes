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

        public IEnumerable<User> GetPage(int page, int size)
        {
            if (page <= 0 || size <= 0)
                throw new ArgumentException("Page and size parameters should be greater than zero.");

            return _userRepository.FindPage(page, size);
        }

        public User Add(User user)
        {
            if (_userRepository.DoesUsernameExist(user.Username))
                throw new ArgumentException("Username already exists");

            if (_userRepository.DoesEmailExist(user.Email))
                throw new ArgumentException("Email already exists");

            return _userRepository.Add(user);
        }

        public User Update(User user)
        {
            if (user.Id is not null && _userRepository.FindById(user.Id.Value) is null)
                throw new ArgumentException($"User with ID {user.Id} not found.");

            return _userRepository.Update(user);
        }

        public User Delete(long id)
        {
            var existingUser = _userRepository.FindById(id);

            if (existingUser is null)
                throw new ArgumentException($"User with ID {id} not found.");

            return _userRepository.Delete(id);
        }

        public int GetTotalCount() => _userRepository.GetTotalCount();
    }
}
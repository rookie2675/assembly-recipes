using Domain;

using Repositories.Contracts;

using Services.Contracts;


namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository) =>_userRepository = userRepository;

        public User? SignIn(string username, string password) => _userRepository.findByUsernameAndPassword(username, password);
    }
}

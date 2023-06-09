using Domain;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        User? SignIn(string username, string password);
    }
}

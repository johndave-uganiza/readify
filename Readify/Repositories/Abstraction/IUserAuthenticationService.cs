using Readify.Models.Authentication;

namespace Readify.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(Login login);
        Task<Status> RegistrationAsync(Registration registration);
        Task LogoutAsync();
    }
}

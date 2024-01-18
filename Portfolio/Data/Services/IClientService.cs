using Portfolio.Data.Models;

namespace Portfolio.Data.Services
{
    public interface IClientService
    {
        Task<(bool flag, string Message)> RegisterUserAsync(RegistrationModel model);
        Task<(bool flag, string Message)> LoginUserAsync(Login model);
        Task LogoutAsync();
    }
}

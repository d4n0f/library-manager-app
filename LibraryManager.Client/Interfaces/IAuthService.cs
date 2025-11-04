using LibraryManager.Shared.DTOs;

namespace LibraryManager.Client.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterRequest request);

        Task<string?> Login(LoginRequest request);
    }
}

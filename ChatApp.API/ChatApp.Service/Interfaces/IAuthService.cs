using ChatApp.Service.Models.Auth;

namespace ChatApp.Service.Interfaces
{
    public interface IAuthService
    {
        AuthResponse Login(LoginRequest request);
        AuthResponse Register(RegisterRequest request);
    }
}
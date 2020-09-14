using ChatApp.Service.Models.Auth;
using ChatApp.Service.Models.Common.Api;

namespace ChatApp.Service.Interfaces
{
    public interface IAuthService
    {
        OperationResponse Login(LoginRequest request);
        OperationResponse Register(RegisterRequest request);
    }
}
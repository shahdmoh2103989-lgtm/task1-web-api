using task1_web_api.Auth;

namespace task1_web_api.Services
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequest request);

        Task<string> Login(LoginRequest request);
    }
}
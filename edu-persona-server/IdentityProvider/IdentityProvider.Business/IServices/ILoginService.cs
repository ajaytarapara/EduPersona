using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;

namespace IdentityProvider.Business.IServices
{
    public interface ILoginService : IBaseService<User>
    {
        Task<IdpLoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<string> GetAccessTokenAsync(string refreshToken);
        Task<LoginResponse> ValidateSessionAsync(int sessionId);
        Task LogoutAsync(int sessionId);
    }
}
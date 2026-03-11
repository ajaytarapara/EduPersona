using Google.Apis.Auth;
using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;

namespace IdentityProvider.Business.IServices
{
    public interface ILoginService : IBaseService<User>
    {
        Task<IdpLoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<AccessTokenByRefreshTokenResponse> GetAccessTokenAsync(string refreshToken);
        Task<LoginResponse> ValidateSessionAsync(int sessionId);
        Task LogoutAsync(int userId);
        Task<IdpLoginResponse> GoogleLoginAsync(string code);
    }
}
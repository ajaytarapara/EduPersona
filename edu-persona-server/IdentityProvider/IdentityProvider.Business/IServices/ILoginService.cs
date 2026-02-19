using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;

namespace IdentityProvider.Business.IServices
{
    public interface ILoginService : IBaseService<User>
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Models.Request;

namespace IdentityProvider.Business.IServices
{
    public interface IRegisterService : IBaseService<User>
    {
        Task<User> RegisterAsync(RegisterRequest request);
    }
}

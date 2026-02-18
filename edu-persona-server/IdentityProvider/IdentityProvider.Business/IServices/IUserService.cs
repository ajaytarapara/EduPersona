using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Models.Request;

namespace IdentityProvider.Business.IServices
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> RegisterAsync(Register request);
    }
}

using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;

namespace IdentityProvider.Business.IServices
{
    public interface IProfileService : IBaseService<User>
    {
        Task UpdateProfileAsync(int userId, UpdateProfileRequest request);
        Task<BasicProfileResponse> GetUserBasicProfile(int userId);
    }
}

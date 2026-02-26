using EduPersona.Core.Data.Entities;
using EduPersona.Core.Shared.Models.Request;

namespace EduPersona.Core.Business.IServices
{
    public interface IUserProfileService : IBaseService<UserProfile>
    {
        Task<bool> CheckIsProfileCompletedAsync();
        Task CompleteUserProfileAsync(UserProfileRequest userProfileRequest);
    }
}

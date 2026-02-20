using EduPersona.Core.Business.IServices;
using EduPersona.Core.Data.Entities;

namespace EduPersona.Core.Business.Services
{
    public class UserProfileService : BaseService<UserProfile>, IUserProfileService
    {
        public UserProfileService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}

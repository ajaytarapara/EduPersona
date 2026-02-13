using EduPersona.Core.Business.IServices;
using EduPersona.Core.Data.Entities;
using EduPersona.Core.Data.IRepositories;

namespace EduPersona.Core.Business.Services
{
    public class UserProfileService : BaseService<UserProfile>, IUserProfileService
    {
        public UserProfileService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
        }
        
    }
}

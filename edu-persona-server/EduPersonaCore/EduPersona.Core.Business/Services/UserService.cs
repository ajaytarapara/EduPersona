using EduPersona.Core.Business.IServices;
using EduPersona.Core.Data.Entities;
using EduPersona.Core.Data.IRepositories;

namespace EduPersona.Core.Business.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
        }
        
    }
}

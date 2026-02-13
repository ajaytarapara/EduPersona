using EduPersona.Core.Data.Entities;
using EduPersona.Core.Data.IRepositories;
using EduPersona.Core.Data.Repositories.EduPersona.Core.Data.Repositories;

namespace EduPersona.Core.Data.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context): base(context)
        {

        }
    }

}

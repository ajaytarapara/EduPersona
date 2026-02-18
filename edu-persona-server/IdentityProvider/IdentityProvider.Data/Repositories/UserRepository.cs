using IdentityProvider.Data.Entities;
using IdentityProvider.Data.IRepositories;

namespace IdentityProvider.Data.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context): base(context)
        {

        }
    }

}

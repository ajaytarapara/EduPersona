using IdentityProvider.Data.Entities;
using IdentityProvider.Data.IRepositories;

namespace IdentityProvider.Data.Repositories
{
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(AppDbContext context) : base(context)
        {

        }
    }
}
using IdentityProvider.Data.Entities;
using IdentityProvider.Data.IRepositories;

namespace IdentityProvider.Data.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
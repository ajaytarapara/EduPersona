
using IdentityProvider.Data.IRepositories;

namespace IdentityProvider.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IUserRepository? _userRepository;

        private IRefreshTokenRepository _refreshTokenRepository;

        private ISessionRepository _sessionRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Generic repository (simple version, no caching needed)
        public IBaseRepository<T> Repository<T>() where T : class
        {
            return new BaseRepository<T>(_context);
        }

        // Custom repository
        public IUserRepository UserRepository
            => _userRepository ??= new UserRepository(_context);

        public IRefreshTokenRepository RefreshTokenRepository
            => _refreshTokenRepository ??= new RefreshTokenRepository(_context);

        public ISessionRepository SessionRepository
            => _sessionRepository ??= new SessionRepository(_context);

        public int Save() => _context.SaveChanges();

        public Task<int> SaveAsync() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}


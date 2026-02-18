using IdentityProvider.Data.IRepositories;
using System.Collections;

namespace IdentityProvider.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Cache for generic repositories
        private Hashtable? _repositories;

        private IUserRepository? _userRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        #region Generic Repository

        public IBaseRepository<T> Repository<T>() where T : class
        {
            _repositories ??= new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new BaseRepository<T>(_context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IBaseRepository<T>)_repositories[type]!;
        }

        #endregion

        #region Custom Repository

        public IUserRepository UserRepository
            => _userRepository ??= new UserRepository(_context);

        #endregion

        #region Save

        public int Save()
            => _context.SaveChanges();

        public Task<int> SaveAsync()
            => _context.SaveChangesAsync();

        #endregion

        #region Dispose

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}

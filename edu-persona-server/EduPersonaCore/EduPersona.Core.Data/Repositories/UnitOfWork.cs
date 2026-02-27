using EduPersona.Core.Data.IRepositories;
using EduPersona.Core.Data.Repositories.EduPersona.Core.Data.Repositories;

namespace EduPersona.Core.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IUserProfileRepository? _userRepository;

        private IDesignationRepository? _designationRepository;

        private IProfessionRepository? _professionRepository;

        private ISkillRepository? _skillRepository;

        private IUserDesignationRepository? _userDesignationRepository;

        private IUserSkillRepository? _userSkillRepository;

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
        public IUserProfileRepository UserRepository
            => _userRepository ??= new UserProfileRepository(_context);

        public IDesignationRepository DesignationRepository
          => _designationRepository ??= new DesignationRepository(_context);

        public IProfessionRepository ProfessionRepository
        => _professionRepository ??= new ProfessionRepository(_context);

        public ISkillRepository SkillRepository
          => _skillRepository ??= new SkillRepository(_context);

        public IUserDesignationRepository UserDesignationRepository
        => _userDesignationRepository ??= new UserDesignationRepository(_context);

        public IUserSkillRepository UserSkillRepository
          => _userSkillRepository ??= new UserSkillRepository(_context);

        public int Save() => _context.SaveChanges();

        public Task<int> SaveAsync() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

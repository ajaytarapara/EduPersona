using EduPersona.Core.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> Repository<T>() where T : class;

    IUserProfileRepository UserRepository { get; }

    IDesignationRepository DesignationRepository { get; }

    IProfessionRepository ProfessionRepository { get; }

    ISkillRepository SkillRepository { get; }

    IUserDesignationRepository UserDesignationRepository { get; }

    IUserSkillRepository UserSkillRepository { get; }

    int Save();
    Task<int> SaveAsync();
}

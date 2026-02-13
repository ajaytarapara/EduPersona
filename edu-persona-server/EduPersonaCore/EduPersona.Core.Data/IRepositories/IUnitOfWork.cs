using EduPersona.Core.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> Repository<T>() where T : class;

    IUserProfileRepository UserRepository { get; }

    int Save();
    Task<int> SaveAsync();
}

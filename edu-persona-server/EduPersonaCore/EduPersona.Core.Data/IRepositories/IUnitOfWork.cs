using EduPersona.Core.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> Repository<T>() where T : class;

    IUserRepository UserRepository { get; }

    int Save();
    Task<int> SaveAsync();
}

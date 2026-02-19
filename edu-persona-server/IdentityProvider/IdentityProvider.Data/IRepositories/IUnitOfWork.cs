
using IdentityProvider.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> Repository<T>() where T : class;

    IUserRepository UserRepository { get; }

    IRefreshTokenRepository RefreshTokenRepository { get; }

    ISessionRepository SessionRepository { get; }

    int Save();
    Task<int> SaveAsync();
}

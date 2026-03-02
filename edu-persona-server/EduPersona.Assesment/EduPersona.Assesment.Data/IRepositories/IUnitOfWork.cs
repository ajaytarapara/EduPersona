namespace EduPersona.Assesment.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> Repository<T>() where T : class;

        int Save();
        Task<int> SaveAsync();
    }
}
using System.Linq.Expressions;

namespace EduPersona.Core.Data.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        // Add
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        // Update
        Task<T> UpdateAsync(T entity);

        // Delete
        Task DeleteAsync(int id);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);

        // Get by Id
        Task<T?> GetByIdAsync(int id);

        // Get All
        Task<IEnumerable<T>> GetAllAsync();

        // Find with predicate
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Get with includes
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,Func<IQueryable<T>,
            IQueryable<T>>? include = null
        );

        // Get first or default with includes
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>>? include = null
        );

        // Check existence
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }

}

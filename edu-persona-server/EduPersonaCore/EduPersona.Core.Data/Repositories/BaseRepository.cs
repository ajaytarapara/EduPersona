using EduPersona.Core.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EduPersona.Core.Data.Repositories
{
    namespace EduPersona.Core.Data.Repositories
    {
        public class BaseRepository<T> : IBaseRepository<T> where T : class
        {
            protected readonly AppDbContext _context;
            protected readonly DbSet<T> _dbSet;

            public BaseRepository(AppDbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            #region Add

            public async Task<T> AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
                return entity;
            }

            public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
            {
                await _dbSet.AddRangeAsync(entities);
                return entities;
            }

            #endregion

            #region Update

            public Task<T> UpdateAsync(T entity)
            {
                _dbSet.Update(entity);
                return Task.FromResult(entity);
            }

            #endregion

            #region Delete

            public async Task DeleteAsync(int id)
            {
                var entity = await GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException("Entity not found");

                _dbSet.Remove(entity);
            }

            public Task DeleteAsync(T entity)
            {
                _dbSet.Remove(entity);
                return Task.CompletedTask;
            }

            public Task DeleteRangeAsync(IEnumerable<T> entities)
            {
                _dbSet.RemoveRange(entities);
                return Task.CompletedTask;
            }

            #endregion

            #region Get

            public async Task<T?> GetByIdAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }

            public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }

            public async Task<IEnumerable<T>> GetAsync(
                Expression<Func<T, bool>>? predicate = null,
                Func<IQueryable<T>, IQueryable<T>>? include = null)
            {
                IQueryable<T> query = _dbSet;

                if (include != null)
                    query = include(query);

                if (predicate != null)
                    query = query.Where(predicate);

                return await query.ToListAsync();
            }

            public async Task<T?> GetFirstOrDefaultAsync(
                Expression<Func<T, bool>> predicate,
                Func<IQueryable<T>, IQueryable<T>>? include = null)
            {
                IQueryable<T> query = _dbSet;

                if (include != null)
                    query = include(query);

                return await query.FirstOrDefaultAsync(predicate);
            }

            public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.AnyAsync(predicate);
            }

            #endregion
        }
    }
}

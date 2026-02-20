using EduPersona.Core.Business.IServices;
using EduPersona.Core.Data.IRepositories;
using System.Linq.Expressions;

namespace EduPersona.Core.Business.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected readonly IBaseRepository<T> _repository;
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<T>();
        }

        #region Add

        public async Task<T> AddAsync(T entity)
        {
            var result = await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return result;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            var result = await _repository.AddRangeAsync(entities);
            await _unitOfWork.SaveAsync();
            return result;
        }

        #endregion

        #region Update

        public async Task<T> UpdateAsync(T entity)
        {
            var result = await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
            return result;
        }

        #endregion

        #region Delete

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            await _repository.DeleteRangeAsync(entities);
            await _unitOfWork.SaveAsync();
        }

        #endregion

        #region Get

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            return await _repository.GetAsync(predicate, include);
        }

        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            return await _repository.GetFirstOrDefaultAsync(predicate, include);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }

        #endregion
    }
}

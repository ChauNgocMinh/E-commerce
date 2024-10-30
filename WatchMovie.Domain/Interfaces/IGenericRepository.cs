using System.Linq.Expressions;

namespace WatchMovie.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}

using System.Linq.Expressions;

namespace GridWallManagement.App.Repository.Abstract
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        ValueTask<T> GetByIdAsync(object key);
        Task<T> CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entity);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entity);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveAsync(int userId, CancellationToken cancellationToken = default);
    }
}

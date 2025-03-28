using GridWallManagement.App.Database.DBContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GridWallManagement.App.Repository.Common
{
    /// <summary>
    /// Generic Repository For EF Core
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : AbstractGenericRepository<T>, IGenericRepository<T> where T : class
    {
        private readonly DataBaseContext _context;
        public GenericRepository(DataBaseContext context)
        {
            _context = context;
        }
        protected DataBaseContext Context => _context;

        public override ValueTask<T> GetByIdAsync(object key)
            => _context.FindAsync<T>(new object[] { key });

        public override ValueTask<T> GetByIdAsync(object[] key)
            => _context.FindAsync<T>(key);

        public override Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().FirstOrDefaultAsync(predicate);

        public override IQueryable<T> GetQueryable()
            => _context.Set<T>().AsQueryable<T>();

        protected override async Task<T> PerformAdd(T entity)
            => (await _context.AddAsync(entity)).Entity;

        protected override async Task PerformAddRange(IEnumerable<T> entities)
            => await _context.AddRangeAsync(entities.ToArray());

        protected override T PerformUpdate(T entity)
            => _context.Update(entity).Entity;

        protected override void PerformDelete(T entity)
        {
            if (entity is ISoftDeletable softDeletable)
                softDeletable.Delete();
            else
                _context.Remove(entity);
        }

        protected override async Task<T> PerformUpsert(object id, T entity)
        {
            var txn = await _context.Database.BeginTransactionAsync();
            try
            {
                var oldEntity = await GetByIdAsync(id);
                if (oldEntity != null)
                {
                    _context.Remove(oldEntity);
                    await _context.SaveChangesAsync();
                }
                var result = await AddAsync(entity);
                await txn.CommitAsync();
                return result;
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }            
        }

        protected override async Task<T> PerformUpsert(object[] id, T entity)
        {
            var txn = await _context.Database.BeginTransactionAsync();
            try
            {
                var oldEntity = await GetByIdAsync(id);
                if (oldEntity != null)
                {
                    _context.Remove(oldEntity);
                    await _context.SaveChangesAsync();
                }
                var result = await AddAsync(entity);
                await txn.CommitAsync();
                return result;
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }
        }

        protected override Task SaveAsync()
            => _context.SaveChangesAsync();

        public override Task<TEntity> GetFirst<TEntity>(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public override async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().AnyAsync(predicate);
    }
}

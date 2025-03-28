using GridWall.App.Database.DBContexts;
using GridWallManagement.App.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GridWallManagement.App.Repository.Concrete
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataBaseContext DatabaseContext { get; set; }
        public RepositoryBase(DataBaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }
        public IQueryable<T> GetAll()
        {
            return this.DatabaseContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DatabaseContext.Set<T>().Where(expression).AsNoTracking();
        }
        public async ValueTask<T> GetByIdAsync(object key)
        {
            return await this.DatabaseContext.FindAsync<T>(new object[] { key });
        }
        public async Task<T> CreateAsync(T entity)
        {
            var entityResult = (await this.DatabaseContext.Set<T>().AddAsync(entity)).Entity;
            await this.DatabaseContext.SaveChangesAsync();
            return entityResult;
        }
        public async Task CreateRangeAsync(IEnumerable<T> entity)
        {
            await this.DatabaseContext.Set<T>().AddRangeAsync(entity);
            await this.DatabaseContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            this.DatabaseContext.Set<T>().Update(entity);
            await this.DatabaseContext.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(IEnumerable<T> entity)
        {
            this.DatabaseContext.Set<T>().UpdateRange(entity);
            await this.DatabaseContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            this.DatabaseContext.Set<T>().Remove(entity);
            await this.DatabaseContext.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync(IEnumerable<T> entity)
        {
            this.DatabaseContext.Set<T>().RemoveRange(entity);
            await this.DatabaseContext.SaveChangesAsync();
        }
        public Task<int> CountAsync()
        {
            return GetAll().CountAsync();
        }
        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return GetByCondition(predicate).CountAsync();
        }
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return GetAll().AnyAsync(predicate);
        }

        public async Task BeginTransactionAsync()
        {
           await this.DatabaseContext.Database.BeginTransactionAsync();
        }


        public async Task RollbackTransactionAsync()
        {
            await this.DatabaseContext.Database.RollbackTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await this.DatabaseContext.Database.CommitTransactionAsync();
        }

        public async Task SaveAsync(int userId, CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules(userId);
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInfoRules(int UserId)
        {
            DateTime timeStamp = DateTime.UtcNow;
            this.DatabaseContext.ChangeTracker
                  .Entries()
                  .Where(e =>
                      (e.State == EntityState.Added ||
                       e.State == EntityState.Modified))
                  .ToList()
                  .ForEach(entry =>
                  {
                      var entity = entry.Entity;

                      if (entry.State == EntityState.Added)
                      {
                          entry.Property("CreatedDate").CurrentValue = timeStamp;
                          entry.Property("CreatedBy").CurrentValue = UserId;
                      }
                      else
                      {
                          entry.Property("ModifiedDate").CurrentValue = timeStamp;
                          entry.Property("ModifiedBy").CurrentValue = UserId;
                      }
                  });
        }
    }
}

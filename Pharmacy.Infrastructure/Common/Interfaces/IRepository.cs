using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Records { get; }

        Task<IDbContextTransaction> BeginTransaction();

        ValueTask<TEntity> GetByIdAsync(long id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        IQueryable<TEntity> GetAllQueryable();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        ValueTask<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity obj);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task SaveAsync();
    }
}

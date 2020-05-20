using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitTransactionAsync(IDbContextTransaction transaction);

        Task RollbackTransactionAsync(IDbContextTransaction transaction);

        Task Create(TEntity entity);

        Task Create(IEnumerable<TEntity> entities);

        IQueryable<TEntity> GetAllQueryable();

        ValueTask<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate);

        ValueTask<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task Delete(TEntity entity);

        Task Delete(IEnumerable<TEntity> entities);

        Task Update(TEntity entity);

        Task Update(IEnumerable<TEntity> entities);

        IQueryable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}

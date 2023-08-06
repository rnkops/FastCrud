using System.Linq.Expressions;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public partial interface IRRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    Task<TEntity?> FindAsync(TId id, CancellationToken cancellationToken = default);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, CancellationToken cancellationToken = default);
    Task<TEntity[]> GetAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>;
    Task<TFields[]> GetAsync<TFields, TQuery>(Expression<Func<TEntity, TFields>> selector, TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>;
    Task<TEntity[]> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25, CancellationToken cancellationToken = default);
    Task<TEntity[]> GetFilteredAndOrderedAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default);
    Task<TEntity[]> GetFilteredAndOrderedDescendinglyAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default);
    Task<TFields[]> GetFilteredAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25, CancellationToken cancellationToken = default);
    Task<TFields[]> GetFilteredAndOrderedAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default);
    Task<TFields[]> GetFilteredAndOrderedDescendinglyAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>;
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>;
}

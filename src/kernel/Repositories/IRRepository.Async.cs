using System.Linq.Expressions;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public partial interface IRRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    Task<TEntity?> FindAsync(TId id);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order);
    Task<TEntity[]> GetAsync<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
    Task<TFields[]> GetAsync<TFields, TQuery>(Expression<Func<TEntity, TFields>> selector, TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
    Task<TEntity[]> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25);
    Task<TEntity[]> GetFilteredAndOrderedAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    Task<TEntity[]> GetFilteredAndOrderedDescendinglyAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    Task<TFields[]> GetFilteredAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25);
    Task<TFields[]> GetFilteredAndOrderedAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    Task<TFields[]> GetFilteredAndOrderedDescendinglyAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> ExistsAsync<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> CountAsync<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
}

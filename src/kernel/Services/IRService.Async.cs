using System.Linq.Expressions;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public partial interface IRService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    Task<TEntity?> FindAsync(TId id);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order);
    Task<TEntity[]> GetAsync<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
    Task<TEntity[]> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25);
    Task<TEntity[]> GetFilteredAndOrderedAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    Task<TEntity[]> GetFilteredAndOrderedDescendinglyAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    Task<TResponse?> FindAsync<TResponse>(TId id) where TResponse : BaseResponse<TEntity, TId>, new();
    Task<TResponse?> FirstOrDefaultAsync<TResponse>(Expression<Func<TEntity, bool>> predicate) where TResponse : BaseResponse<TEntity, TId>, new();
    Task<TResponse?> FirstOrDefaultAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order) where TResponse : BaseResponse<TEntity, TId>, new();
    Task<TResponse[]> GetAsync<TResponse, TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId> where TResponse : BaseResponse<TEntity, TId>, new();
    Task<TResponse[]> GetFilteredAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new();
    Task<TResponse[]> GetFilteredAndOrderedAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new();
    Task<TResponse[]> GetFilteredAndOrderedDescendinglyAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new();
    Task<TFields[]> GetAsync<TFields, TQuery>(Expression<Func<TEntity, TFields>> selector, TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
    Task<TFields[]> GetFilteredAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25);
    Task<TFields[]> GetFilteredAndOrderedAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    Task<TFields[]> GetFilteredAndOrderedDescendinglyAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> ExistsAsync<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> CountAsync<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
}

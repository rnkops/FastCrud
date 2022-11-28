using System.Linq.Expressions;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public partial interface IRService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    TEntity? Find(TId id);
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order);
    TEntity[] Get<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
    TEntity[] GetFiltered(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25);
    TEntity[] GetFilteredAndOrdered(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    TEntity[] GetFilteredAndOrderedDescendingly(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    TResponse? Find<TResponse>(TId id) where TResponse : BaseResponse<TEntity, TId>, new();
    TResponse? FirstOrDefault<TResponse>(Expression<Func<TEntity, bool>> predicate) where TResponse : BaseResponse<TEntity, TId>, new();
    TResponse? FirstOrDefault<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order) where TResponse : BaseResponse<TEntity, TId>, new();
    TResponse[] Get<TResponse, TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId> where TResponse : BaseResponse<TEntity, TId>, new();
    TResponse[] GetFiltered<TResponse>(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new();
    TResponse[] GetFilteredAndOrdered<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new();
    TResponse[] GetFilteredAndOrderedDescendingly<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new();
    TFields[] Get<TFields, TQuery>(Expression<Func<TEntity, TFields>> selector, TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
    TFields[] GetFiltered<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25);
    TFields[] GetFilteredAndOrdered<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    TFields[] GetFilteredAndOrderedDescendingly<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25);
    bool Exists(Expression<Func<TEntity, bool>> predicate);
    bool Exists<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
    int Count(Expression<Func<TEntity, bool>> predicate);
    int Count<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>;
}

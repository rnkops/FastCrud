using System.Linq.Expressions;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public partial class RService<TEntity, TId> : IRService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => Repository.CountAsync(predicate, cancellationToken);

    public virtual Task<int> CountAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.CountAsync(query, cancellationToken);

    public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => Repository.ExistsAsync(predicate, cancellationToken);

    public virtual Task<bool> ExistsAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.ExistsAsync(query, cancellationToken);

    public virtual Task<TEntity?> FindAsync(TId id, CancellationToken cancellationToken = default)
        => Repository.FindAsync(id, cancellationToken);

    public virtual async Task<TResponse?> FindAsync<TResponse>(TId id, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entity = await FindAsync(id, cancellationToken);
        if (entity is null)
            return null;
        var response = new TResponse();
        response.Set(entity);
        await OnBeforeResponseReturnedAsync(response, cancellationToken);
        return response;
    }

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => Repository.FirstOrDefaultAsync(predicate, cancellationToken);

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, CancellationToken cancellationToken = default)
        => Repository.FirstOrDefaultAsync(predicate, order, cancellationToken);

    public virtual async Task<TResponse?> FirstOrDefaultAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entity = await FirstOrDefaultAsync(predicate, cancellationToken);
        if (entity is null)
            return null;
        var response = new TResponse();
        response.Set(entity);
        await OnBeforeResponseReturnedAsync(response, cancellationToken);
        return response;
    }

    public virtual async Task<TResponse?> FirstOrDefaultAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entity = await FirstOrDefaultAsync(predicate, order, cancellationToken);
        if (entity is null)
            return null;
        var response = new TResponse();
        response.Set(entity);
        await OnBeforeResponseReturnedAsync(response, cancellationToken);
        return response;
    }

    public virtual Task<TEntity[]> GetAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.GetAsync(query);

    public virtual async Task<TResponse[]> GetAsync<TResponse, TQuery>(TQuery query, CancellationToken cancellationToken = default)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TQuery : BaseGetQuery<TEntity, TId>
    {
        var entities = await GetAsync(query, cancellationToken);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = new TResponse[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            var response = new TResponse();
            response.Set(entities[i]);
            responses[i] = response;
        }
        await OnBeforeResponseReturnedAsync(responses, cancellationToken);
        return responses;
    }

    public virtual Task<TFields[]> GetAsync<TFields, TQuery>(Expression<Func<TEntity, TFields>> selector, TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.GetAsync(selector, query, cancellationToken);

    public virtual Task<TEntity[]> GetFilteredAndOrderedAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Repository.GetFilteredAndOrderedAsync(predicate, order, offset, limit, cancellationToken);

    public virtual async Task<TResponse[]> GetFilteredAndOrderedAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entities = await GetFilteredAndOrderedAsync(predicate, order, offset, limit, cancellationToken);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = entities.Select(entity =>
        {
            var response = new TResponse();
            response.Set(entity);
            return response;
        }).ToArray();
        await OnBeforeResponseReturnedAsync(responses, cancellationToken);
        return responses;
    }

    public virtual Task<TFields[]> GetFilteredAndOrderedAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Repository.GetFilteredAndOrderedAsync(selector, predicate, order, offset, limit, cancellationToken);

    public virtual Task<TEntity[]> GetFilteredAndOrderedDescendinglyAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Repository.GetFilteredAndOrderedDescendinglyAsync(predicate, order, offset, limit, cancellationToken);

    public virtual async Task<TResponse[]> GetFilteredAndOrderedDescendinglyAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entities = await GetFilteredAndOrderedDescendinglyAsync(predicate, order, offset, limit, cancellationToken);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = entities.Select(entity =>
        {
            var response = new TResponse();
            response.Set(entity);
            return response;
        }).ToArray();
        await OnBeforeResponseReturnedAsync(responses, cancellationToken);
        return responses;
    }

    public virtual Task<TFields[]> GetFilteredAndOrderedDescendinglyAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Repository.GetFilteredAndOrderedDescendinglyAsync(selector, predicate, order, offset, limit, cancellationToken);

    public virtual Task<TEntity[]> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Repository.GetFilteredAsync(predicate, offset, limit, cancellationToken);

    public virtual async Task<TResponse[]> GetFilteredAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entities = await GetFilteredAsync(predicate, offset, limit, cancellationToken);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = entities.Select(entity =>
        {
            var response = new TResponse();
            response.Set(entity);
            return response;
        }).ToArray();
        await OnBeforeResponseReturnedAsync(responses, cancellationToken);
        return responses;
    }

    public virtual Task<TFields[]> GetFilteredAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Repository.GetFilteredAsync(selector, predicate, offset, limit);

    protected virtual Task OnBeforeResponseReturnedAsync<TResponse>(TResponse response, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnBeforeResponseReturnedAsync<TResponse>(IEnumerable<TResponse> responses, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>
    {
        return Task.CompletedTask;
    }
}

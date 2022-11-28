using System.Linq.Expressions;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public partial class RService<TEntity, TId> : IRService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        => Repository.CountAsync(predicate);

    public virtual Task<int> CountAsync<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.CountAsync(query);

    public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        => Repository.ExistsAsync(predicate);

    public virtual Task<bool> ExistsAsync<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.ExistsAsync(query);

    public virtual Task<TEntity?> FindAsync(TId id)
        => Repository.FindAsync(id);

    public virtual async Task<TResponse?> FindAsync<TResponse>(TId id) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entity = await FindAsync(id);
        if (entity is null)
            return null;
        var response = new TResponse();
        response.Set(entity);
        await OnBeforeResponseReturnedAsync(response);
        return response;
    }

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        => Repository.FirstOrDefaultAsync(predicate);

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order)
        => Repository.FirstOrDefaultAsync(predicate, order);

    public virtual async Task<TResponse?> FirstOrDefaultAsync<TResponse>(Expression<Func<TEntity, bool>> predicate) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entity = await FirstOrDefaultAsync(predicate);
        if (entity is null)
            return null;
        var response = new TResponse();
        response.Set(entity);
        await OnBeforeResponseReturnedAsync(response);
        return response;
    }

    public virtual async Task<TResponse?> FirstOrDefaultAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entity = await FirstOrDefaultAsync(predicate, order);
        if (entity is null)
            return null;
        var response = new TResponse();
        response.Set(entity);
        await OnBeforeResponseReturnedAsync(response);
        return response;
    }

    public virtual Task<TEntity[]> GetAsync<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.GetAsync(query);

    public virtual async Task<TResponse[]> GetAsync<TResponse, TQuery>(TQuery query)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TQuery : BaseGetQuery<TEntity, TId>
    {
        var entities = await GetAsync(query);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = new TResponse[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            var response = new TResponse();
            response.Set(entities[i]);
            responses[i] = response;
        }
        await OnBeforeResponseReturnedAsync(responses);
        return responses;
    }

    public virtual Task<TFields[]> GetAsync<TFields, TQuery>(Expression<Func<TEntity, TFields>> selector, TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.GetAsync(selector, query);

    public virtual Task<TEntity[]> GetFilteredAndOrderedAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Repository.GetFilteredAndOrderedAsync(predicate, order, offset, limit);

    public virtual async Task<TResponse[]> GetFilteredAndOrderedAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entities = await GetFilteredAndOrderedAsync(predicate, order, offset, limit);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = entities.Select(entity =>
        {
            var response = new TResponse();
            response.Set(entity);
            return response;
        }).ToArray();
        await OnBeforeResponseReturnedAsync(responses);
        return responses;
    }

    public virtual Task<TFields[]> GetFilteredAndOrderedAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Repository.GetFilteredAndOrderedAsync(selector, predicate, order, offset, limit);

    public virtual Task<TEntity[]> GetFilteredAndOrderedDescendinglyAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Repository.GetFilteredAndOrderedDescendinglyAsync(predicate, order, offset, limit);

    public virtual async Task<TResponse[]> GetFilteredAndOrderedDescendinglyAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entities = await GetFilteredAndOrderedDescendinglyAsync(predicate, order, offset, limit);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = entities.Select(entity =>
        {
            var response = new TResponse();
            response.Set(entity);
            return response;
        }).ToArray();
        await OnBeforeResponseReturnedAsync(responses);
        return responses;
    }

    public virtual Task<TFields[]> GetFilteredAndOrderedDescendinglyAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Repository.GetFilteredAndOrderedDescendinglyAsync(selector, predicate, order, offset, limit);

    public virtual Task<TEntity[]> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25)
        => Repository.GetFilteredAsync(predicate, offset, limit);

    public virtual async Task<TResponse[]> GetFilteredAsync<TResponse>(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entities = await GetFilteredAsync(predicate, offset, limit);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = entities.Select(entity =>
        {
            var response = new TResponse();
            response.Set(entity);
            return response;
        }).ToArray();
        await OnBeforeResponseReturnedAsync(responses);
        return responses;
    }

    public virtual Task<TFields[]> GetFilteredAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25)
        => Repository.GetFilteredAsync(selector, predicate, offset, limit);

    protected virtual Task OnBeforeResponseReturnedAsync<TResponse>(TResponse response) where TResponse : BaseResponse<TEntity, TId>
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnBeforeResponseReturnedAsync<TResponse>(IEnumerable<TResponse> responses) where TResponse : BaseResponse<TEntity, TId>
    {
        return Task.CompletedTask;
    }
}

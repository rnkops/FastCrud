using System.Linq.Expressions;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;

namespace FastCrud.Kernel.Services;

public partial class RService<TEntity, TId> : IRService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    protected readonly IRRepository<TEntity, TId> Repository;

    public RService(IRRepository<TEntity, TId> repository)
    {
        Repository = repository;
    }

    public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        => Repository.Count(predicate);

    public virtual int Count<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.Count(query);

    public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        => Repository.Exists(predicate);

    public virtual bool Exists<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.Exists(query);

    public virtual TEntity? Find(TId id)
        => Repository.Find(id);

    public virtual TResponse? Find<TResponse>(TId id) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entity = Repository.Find(id);
        if (entity is null)
            return null;
        var response = new TResponse();
        response.Set(entity);
        OnBeforeResponseReturned(response);
        return response;
    }

    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        => Repository.FirstOrDefault(predicate);

    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order)
        => Repository.FirstOrDefault(predicate, order);

    public virtual TResponse? FirstOrDefault<TResponse>(Expression<Func<TEntity, bool>> predicate) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entity = Repository.FirstOrDefault(predicate);
        if (entity is null)
            return null;
        var response = new TResponse();
        response.Set(entity);
        OnBeforeResponseReturned(response);
        return response;
    }

    public virtual TResponse? FirstOrDefault<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entity = Repository.FirstOrDefault(predicate, order);
        if (entity is null)
            return null;
        var response = new TResponse();
        response.Set(entity);
        OnBeforeResponseReturned(response);
        return response;
    }

    public virtual TEntity[] Get<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.Get(query);

    public virtual TResponse[] Get<TResponse, TQuery>(TQuery query)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TQuery : BaseGetQuery<TEntity, TId>
    {
        var entities = Repository.Get(query);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = new TResponse[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            var response = new TResponse();
            response.Set(entities[i]);
            responses[i] = response;
        }
        OnBeforeResponseReturned(responses);
        return responses;
    }

    public virtual TFields[] Get<TFields, TQuery>(Expression<Func<TEntity, TFields>> selector, TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
        => Repository.Get(selector, query);

    public virtual TEntity[] GetFiltered(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25)
        => Repository.GetFiltered(predicate, offset, limit);

    public virtual TResponse[] GetFiltered<TResponse>(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entities = Repository.GetFiltered(predicate, offset, limit);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = new TResponse[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            var response = new TResponse();
            response.Set(entities[i]);
            responses[i] = response;
        }
        OnBeforeResponseReturned(responses);
        return responses;
    }

    public virtual TFields[] GetFiltered<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25)
        => Repository.GetFiltered(selector, predicate, offset, limit);

    public virtual TEntity[] GetFilteredAndOrdered(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Repository.GetFilteredAndOrdered(predicate, order, offset, limit);

    public virtual TResponse[] GetFilteredAndOrdered<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entities = Repository.GetFilteredAndOrdered(predicate, order, offset, limit);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = new TResponse[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            var response = new TResponse();
            response.Set(entities[i]);
            responses[i] = response;
        }
        OnBeforeResponseReturned(responses);
        return responses;
    }

    public virtual TFields[] GetFilteredAndOrdered<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Repository.GetFilteredAndOrdered(selector, predicate, order, offset, limit);

    public virtual TEntity[] GetFilteredAndOrderedDescendingly(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Repository.GetFilteredAndOrderedDescendingly(predicate, order, offset, limit);

    public virtual TResponse[] GetFilteredAndOrderedDescendingly<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25) where TResponse : BaseResponse<TEntity, TId>, new()
    {
        var entities = Repository.GetFilteredAndOrderedDescendingly(predicate, order, offset, limit);
        if (entities.Length == 0)
            return Array.Empty<TResponse>();
        var responses = new TResponse[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            var response = new TResponse();
            response.Set(entities[i]);
            responses[i] = response;
        }
        OnBeforeResponseReturned(responses);
        return responses;
    }

    public virtual TFields[] GetFilteredAndOrderedDescendingly<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Repository.GetFilteredAndOrderedDescendingly(selector, predicate, order, offset, limit);

    protected virtual void OnBeforeResponseReturned<TResponse>(TResponse response) where TResponse : BaseResponse<TEntity, TId>
    {
    }

    protected virtual void OnBeforeResponseReturned<TResponse>(IEnumerable<TResponse> responses) where TResponse : BaseResponse<TEntity, TId>
    {
    }
}

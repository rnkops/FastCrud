using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;

namespace FastCrud.Kernel.Services;

public class CRService<TEntity, TId> : RService<TEntity, TId>, ICService<TEntity, TId>, IRService<TEntity, TId>, ICRService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    public CRService(ICRRepository<TEntity, TId> repository) : base(repository)
    {
    }

    public virtual TResponse Add<TResponse, TRequest>(TRequest request)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseCreateRequest<TEntity, TId>
    {
        var entity = request.ToEntity();
        ((ICRepository<TEntity, TId>)Repository).Add(entity);
        var response = new TResponse();
        response.Set(entity);
        return response;
    }

    public virtual TResponse[] Add<TResponse, TRequest>(IEnumerable<TRequest> request)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseCreateRequest<TEntity, TId>
    {
        var entities = request.Select(x => x.ToEntity()).ToArray();
        ((ICRepository<TEntity, TId>)Repository).Add(entities);
        var res = new TResponse[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            res[i] = new TResponse();
            res[i].Set(entities[i]);
        }
        return res;
    }

    public virtual TEntity Add<TRequest>(TRequest request) where TRequest : BaseCreateRequest<TEntity, TId>
    {
        var entity = request.ToEntity();
        ((ICRepository<TEntity, TId>)Repository).Add(entity);
        return entity;
    }

    public virtual TEntity[] Add<TRequest>(IEnumerable<TRequest> request) where TRequest : BaseCreateRequest<TEntity, TId>
    {
        var entities = request.Select(x => x.ToEntity()).ToArray();
        ((ICRepository<TEntity, TId>)Repository).Add(entities);
        return entities;
    }

    public virtual void Add(TEntity entity)
        => ((ICRepository<TEntity, TId>)Repository).Add(entity);

    public virtual void Add(IEnumerable<TEntity> entities)
        => ((ICRepository<TEntity, TId>)Repository).Add(entities);

    public virtual async Task<TResponse> AddAsync<TResponse, TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseCreateRequest<TEntity, TId>
    {
        var entity = request.ToEntity();
        await ((ICRepository<TEntity, TId>)Repository).AddAsync(entity, cancellationToken);
        var response = new TResponse();
        response.Set(entity);
        return response;
    }

    public virtual async Task<TResponse[]> AddAsync<TResponse, TRequest>(IEnumerable<TRequest> request, CancellationToken cancellationToken = default)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseCreateRequest<TEntity, TId>
    {
        var entities = request.Select(x => x.ToEntity()).ToArray();
        await ((ICRepository<TEntity, TId>)Repository).AddAsync(entities, cancellationToken);
        var res = new TResponse[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            res[i] = new TResponse();
            res[i].Set(entities[i]);
        }
        return res;
    }

    public virtual async Task<TEntity> AddAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : BaseCreateRequest<TEntity, TId>
    {
        var entity = request.ToEntity();
        await ((ICRepository<TEntity, TId>)Repository).AddAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity[]> AddAsync<TRequest>(IEnumerable<TRequest> request, CancellationToken cancellationToken = default) where TRequest : BaseCreateRequest<TEntity, TId>
    {
        var entities = request.Select(x => x.ToEntity()).ToArray();
        await ((ICRepository<TEntity, TId>)Repository).AddAsync(entities, cancellationToken);
        return entities;
    }

    public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => ((ICRepository<TEntity, TId>)Repository).AddAsync(entity, cancellationToken);

    public virtual Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => ((ICRepository<TEntity, TId>)Repository).AddAsync(entities, cancellationToken);

    public virtual int SaveChanges()
        => ((ICRepository<TEntity, TId>)Repository).SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => ((ICRepository<TEntity, TId>)Repository).SaveChangesAsync(cancellationToken);
}

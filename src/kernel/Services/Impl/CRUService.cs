using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;

namespace FastCrud.Kernel.Services;

public class CRUService<TEntity, TId> : CRService<TEntity, TId>, ICRService<TEntity, TId>, IRUService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    public CRUService(ICRURepository<TEntity, TId> repository) : base(repository)
    {
    }

    public override int SaveChanges()
        => ((ICRepository<TEntity, TId>)Repository).SaveChanges();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => ((ICRepository<TEntity, TId>)Repository).SaveChangesAsync(cancellationToken);

    public TResponse? Update<TResponse, TRequest>(TRequest request)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        var entity = Repository.Find(request.Id);
        if (entity is null)
            return null;
        request.UpdateEntity(entity);
        ((IRURepository<TEntity, TId>)Repository).Update(entity);
        var res = new TResponse();
        res.Set(entity);
        return res;
    }

    public TResponse[]? Update<TResponse, TRequest>(IEnumerable<TRequest> request)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        var ids = request.Select(x => x.Id).ToArray();
        var entities = Repository.GetFiltered(x => ids.Contains(x.Id), 0, ids.Length);
        if (entities.Length != ids.Length)
            return null;
        var res = new TResponse[entities.Length];
        var toUpdate = new List<TEntity>();
        for (var i = 0; i < entities.Length; i++)
        {
            request.First(x => x.Id!.Equals(entities[i].Id)).UpdateEntity(entities[i]);
            toUpdate.Add(entities[i]);
            res[i] = new TResponse();
            res[i].Set(entities[i]);
        }
        ((IRURepository<TEntity, TId>)Repository).Update(toUpdate);
        return res;
    }

    public TEntity? Update<TRequest>(TRequest request) where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        var entity = Repository.Find(request.Id);
        if (entity is null)
            return null;
        request.UpdateEntity(entity);
        ((IRURepository<TEntity, TId>)Repository).Update(entity);
        return entity;
    }

    public TEntity[]? Update<TRequest>(IEnumerable<TRequest> request) where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        var ids = request.Select(x => x.Id).ToArray();
        var entities = Repository.GetFiltered(x => ids.Contains(x.Id), 0, ids.Length);
        if (entities.Length != ids.Length)
            return null;
        for (var i = 0; i < entities.Length; i++)
        {
            request.First(x => x.Id!.Equals(entities[i].Id)).UpdateEntity(entities[i]);
            ((IRURepository<TEntity, TId>)Repository).Update(entities[i]);
        }
        return entities;
    }

    public void Update(TEntity entity)
        => ((IRURepository<TEntity, TId>)Repository).Update(entity);

    public void Update(IEnumerable<TEntity> entities)
        => ((IRURepository<TEntity, TId>)Repository).Update(entities);

    public async Task<TResponse?> UpdateAsync<TResponse, TRequest>(TRequest request)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        var entity = await Repository.FindAsync(request.Id);
        if (entity is null)
            return null;
        request.UpdateEntity(entity);
        await ((IRURepository<TEntity, TId>)Repository).UpdateAsync(entity);
        var res = new TResponse();
        res.Set(entity);
        return res;
    }

    public async Task<TResponse[]?> UpdateAsync<TResponse, TRequest>(IEnumerable<TRequest> request)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        var ids = request.Select(x => x.Id).ToArray();
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id), 0, ids.Length);
        if (entities.Length != ids.Length)
            return null;
        var res = new TResponse[entities.Length];
        var toUpdate = new List<TEntity>();
        for (var i = 0; i < entities.Length; i++)
        {
            request.First(x => x.Id!.Equals(entities[i].Id)).UpdateEntity(entities[i]);
            toUpdate.Add(entities[i]);
            res[i] = new TResponse();
            res[i].Set(entities[i]);
        }
        await ((IRURepository<TEntity, TId>)Repository).UpdateAsync(toUpdate);
        return res;
    }

    public async Task<TEntity?> UpdateAsync<TRequest>(TRequest request) where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        var entity = await Repository.FindAsync(request.Id);
        if (entity is null)
            return null;
        request.UpdateEntity(entity);
        await ((IRURepository<TEntity, TId>)Repository).UpdateAsync(entity);
        return entity;
    }

    public async Task<TEntity[]?> UpdateAsync<TRequest>(IEnumerable<TRequest> request) where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        var ids = request.Select(x => x.Id).ToArray();
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id), 0, ids.Length);
        if (entities.Length != ids.Length)
            return null;
        var toUpdate = new List<TEntity>();
        for (var i = 0; i < entities.Length; i++)
        {
            request.First(x => x.Id!.Equals(entities[i].Id)).UpdateEntity(entities[i]);
            toUpdate.Add(entities[i]);
        }
        await ((IRURepository<TEntity, TId>)Repository).UpdateAsync(toUpdate);
        return entities;
    }

    public Task UpdateAsync(TEntity entity)
        => ((IRURepository<TEntity, TId>)Repository).UpdateAsync(entity);

    public Task UpdateAsync(IEnumerable<TEntity> entities)
        => ((IRURepository<TEntity, TId>)Repository).UpdateAsync(entities);
}

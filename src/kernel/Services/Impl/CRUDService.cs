using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;

namespace FastCrud.Kernel.Services;

public class CRUDService<TEntity, TId> : CRService<TEntity, TId>, ICRService<TEntity, TId>, IRUService<TEntity, TId>, IRDService<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    public CRUDService(ICRUDRepository<TEntity, TId> repository) : base(repository)
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

    public virtual void Delete(TId id)
    {
        var entity = Repository.FirstOrDefault(x => x.Id!.Equals(id) && x.DeletedAt == null);
        if (entity != null)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
            ((IDRepository<TEntity, TId>)Repository).Update(entity);
        }
    }

    public virtual void Delete(IEnumerable<TId> ids)
    {
        var entities = Repository.GetFiltered(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue);
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        ((IDRepository<TEntity, TId>)Repository).Update(entities);
    }

    public virtual void Delete(TEntity entity)
    {
        entity.DeletedAt = DateTimeOffset.UtcNow;
        ((IDRepository<TEntity, TId>)Repository).Update(entity);
    }

    public virtual void Delete(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        ((IDRepository<TEntity, TId>)Repository).Update(entities);
    }

    public virtual async Task DeleteAsync(TId id)
    {
        var entity = await Repository.FirstOrDefaultAsync(x => x.Id!.Equals(id) && x.DeletedAt == null);
        if (entity != null)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
            await ((IDRepository<TEntity, TId>)Repository).UpdateAsync(entity);
        }
    }

    public virtual async Task DeleteAsync(IEnumerable<TId> ids)
    {
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue);
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        await ((IDRepository<TEntity, TId>)Repository).UpdateAsync(entities);
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        entity.DeletedAt = DateTimeOffset.UtcNow;
        await ((IDRepository<TEntity, TId>)Repository).UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        await ((IDRepository<TEntity, TId>)Repository).UpdateAsync(entities);
    }

    public virtual void Remove(TId id)
    {
        var entity = Repository.FirstOrDefault(x => x.Id!.Equals(id) && x.DeletedAt == null);
        if (entity != null)
        {
            ((IDRepository<TEntity, TId>)Repository).Remove(entity);
        }
    }

    public virtual void Remove(IEnumerable<TId> ids)
    {
        var entities = Repository.GetFiltered(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue);
        ((IDRepository<TEntity, TId>)Repository).Remove(entities);
    }

    public virtual void Remove(TEntity entity)
        => ((IDRepository<TEntity, TId>)Repository).Remove(entity);

    public virtual void Remove(IEnumerable<TEntity> entities)
        => ((IDRepository<TEntity, TId>)Repository).Remove(entities);

    public virtual async Task RemoveAsync(TId id)
    {
        var entity = await Repository.FirstOrDefaultAsync(x => x.Id!.Equals(id) && x.DeletedAt == null);
        if (entity != null)
        {
            await ((IDRepository<TEntity, TId>)Repository).RemoveAsync(entity);
        }
    }

    public virtual async Task RemoveAsync(IEnumerable<TId> ids)
    {
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue);
        await ((IDRepository<TEntity, TId>)Repository).RemoveAsync(entities);
    }

    public virtual Task RemoveAsync(TEntity entity)
        => ((IDRepository<TEntity, TId>)Repository).RemoveAsync(entity);

    public virtual Task RemoveAsync(IEnumerable<TEntity> entities)
        => ((IDRepository<TEntity, TId>)Repository).RemoveAsync(entities);
}

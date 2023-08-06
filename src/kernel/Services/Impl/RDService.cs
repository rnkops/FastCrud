using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;

namespace FastCrud.Kernel.Services;

public class RDService<TEntity, TId> : RService<TEntity, TId>, IRService<TEntity, TId>, IDService<TEntity, TId>, IRDService<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    public RDService(IRDRepository<TEntity, TId> repository) : base(repository)
    {
    }

    public virtual void Delete(TId id)
    {
        var entity = Repository.FirstOrDefault(x => x.Id!.Equals(id) && x.DeletedAt == null);
        if (entity != null)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
            ((IRDRepository<TEntity, TId>)Repository).Update(entity);
        }
    }

    public virtual void Delete(IEnumerable<TId> ids)
    {
        var entities = Repository.GetFiltered(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue);
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        ((IRDRepository<TEntity, TId>)Repository).Update(entities);
    }

    public virtual void Delete(TEntity entity)
    {
        entity.DeletedAt = DateTimeOffset.UtcNow;
        ((IRDRepository<TEntity, TId>)Repository).Update(entity);
    }

    public virtual void Delete(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        ((IRDRepository<TEntity, TId>)Repository).Update(entities);
    }

    public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.FirstOrDefaultAsync(x => x.Id!.Equals(id) && x.DeletedAt == null, cancellationToken);
        if (entity != null)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
            await ((IRDRepository<TEntity, TId>)Repository).UpdateAsync(entity, cancellationToken);
        }
    }

    public virtual async Task DeleteAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default)
    {
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue, cancellationToken);
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        await ((IRDRepository<TEntity, TId>)Repository).UpdateAsync(entities, cancellationToken);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.DeletedAt = DateTimeOffset.UtcNow;
        await ((IRDRepository<TEntity, TId>)Repository).UpdateAsync(entity, cancellationToken);
    }

    public virtual async Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        await ((IRDRepository<TEntity, TId>)Repository).UpdateAsync(entities, cancellationToken);
    }

    public virtual void Remove(TId id)
    {
        var entity = Repository.FirstOrDefault(x => x.Id!.Equals(id) && x.DeletedAt == null);
        if (entity != null)
        {
            ((IRDRepository<TEntity, TId>)Repository).Remove(entity);
        }
    }

    public virtual void Remove(IEnumerable<TId> ids)
    {
        var entities = Repository.GetFiltered(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue);
        ((IRDRepository<TEntity, TId>)Repository).Remove(entities);
    }

    public virtual void Remove(TEntity entity)
    {
        ((IRDRepository<TEntity, TId>)Repository).Remove(entity);
    }

    public virtual void Remove(IEnumerable<TEntity> entities)
    {
        ((IRDRepository<TEntity, TId>)Repository).Remove(entities);
    }

    public virtual async Task RemoveAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.FirstOrDefaultAsync(x => x.Id!.Equals(id) && x.DeletedAt == null, cancellationToken);
        if (entity != null)
        {
            await ((IRDRepository<TEntity, TId>)Repository).RemoveAsync(entity, cancellationToken);
        }
    }

    public virtual async Task RemoveAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default)
    {
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue, cancellationToken);
        await ((IRDRepository<TEntity, TId>)Repository).RemoveAsync(entities, cancellationToken);
    }

    public virtual Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        => ((IRDRepository<TEntity, TId>)Repository).RemoveAsync(entity, cancellationToken);

    public virtual Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => ((IRDRepository<TEntity, TId>)Repository).RemoveAsync(entities, cancellationToken);

    public virtual void Remove<TRequest>(TRequest request) where TRequest : BaseDeleteRequest<TEntity, TId>
    {
        var entity = Repository.FirstOrDefault(x => x.Id!.Equals(request.Id) && x.DeletedAt == null);
        if (entity != null)
        {
            ((IRDRepository<TEntity, TId>)Repository).Remove(entity);
        }
    }

    public virtual void Delete<TRequest>(TRequest request) where TRequest : BaseDeleteRequest<TEntity, TId>
    {
        var entity = Repository.FirstOrDefault(x => x.Id!.Equals(request.Id) && x.DeletedAt == null);
        if (entity is null)
            return;
        request.DeleteEntity(entity);
        Delete(entity);
    }

    public virtual async Task RemoveAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : BaseDeleteRequest<TEntity, TId>
    {
        var entity = await Repository.FirstOrDefaultAsync(x => x.Id!.Equals(request.Id) && x.DeletedAt == null, cancellationToken);
        if (entity != null)
        {
            await ((IRDRepository<TEntity, TId>)Repository).RemoveAsync(entity, cancellationToken);
        }
    }

    public virtual async Task DeleteAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : BaseDeleteRequest<TEntity, TId>
    {
        var entity = await Repository.FirstOrDefaultAsync(x => x.Id!.Equals(request.Id) && x.DeletedAt == null, cancellationToken);
        if (entity is null)
            return;
        request.DeleteEntity(entity);
        await DeleteAsync(entity, cancellationToken);
    }

    public virtual int SaveChanges()
        => ((IRDRepository<TEntity, TId>)Repository).SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => ((IRDRepository<TEntity, TId>)Repository).SaveChangesAsync(cancellationToken);
}

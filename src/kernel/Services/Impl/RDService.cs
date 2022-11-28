using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;

namespace FastCrud.Kernel.Services;

public class RDService<TEntity, TId> : RService<TEntity, TId>, IRDService<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
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

    public virtual async Task DeleteAsync(TId id)
    {
        var entity = await Repository.FirstOrDefaultAsync(x => x.Id!.Equals(id) && x.DeletedAt == null);
        if (entity != null)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
            await ((IRDRepository<TEntity, TId>)Repository).UpdateAsync(entity);
        }
    }

    public virtual async Task DeleteAsync(IEnumerable<TId> ids)
    {
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue);
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        await ((IRDRepository<TEntity, TId>)Repository).UpdateAsync(entities);
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        entity.DeletedAt = DateTimeOffset.UtcNow;
        await ((IRDRepository<TEntity, TId>)Repository).UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        await ((IRDRepository<TEntity, TId>)Repository).UpdateAsync(entities);
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

    public virtual async Task RemoveAsync(TId id)
    {
        var entity = await Repository.FirstOrDefaultAsync(x => x.Id!.Equals(id) && x.DeletedAt == null);
        if (entity != null)
        {
            await ((IRDRepository<TEntity, TId>)Repository).RemoveAsync(entity);
        }
    }

    public virtual async Task RemoveAsync(IEnumerable<TId> ids)
    {
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id!) && x.DeletedAt == null, 0, int.MaxValue);
        await ((IRDRepository<TEntity, TId>)Repository).RemoveAsync(entities);
    }

    public virtual Task RemoveAsync(TEntity entity)
        => ((IRDRepository<TEntity, TId>)Repository).RemoveAsync(entity);

    public virtual Task RemoveAsync(IEnumerable<TEntity> entities)
        => ((IRDRepository<TEntity, TId>)Repository).RemoveAsync(entities);

    public virtual int SaveChanges()
        => ((IRDRepository<TEntity, TId>)Repository).SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => ((IRDRepository<TEntity, TId>)Repository).SaveChangesAsync(cancellationToken);
}

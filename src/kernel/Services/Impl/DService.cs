using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;

namespace FastCrud.Kernel.Services;

public class DService<TEntity, TId> : IDService<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    protected readonly IDRepository<TEntity, TId> Repository;

    public DService(IDRepository<TEntity, TId> repository)
    {
        Repository = repository;
    }

    public virtual void Delete(TEntity entity)
    {
        entity.DeletedAt = DateTimeOffset.UtcNow;
        Repository.Update(entity);
    }

    public virtual void Delete(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
        }
        Repository.Update(entities);
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.DeletedAt = DateTimeOffset.UtcNow;
        return Repository.UpdateAsync(entity, cancellationToken);
    }

    public virtual Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        foreach (var entity in entities)
        {
            entity.DeletedAt = now;
        }
        return Repository.UpdateAsync(entities, cancellationToken);
    }

    public virtual void Remove(TEntity entity)
        => Repository.Remove(entity);

    public virtual void Remove(IEnumerable<TEntity> entities)
        => Repository.Remove(entities);

    public virtual Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        => Repository.RemoveAsync(entity, cancellationToken);

    public virtual Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => Repository.RemoveAsync(entities, cancellationToken);

    public virtual int SaveChanges()
        => Repository.SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Repository.SaveChangesAsync(cancellationToken);
}

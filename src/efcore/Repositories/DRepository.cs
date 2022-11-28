using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;
using FastCrud.Persistence.EFCore.Data;

namespace FastCrud.Persistence.EFCore.Repositories;

public class DRepository<TEntity, TId> : IDRepository<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    protected readonly BaseDbContext Context;

    public DRepository(BaseDbContext context)
    {
        Context = context;
    }

    public virtual void Remove(TEntity entity)
        => Context.Set<TEntity>().Remove(entity);

    public virtual void Remove(IEnumerable<TEntity> entities)
        => Context.Set<TEntity>().RemoveRange(entities);

    public virtual Task RemoveAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task RemoveAsync(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    public virtual void Update(TEntity entity)
        => Context.Set<TEntity>().Update(entity);

    public virtual void Update(IEnumerable<TEntity> entities)
        => Context.Set<TEntity>().UpdateRange(entities);

    public virtual Task UpdateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().UpdateRange(entities);
        return Task.CompletedTask;
    }

    public virtual int SaveChanges()
        => Context.SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Context.SaveChangesAsync(cancellationToken);
}

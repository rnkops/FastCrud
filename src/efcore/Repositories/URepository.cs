using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;
using FastCrud.Persistence.EFCore.Data;

namespace FastCrud.Persistence.EFCore.Repositories;

public class URepository<TEntity, TId> : IURepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    protected readonly BaseDbContext Context;

    public URepository(BaseDbContext context)
    {
        Context = context;
    }

    public virtual void Update(TEntity entity)
        => Context.Set<TEntity>().Update(entity);

    public virtual void Update(IEnumerable<TEntity> entities)
        => Context.Set<TEntity>().UpdateRange(entities);

    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().UpdateRange(entities);
        return Task.CompletedTask;
    }

    public virtual int SaveChanges()
        => Context.SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Context.SaveChangesAsync(cancellationToken);
}

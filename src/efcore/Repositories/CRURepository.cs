using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;
using FastCrud.Persistence.EFCore.Data;

namespace FastCrud.Persistence.EFCore.Repositories;

public class CRURepository<TEntity, TId> : CRRepository<TEntity, TId>, ICRURepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    public CRURepository(BaseDbContext context) : base(context)
    {
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
}

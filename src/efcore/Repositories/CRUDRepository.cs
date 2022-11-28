using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;
using FastCrud.Persistence.EFCore.Data;

namespace FastCrud.Persistence.EFCore.Repositories;

public class CRUDRepository<TEntity, TId> : CRURepository<TEntity, TId>, ICRUDRepository<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    public CRUDRepository(BaseDbContext context) : base(context)
    {
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
}

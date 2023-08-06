using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;
using FastCrud.Persistence.EFCore.Data;

namespace FastCrud.Persistence.EFCore.Repositories;

public class CRepository<TEntity, TId> : ICRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    protected readonly BaseDbContext Context;

    public CRepository(BaseDbContext context)
    {
        Context = context;
    }

    public virtual void Add(TEntity entity)
        => Context.Set<TEntity>().Add(entity);

    public virtual void Add(IEnumerable<TEntity> entities)
        => Context.Set<TEntity>().AddRange(entities);

    public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>().AddAsync(entity, cancellationToken).AsTask();

    public virtual Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

    public virtual int SaveChanges()
        => Context.SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Context.SaveChangesAsync(cancellationToken);
}

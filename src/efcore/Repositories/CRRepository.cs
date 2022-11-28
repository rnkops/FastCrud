using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;
using FastCrud.Persistence.EFCore.Data;

namespace FastCrud.Persistence.EFCore.Repositories;

public class CRRepository<TEntity, TId> : RRepository<TEntity, TId>, ICRRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    public CRRepository(BaseDbContext context) : base(context)
    {
    }

    public virtual void Add(TEntity entity)
        => Context.Set<TEntity>().Add(entity);

    public virtual void Add(IEnumerable<TEntity> entities)
        => Context.Set<TEntity>().AddRange(entities);

    public virtual Task AddAsync(TEntity entity)
        => Context.Set<TEntity>().AddAsync(entity).AsTask();

    public virtual Task AddAsync(IEnumerable<TEntity> entities)
        => Context.Set<TEntity>().AddRangeAsync(entities);

    public virtual int SaveChanges()
        => Context.SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Context.SaveChangesAsync(cancellationToken);
}

using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;

namespace FastCrud.Kernel.Services;

public class UService<TEntity, TId> : IUService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    protected readonly IURepository<TEntity, TId> Repository;

    public UService(IURepository<TEntity, TId> repository)
    {
        Repository = repository;
    }

    public virtual int SaveChanges()
        => Repository.SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Repository.SaveChangesAsync(cancellationToken);

    public virtual void Update(TEntity entity)
        => Repository.Update(entity);

    public virtual void Update(IEnumerable<TEntity> entities)
        => Repository.Update(entities);

    public virtual Task UpdateAsync(TEntity entity)
        => Repository.UpdateAsync(entity);

    public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        => Repository.UpdateAsync(entities);
}

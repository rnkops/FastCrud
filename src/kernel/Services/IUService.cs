using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public interface IUService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task UpdateAsync(IEnumerable<TEntity> entities);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

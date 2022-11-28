using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public interface IRDService<TEntity, TId> : IRService<TEntity, TId>, IDService<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    void Delete(TId id);
    void Delete(IEnumerable<TId> ids);
    void Remove(TId id);
    void Remove(IEnumerable<TId> ids);
    Task DeleteAsync(TId id);
    Task DeleteAsync(IEnumerable<TId> ids);
    Task RemoveAsync(TId id);
    Task RemoveAsync(IEnumerable<TId> ids);
}

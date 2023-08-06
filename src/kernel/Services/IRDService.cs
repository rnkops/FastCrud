using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public interface IRDService<TEntity, TId> : IRService<TEntity, TId>, IDService<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
    void Delete(TId id);
    void Delete(IEnumerable<TId> ids);
    void Remove(TId id);
    void Remove(IEnumerable<TId> ids);
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
    Task DeleteAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
    Task RemoveAsync(TId id, CancellationToken cancellationToken = default);
    Task RemoveAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
    void Remove<TRequest>(TRequest request) where TRequest : BaseDeleteRequest<TEntity, TId>;
    void Delete<TRequest>(TRequest request) where TRequest : BaseDeleteRequest<TEntity, TId>;
    Task RemoveAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : BaseDeleteRequest<TEntity, TId>;
    Task DeleteAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : BaseDeleteRequest<TEntity, TId>;
}

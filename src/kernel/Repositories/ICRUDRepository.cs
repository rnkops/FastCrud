using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Repositories;

public interface ICRUDRepository<TEntity, TId> : IRRepository<TEntity, TId>, ICRepository<TEntity, TId>, ICRRepository<TEntity, TId>, IRURepository<TEntity, TId>, IURepository<TEntity, TId>, IDRepository<TEntity, TId>, IRDRepository<TEntity, TId> where TEntity : class, IDeletableEntity<TId>
{
}

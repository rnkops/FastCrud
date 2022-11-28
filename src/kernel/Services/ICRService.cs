using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public interface ICRService<TEntity, TId> : ICService<TEntity, TId>, IRService<TEntity, TId> where TEntity : class, IEntity<TId>
{
}

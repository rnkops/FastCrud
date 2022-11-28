using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Dtos;

public abstract class BaseCreateRequest<TEntity, TId> where TEntity : IEntity<TId>
{
    public abstract TEntity ToEntity();
}

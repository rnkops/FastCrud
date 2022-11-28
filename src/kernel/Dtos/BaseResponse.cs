using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Dtos;

public abstract class BaseResponse<TEntity, TId> where TEntity : IEntity<TId>
{
    public abstract void Set(TEntity entity);
}

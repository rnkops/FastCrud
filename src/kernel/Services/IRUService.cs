using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public interface IRUService<TEntity, TId> : IRService<TEntity, TId>, IUService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    TResponse? Update<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse<TEntity, TId>, new() where TRequest : BaseUpdateRequest<TEntity, TId>;
    TResponse[]? Update<TResponse, TRequest>(IEnumerable<TRequest> request) where TResponse : BaseResponse<TEntity, TId>, new() where TRequest : BaseUpdateRequest<TEntity, TId>;
    Task<TResponse?> UpdateAsync<TResponse, TRequest>(TRequest request, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>, new() where TRequest : BaseUpdateRequest<TEntity, TId>;
    Task<TResponse[]?> UpdateAsync<TResponse, TRequest>(IEnumerable<TRequest> request, CancellationToken cancellationToken = default) where TResponse : BaseResponse<TEntity, TId>, new() where TRequest : BaseUpdateRequest<TEntity, TId>;
    TEntity? Update<TRequest>(TRequest request) where TRequest : BaseUpdateRequest<TEntity, TId>;
    TEntity[]? Update<TRequest>(IEnumerable<TRequest> request) where TRequest : BaseUpdateRequest<TEntity, TId>;
    Task<TEntity?> UpdateAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : BaseUpdateRequest<TEntity, TId>;
    Task<TEntity[]?> UpdateAsync<TRequest>(IEnumerable<TRequest> request, CancellationToken cancellationToken = default) where TRequest : BaseUpdateRequest<TEntity, TId>;
}

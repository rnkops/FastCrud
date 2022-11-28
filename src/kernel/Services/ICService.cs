using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Services;

public interface ICService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    void Add(TEntity entity);
    void Add(IEnumerable<TEntity> entities);
    Task AddAsync(TEntity entity);
    Task AddAsync(IEnumerable<TEntity> entities);
    TResponse Add<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse<TEntity, TId>, new() where TRequest : BaseCreateRequest<TEntity, TId>;
    TResponse[] Add<TResponse, TRequest>(IEnumerable<TRequest> request) where TResponse : BaseResponse<TEntity, TId>, new() where TRequest : BaseCreateRequest<TEntity, TId>;
    Task<TResponse> AddAsync<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse<TEntity, TId>, new() where TRequest : BaseCreateRequest<TEntity, TId>;
    Task<TResponse[]> AddAsync<TResponse, TRequest>(IEnumerable<TRequest> request) where TResponse : BaseResponse<TEntity, TId>, new() where TRequest : BaseCreateRequest<TEntity, TId>;
    TEntity Add<TRequest>(TRequest request) where TRequest : BaseCreateRequest<TEntity, TId>;
    TEntity[] Add<TRequest>(IEnumerable<TRequest> request) where TRequest : BaseCreateRequest<TEntity, TId>;
    Task<TEntity> AddAsync<TRequest>(TRequest request) where TRequest : BaseCreateRequest<TEntity, TId>;
    Task<TEntity[]> AddAsync<TRequest>(IEnumerable<TRequest> request) where TRequest : BaseCreateRequest<TEntity, TId>;
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

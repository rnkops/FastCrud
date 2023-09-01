using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;

namespace FastCrud.Kernel.Services;

public class RUService<TEntity, TId> : RService<TEntity, TId>, IRService<TEntity, TId>, IUService<TEntity, TId>, IRUService<TEntity, TId> where TEntity : class, IEntity<TId>
{
    public RUService(IRURepository<TEntity, TId> repository) : base(repository)
    {
    }

    public virtual int SaveChanges()
        => ((IRURepository<TEntity, TId>)Repository).SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => ((IRURepository<TEntity, TId>)Repository).SaveChangesAsync(cancellationToken);

    public virtual TResponse? Update<TResponse, TRequest>(TRequest request)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        if (request is IValidatable validatable)
        {
            if (!validatable.IsValid() || !IsValid(validatable))
            {
                throw new InvalidDataException($"Invalid data: {validatable.GetType().Name}");
            }
        }
        var entity = Repository.Find(request.Id);
        if (entity is null)
            return null;
        request.UpdateEntity(entity);
        ((IRURepository<TEntity, TId>)Repository).Update(entity);
        var res = new TResponse();
        res.Set(entity);
        return res;
    }

    public virtual TResponse[]? Update<TResponse, TRequest>(IEnumerable<TRequest> request)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        if (typeof(IValidatable).IsAssignableFrom(typeof(TRequest)))
        {
            foreach (var validatable in request.Cast<IValidatable>())
            {
                if (!validatable.IsValid() || !IsValid(validatable))
                {
                    throw new InvalidDataException($"Invalid data: {validatable.GetType().Name}");
                }
            }
        }
        var ids = request.Select(x => x.Id).ToArray();
        var entities = Repository.GetFiltered(x => ids.Contains(x.Id), 0, ids.Length);
        if (entities.Length != ids.Length)
            return null;
        var res = new TResponse[entities.Length];
        var toUpdate = new List<TEntity>();
        for (var i = 0; i < entities.Length; i++)
        {
            request.First(x => x.Id!.Equals(entities[i].Id)).UpdateEntity(entities[i]);
            toUpdate.Add(entities[i]);
            res[i] = new TResponse();
            res[i].Set(entities[i]);
        }
        ((IRURepository<TEntity, TId>)Repository).Update(toUpdate);
        return res;
    }

    public virtual TEntity? Update<TRequest>(TRequest request) where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        if (request is IValidatable validatable)
        {
            if (!validatable.IsValid() || !IsValid(validatable))
            {
                throw new InvalidDataException($"Invalid data: {validatable.GetType().Name}");
            }
        }
        var entity = Repository.Find(request.Id);
        if (entity is null)
            return null;
        request.UpdateEntity(entity);
        ((IRURepository<TEntity, TId>)Repository).Update(entity);
        return entity;
    }

    public virtual TEntity[]? Update<TRequest>(IEnumerable<TRequest> request) where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        if (typeof(IValidatable).IsAssignableFrom(typeof(TRequest)))
        {
            foreach (var validatable in request.Cast<IValidatable>())
            {
                if (!validatable.IsValid() || !IsValid(validatable))
                {
                    throw new InvalidDataException($"Invalid data: {validatable.GetType().Name}");
                }
            }
        }
        var ids = request.Select(x => x.Id).ToArray();
        var entities = Repository.GetFiltered(x => ids.Contains(x.Id), 0, ids.Length);
        if (entities.Length != ids.Length)
            return null;
        for (var i = 0; i < entities.Length; i++)
        {
            request.First(x => x.Id!.Equals(entities[i].Id)).UpdateEntity(entities[i]);
            ((IRURepository<TEntity, TId>)Repository).Update(entities[i]);
        }
        return entities;
    }

    public virtual void Update(TEntity entity)
        => ((IRURepository<TEntity, TId>)Repository).Update(entity);

    public virtual void Update(IEnumerable<TEntity> entities)
        => ((IRURepository<TEntity, TId>)Repository).Update(entities);

    public virtual async Task<TResponse?> UpdateAsync<TResponse, TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        if (request is IValidatable validatable)
        {
            if (!validatable.IsValid() || !await IsValidAsync(validatable, cancellationToken))
            {
                throw new InvalidDataException($"Invalid data: {validatable.GetType().Name}");
            }
        }
        var entity = await Repository.FindAsync(request.Id, cancellationToken);
        if (entity is null)
            return null;
        request.UpdateEntity(entity);
        await ((IRURepository<TEntity, TId>)Repository).UpdateAsync(entity, cancellationToken);
        var res = new TResponse();
        res.Set(entity);
        return res;
    }

    public virtual async Task<TResponse[]?> UpdateAsync<TResponse, TRequest>(IEnumerable<TRequest> request, CancellationToken cancellationToken = default)
        where TResponse : BaseResponse<TEntity, TId>, new()
        where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        if (typeof(IValidatable).IsAssignableFrom(typeof(TRequest)))
        {
            foreach (var validatable in request.Cast<IValidatable>())
            {
                if (!validatable.IsValid() || !await IsValidAsync(validatable, cancellationToken))
                {
                    throw new InvalidDataException($"Invalid data: {validatable.GetType().Name}");
                }
            }
        }
        var ids = request.Select(x => x.Id).ToArray();
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id), 0, ids.Length, cancellationToken);
        if (entities.Length != ids.Length)
            return null;
        var res = new TResponse[entities.Length];
        var toUpdate = new List<TEntity>();
        for (var i = 0; i < entities.Length; i++)
        {
            request.First(x => x.Id!.Equals(entities[i].Id)).UpdateEntity(entities[i]);
            toUpdate.Add(entities[i]);
            res[i] = new TResponse();
            res[i].Set(entities[i]);
        }
        await ((IRURepository<TEntity, TId>)Repository).UpdateAsync(toUpdate, cancellationToken);
        return res;
    }

    public virtual async Task<TEntity?> UpdateAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        if (request is IValidatable validatable)
        {
            if (!validatable.IsValid() || !await IsValidAsync(validatable, cancellationToken))
            {
                throw new InvalidDataException($"Invalid data: {validatable.GetType().Name}");
            }
        }
        var entity = await Repository.FindAsync(request.Id, cancellationToken);
        if (entity is null)
            return null;
        request.UpdateEntity(entity);
        await ((IRURepository<TEntity, TId>)Repository).UpdateAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity[]?> UpdateAsync<TRequest>(IEnumerable<TRequest> request, CancellationToken cancellationToken = default) where TRequest : BaseUpdateRequest<TEntity, TId>
    {
        if (typeof(IValidatable).IsAssignableFrom(typeof(TRequest)))
        {
            foreach (var validatable in request.Cast<IValidatable>())
            {
                if (!validatable.IsValid() || !await IsValidAsync(validatable, cancellationToken))
                {
                    throw new InvalidDataException($"Invalid data: {validatable.GetType().Name}");
                }
            }
        }
        var ids = request.Select(x => x.Id).ToArray();
        var entities = await Repository.GetFilteredAsync(x => ids.Contains(x.Id), 0, ids.Length, cancellationToken);
        if (entities.Length != ids.Length)
            return null;
        var toUpdate = new List<TEntity>();
        for (var i = 0; i < entities.Length; i++)
        {
            request.First(x => x.Id!.Equals(entities[i].Id)).UpdateEntity(entities[i]);
            toUpdate.Add(entities[i]);
        }
        await ((IRURepository<TEntity, TId>)Repository).UpdateAsync(toUpdate, cancellationToken);
        return entities;
    }

    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        => ((IRURepository<TEntity, TId>)Repository).UpdateAsync(entity, cancellationToken);

    public virtual Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => ((IRURepository<TEntity, TId>)Repository).UpdateAsync(entities, cancellationToken);

    protected virtual Task<bool> IsValidAsync<TValidatable>(TValidatable validatable, CancellationToken cancellationToken = default) where TValidatable : IValidatable
    {
        return Task.FromResult(true);
    }

    protected virtual bool IsValid<TValidatable>(TValidatable validatable, CancellationToken cancellationToken = default) where TValidatable : IValidatable
    {
        return true;
    }
}

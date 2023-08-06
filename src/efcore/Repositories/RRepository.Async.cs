using System.Linq.Expressions;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FastCrud.Persistence.EFCore.Repositories;

public partial class RRepository<TEntity, TId> : IRRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>().CountAsync(predicate, cancellationToken);

    public virtual Task<int> CountAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>
    {
        var queryable = Context.Set<TEntity>().AsQueryable();
        queryable = query.GetFiltered(queryable);
        return queryable.CountAsync(cancellationToken);
    }

    public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>().AnyAsync(predicate, cancellationToken);

    public virtual Task<bool> ExistsAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>
    {
        var queryable = Context.Set<TEntity>().AsQueryable();
        queryable = query.GetFiltered(queryable);
        return queryable.AnyAsync(cancellationToken);
    }

    public virtual Task<TEntity?> FindAsync(TId id, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>().FindAsync(id, cancellationToken).AsTask();

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>().OrderBy(order).FirstOrDefaultAsync(predicate, cancellationToken);

    public virtual Task<TEntity[]> GetAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>
    {
        var queryable = Context.Set<TEntity>().AsQueryable();
        queryable = query.GetFiltered(queryable);
        queryable = query.GetOrdered(queryable);
        queryable = query.GetSkipped(queryable);
        return queryable.ToArrayAsync(cancellationToken);
    }

    public virtual Task<TFields[]> GetAsync<TFields, TQuery>(Expression<Func<TEntity, TFields>> selector, TQuery query, CancellationToken cancellationToken = default) where TQuery : BaseGetQuery<TEntity, TId>
    {
        var queryable = Context.Set<TEntity>().AsQueryable();
        queryable = query.GetFiltered(queryable);
        queryable = query.GetOrdered(queryable);
        queryable = query.GetSkipped(queryable);
        return queryable.Select(selector).ToArrayAsync(cancellationToken);
    }

    public virtual Task<TEntity[]> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>()
        .Where(predicate)
        .Skip(offset)
        .Take(limit)
        .ToArrayAsync(cancellationToken);

    public virtual Task<TFields[]> GetFilteredAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>()
        .Where(predicate)
        .Select(selector)
        .Skip(offset)
        .Take(limit)
        .ToArrayAsync(cancellationToken);

    public virtual Task<TEntity[]> GetFilteredAndOrderedAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>()
        .Where(predicate)
        .Skip(offset)
        .Take(limit)
        .ToArrayAsync(cancellationToken);

    public virtual Task<TFields[]> GetFilteredAndOrderedAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>()
        .Where(predicate)
        .Select(selector)
        .Skip(offset)
        .Take(limit)
        .ToArrayAsync(cancellationToken);

    public virtual Task<TEntity[]> GetFilteredAndOrderedDescendinglyAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>()
        .Where(predicate)
        .OrderBy(order)
        .Skip(offset)
        .Take(limit)
        .ToArrayAsync(cancellationToken);

    public virtual Task<TFields[]> GetFilteredAndOrderedDescendinglyAsync<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
        => Context.Set<TEntity>()
        .Where(predicate)
        .OrderBy(order)
        .Select(selector)
        .Skip(offset)
        .Take(limit)
        .ToArrayAsync(cancellationToken);
}

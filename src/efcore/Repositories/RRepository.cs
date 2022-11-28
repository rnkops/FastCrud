using System.Linq.Expressions;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Repositories;
using FastCrud.Persistence.EFCore.Data;

namespace FastCrud.Persistence.EFCore.Repositories;

public partial class RRepository<TEntity, TId> : IRRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    protected readonly BaseDbContext Context;

    public RRepository(BaseDbContext context)
    {
        Context = context;
    }

    public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        => Context.Set<TEntity>().Count(predicate);

    public virtual int Count<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
    {
        var queryable = Context.Set<TEntity>().AsQueryable();
        queryable = query.GetFiltered(queryable);
        return queryable.Count();
    }

    public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        => Context.Set<TEntity>().Any(predicate);

    public virtual bool Exists<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
    {
        var queryable = Context.Set<TEntity>().AsQueryable();
        queryable = query.GetFiltered(queryable);
        return queryable.Any();
    }

    public virtual TEntity? Find(TId id)
        => Context.Set<TEntity>().Find(id);

    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        => Context.Set<TEntity>().FirstOrDefault(predicate);

    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order)
        => Context.Set<TEntity>().OrderBy(order).FirstOrDefault(predicate);

    public virtual TEntity[] Get<TQuery>(TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
    {
        var queryable = Context.Set<TEntity>().AsQueryable();
        queryable = query.GetFiltered(queryable);
        queryable = query.GetOrdered(queryable);
        queryable = query.GetSkipped(queryable);
        return queryable.ToArray();
    }

    public virtual TFields[] Get<TFields, TQuery>(Expression<Func<TEntity, TFields>> selector, TQuery query) where TQuery : BaseGetQuery<TEntity, TId>
    {
        var queryable = Context.Set<TEntity>().AsQueryable();
        queryable = query.GetFiltered(queryable);
        queryable = query.GetOrdered(queryable);
        queryable = query.GetSkipped(queryable);
        return queryable.Select(selector).ToArray();
    }

    public virtual TEntity[] GetFiltered(Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25)
        => Context.Set<TEntity>()
        .Where(predicate)
        .Skip(offset)
        .Take(limit)
        .ToArray();

    public virtual TFields[] GetFiltered<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, int offset = 0, int limit = 25)
        => Context.Set<TEntity>()
        .Where(predicate)
        .Select(selector)
        .Skip(offset)
        .Take(limit)
        .ToArray();

    public virtual TEntity[] GetFilteredAndOrdered(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Context.Set<TEntity>()
        .Where(predicate)
        .OrderBy(order)
        .Skip(offset)
        .Take(limit)
        .ToArray();

    public virtual TFields[] GetFilteredAndOrdered<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Context.Set<TEntity>()
        .Where(predicate)
        .OrderBy(order)
        .Select(selector)
        .Skip(offset)
        .Take(limit)
        .ToArray();

    public virtual TEntity[] GetFilteredAndOrderedDescendingly(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Context.Set<TEntity>()
        .Where(predicate)
        .OrderByDescending(order)
        .Skip(offset)
        .Take(limit)
        .ToArray();

    public virtual TFields[] GetFilteredAndOrderedDescendingly<TFields>(Expression<Func<TEntity, TFields>> selector, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int offset = 0, int limit = 25)
        => Context.Set<TEntity>()
        .Where(predicate)
        .OrderByDescending(order)
        .Select(selector)
        .Skip(offset)
        .Take(limit)
        .ToArray();
}

using System.Linq.Expressions;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Extensions;

namespace FastCrud.Kernel.Dtos;

public abstract class BaseGetQuery<TEntity, TId> where TEntity : class, IEntity<TId>
{
    public DateTimeOffset? CreatedAtGTE { get; set; }
    public DateTimeOffset? CreatedAtLTE { get; set; }
    public long? SerialGTE { get; set; }
    public long? SerialLTE { get; set; }
    public int Limit { get; set; }
    public string? Order { get; set; }

    public virtual IQueryable<TEntity> GetFiltered(IQueryable<TEntity> queryable)
    {
        if (typeof(TEntity) is IHasCreatedAt hasCreatedAt)
        {
            queryable = queryable
                .ConditionalWhere(CreatedAtGTE.HasValue, x => hasCreatedAt.CreatedAt >= CreatedAtGTE!.Value)
                .ConditionalWhere(CreatedAtLTE.HasValue, x => hasCreatedAt.CreatedAt <= CreatedAtLTE!.Value);
        }
        if (typeof(TEntity) is IHasSerial hasSerial)
        {
            queryable = queryable
                .ConditionalWhere(SerialGTE.HasValue, x => hasSerial.Serial >= SerialGTE!.Value)
                .ConditionalWhere(SerialLTE.HasValue, x => hasSerial.Serial <= SerialLTE!.Value);
        }
        return queryable;
    }

    public virtual IOrderedQueryable<TEntity> GetOrdered(IQueryable<TEntity> queryable)
    {
        var props = typeof(TEntity).GetProperties().ToDictionary(x => x.Name.ToLower());
        IOrderedQueryable<TEntity>? orderedQueryable = InitialSort(queryable);
        if (string.IsNullOrWhiteSpace(Order))
        {
            return orderedQueryable ?? queryable.OrderBy(x => x.Id);
        }
        foreach (var order in Order.Split(','))
        {
            var desc = order.StartsWith("-");
            var propName = order.Replace("-", "").ToLower();
            var prop = props!.GetValueOrDefault(propName, null);
            if (prop is null)
                continue;
            var parameter = Expression.Parameter(queryable.ElementType, "");
            var property = Expression.Property(parameter, prop.Name);
            var lambda = Expression.Lambda(property, parameter);
            if (orderedQueryable is null)
            {
                var methodCallExpression = Expression.Call(typeof(Queryable), desc ? "OrderByDescending" : "OrderBy",
                                      new Type[] { queryable.ElementType, property.Type },
                                      queryable.Expression, Expression.Quote(lambda));
                orderedQueryable = queryable.Provider.CreateQuery<TEntity>(methodCallExpression) as IOrderedQueryable<TEntity>;
            }
            else
            {
                var methodCallExpression = Expression.Call(typeof(Queryable), desc ? "ThenByDescending" : "ThenBy",
                                      new Type[] { queryable.ElementType, property.Type },
                                      orderedQueryable.Expression, Expression.Quote(lambda));
                orderedQueryable = orderedQueryable.Provider.CreateQuery<TEntity>(methodCallExpression) as IOrderedQueryable<TEntity>;
            }
        }
        return orderedQueryable ?? queryable.OrderBy(x => x.Id);
    }

    public virtual IQueryable<TEntity> GetSkipped(IQueryable<TEntity> queryable)
        => queryable.Take(Limit > 0 && Limit < 101 ? Limit : 25);

    protected virtual IOrderedQueryable<TEntity>? InitialSort(IQueryable<TEntity> queryable)
        => null;
}

using System.Linq.Expressions;
using FastCrud.Kernel.Entities;

namespace FastCrud.Kernel.Extensions;

public static class LinqExtensions
{
    public static IEnumerable<T> ConditionalWhere<T>(this IEnumerable<T> source, Func<bool> condition, Func<T, bool> predicate)
        => condition() ? source.Where(predicate) : source;

    public static IEnumerable<T> ConditionalWhere<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        => source.ConditionalWhere(() => condition, predicate);

    public static IEnumerable<T> ConditionalWhere<T>(this IEnumerable<T> source, Func<bool> condition, Func<T, bool> truePredicate, Func<T, bool> falsePredicate)
        => condition() ? source.Where(truePredicate) : source.Where(falsePredicate);

    public static IEnumerable<T> ConditionalWhere<T>(this IEnumerable<T> source, bool condition, Func<T, bool> truePredicate, Func<T, bool> falsePredicate)
        => source.ConditionalWhere(() => condition, truePredicate, falsePredicate);

    public static IQueryable<T> ConditionalWhere<T>(this IQueryable<T> source, Func<bool> condition, Expression<Func<T, bool>> predicate)
        => condition() ? source.Where(predicate) : source;

    public static IQueryable<T> ConditionalWhere<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
        => source.ConditionalWhere(() => condition, predicate);

    public static IQueryable<T> ConditionalWhere<T>(this IQueryable<T> source, Func<bool> condition, Expression<Func<T, bool>> truePredicate, Expression<Func<T, bool>> falsePredicate)
        => condition() ? source.Where(truePredicate) : source.Where(falsePredicate);

    public static IQueryable<T> ConditionalWhere<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> truePredicate, Expression<Func<T, bool>> falsePredicate)
        => source.ConditionalWhere(() => condition, truePredicate, falsePredicate);

    public static IQueryable<T> WhenStringIsNotNullOrEmpty<T>(this IQueryable<T> source, string? value, Func<string, Expression<Func<T, bool>>> predicate)
        => !string.IsNullOrEmpty(value) ? source.Where(predicate(value.Trim().ToLower())) : source;

    public static IQueryable<T> WhenEnumerableIsNotNullOrEmpty<T>(this IQueryable<T> source, IEnumerable<T>? value, Func<IEnumerable<T>, Expression<Func<T, bool>>> predicate)
        => value != null && value.Any() ? source.Where(predicate(value)) : source;

    public static IQueryable<T> WhereAny<T>(this IQueryable<T> source, params Expression<Func<T, bool>>[] predicates)
        => source.Where(predicates.Aggregate((a, b) => a.Or(b)));

    public static IQueryable<T> WhereAll<T>(this IQueryable<T> source, params Expression<Func<T, bool>>[] predicates)
        => source.Where(predicates.Aggregate((a, b) => a.And(b)));

    public static IQueryable<TEntity> WhereNotDeleted<TEntity, TEntityId>(this IQueryable<TEntity> queryable) where TEntity : IDeletableEntity<TEntityId>
        => queryable.Where(x => x.DeletedAt == null);

    // https://stackoverflow.com/questions/22569043/merge-two-linq-expressions/22569086#22569086
#nullable disable
    public static Expression<Func<T, bool>> True<T>() { return _ => true; }
    public static Expression<Func<T, bool>> False<T>() { return _ => false; }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var secondBody = expr2.Body.Replace(expr2.Parameters[0], expr1.Parameters[0]);
        return Expression.Lambda<Func<T, bool>>
              (Expression.OrElse(expr1.Body, secondBody), expr1.Parameters);
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var secondBody = expr2.Body.Replace(expr2.Parameters[0], expr1.Parameters[0]);
        return Expression.Lambda<Func<T, bool>>
              (Expression.AndAlso(expr1.Body, secondBody), expr1.Parameters);
    }

    public static Expression Replace(this Expression expression, Expression searchEx, Expression replaceEx)
    {
        return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
    }

    internal class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public ReplaceVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
#nullable restore
}

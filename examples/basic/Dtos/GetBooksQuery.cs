using FastCrud.Examples.Basic.Entities;
using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Extensions;

namespace FastCrud.Examples.Basic.Dtos;

public class GetBooksQuery : BaseGetQuery<Book, int>
{
    public override IQueryable<Book> GetFiltered(IQueryable<Book> queryable)
    {
        return base.GetFiltered(queryable)
            .Where(x => x.DeletedAt == null)
            .ConditionalWhere(SerialGTE.HasValue, x => x.Id >= SerialGTE!.Value)
            .ConditionalWhere(SerialLTE.HasValue, x => x.Id <= SerialLTE!.Value);
    }

    protected override IOrderedQueryable<Book>? InitialSort(IQueryable<Book> queryable)
    {
        return queryable.OrderBy(x => x.Id);
    }
}

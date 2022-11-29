using FastCrud.Examples.Basic.Data;
using FastCrud.Examples.Basic.Entities;
using FastCrud.Persistence.EFCore.Repositories;

namespace FastCrud.Examples.Basic.Repositories;

public class BookRepository : CRUDRepository<Book, int>
{
    public BookRepository(PostgresDbContext context) : base(context)
    {
    }
}

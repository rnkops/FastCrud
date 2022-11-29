using FastCrud.Examples.Basic.Entities;
using FastCrud.Examples.Basic.Repositories;
using FastCrud.Kernel.Services;

namespace FastCrud.Examples.Basic.Services;

public class BookService : CRUDService<Book, int>
{
    public BookService(BookRepository repository) : base(repository)
    {
    }
}

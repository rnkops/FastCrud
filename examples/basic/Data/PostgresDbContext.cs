using FastCrud.Examples.Basic.Entities;
using FastCrud.Persistence.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace FastCrud.Examples.Basic.Data;

public class PostgresDbContext : BaseSnakeCaseDbContext
{
    public DbSet<Book> Books => Set<Book>();

    public PostgresDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

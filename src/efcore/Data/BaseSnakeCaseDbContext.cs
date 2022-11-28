using Microsoft.EntityFrameworkCore;

namespace FastCrud.Persistence.EFCore.Data;

public abstract class BaseSnakeCaseDbContext : BaseDbContext
{
    protected BaseSnakeCaseDbContext()
    {
    }

    protected BaseSnakeCaseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        _ = optionsBuilder.UseSnakeCaseNamingConvention();
    }
}

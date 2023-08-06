using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FastCrud.Persistence.EFCore.Data;

public abstract class BaseDbContext : DbContext
{
    protected BaseDbContext()
    {
    }

    protected BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        _ = optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var prop in entityType.GetProperties())
            {
                if (prop.ClrType == typeof(string) && prop.GetMaxLength() == null)
                {
                    prop.SetMaxLength(GetDefaultStringLength());
                }
                else if ((prop.ClrType == typeof(int) || prop.ClrType == typeof(long)) && prop.Name == "Serial")
                {
                    prop.ValueGenerated = ValueGenerated.OnAdd;
                    prop.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                    prop.DeclaringEntityType.AddIndex(prop);
                }
            }
        }
    }

    protected virtual int GetDefaultStringLength()
        => 256;
}

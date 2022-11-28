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

    public virtual void ApplySchemaChanges()
    {
        Database.Migrate();
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
        var stringProps = new List<IMutableProperty>();
        var serialProps = new List<IMutableProperty>();
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var prop in entityType.GetProperties())
            {
                if (prop.ClrType == typeof(string) && prop.GetMaxLength() == null)
                {
                    stringProps.Add(prop);
                }
                else if (prop.ClrType == typeof(int) && prop.Name == "Serial")
                {
                    serialProps.Add(prop);
                }
                else if (prop.ClrType == typeof(long) && prop.Name == "Serial")
                {
                    serialProps.Add(prop);
                }
            }
        }
        foreach (var property in stringProps)
        {
            property.SetMaxLength(256);
        }
        foreach (var property in serialProps)
        {
            property.ValueGenerated = ValueGenerated.OnAdd;
            property.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            property.DeclaringEntityType.AddIndex(property);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace Ordering.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options)
{

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entities = modelBuilder.Model.GetEntityTypes();
        ManageDecimalPrecision(entities);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    private static void ManageDecimalPrecision(IEnumerable<IMutableEntityType> entities)
    {
        entities
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal)
                     || p.ClrType == typeof(decimal?))
            .ToList()
            .ForEach(p =>
            {
                if (p.GetPrecision() is null)
                    p.SetPrecision(18);
                if (p.GetScale() is null)
                    p.SetScale(4);
            });
    }
}

using EcoSync.BuildingBlocks.Infrastructure;
using EcoSync.Modules.Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace EcoSync.Modules.Catalog.Infrastructure.Database;

public sealed class CatalogDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}

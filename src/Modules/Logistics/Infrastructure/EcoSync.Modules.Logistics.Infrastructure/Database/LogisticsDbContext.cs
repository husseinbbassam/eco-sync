using EcoSync.Modules.Logistics.Domain.Shipments;
using Microsoft.EntityFrameworkCore;

namespace EcoSync.Modules.Logistics.Infrastructure.Database;

public sealed class LogisticsDbContext : DbContext
{
    public DbSet<Shipment> Shipments { get; set; }

    public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("logistics");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LogisticsDbContext).Assembly);
    }
}

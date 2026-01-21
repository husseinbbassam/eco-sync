using EcoSync.BuildingBlocks.Application;
using EcoSync.Modules.Logistics.Domain.Shipments;
using EcoSync.Modules.Logistics.Infrastructure.Database;
using EcoSync.Modules.Logistics.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoSync.Modules.Logistics.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddLogisticsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<LogisticsDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

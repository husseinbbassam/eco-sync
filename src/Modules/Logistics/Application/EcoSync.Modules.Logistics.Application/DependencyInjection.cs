using Microsoft.Extensions.DependencyInjection;

namespace EcoSync.Modules.Logistics.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddLogisticsApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}

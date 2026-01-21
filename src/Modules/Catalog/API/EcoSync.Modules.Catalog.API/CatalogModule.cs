using EcoSync.Modules.Catalog.API.Endpoints;
using EcoSync.Modules.Catalog.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoSync.Modules.Catalog.API;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add Infrastructure
        services.AddCatalogInfrastructure(configuration);

        // Add MediatR handlers from Application layer
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
        });

        // Add FluentValidation
        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);

        return services;
    }

    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapProductEndpoints();
        return app;
    }
}

using EcoSync.Modules.Sustainability.API.Endpoints;
using EcoSync.Modules.Sustainability.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoSync.Modules.Sustainability.API;

public static class SustainabilityModule
{
    public static IServiceCollection AddSustainabilityModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add Infrastructure
        services.AddSustainabilityInfrastructure(configuration);

        // Add MediatR handlers from Application layer
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
        });

        // Add FluentValidation
        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);

        return services;
    }

    public static IEndpointRouteBuilder MapSustainabilityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapSustainabilityEndpoints();
        return app;
    }
}

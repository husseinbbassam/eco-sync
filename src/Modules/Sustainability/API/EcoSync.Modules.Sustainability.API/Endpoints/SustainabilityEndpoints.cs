using EcoSync.Modules.Sustainability.Application.Sustainability.Queries.AnalyzeProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EcoSync.Modules.Sustainability.API.Endpoints;

public static class SustainabilityEndpoints
{
    public static IEndpointRouteBuilder MapSustainabilityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sustainability").WithTags("Sustainability");

        group.MapGet("/analyze/{productId:guid}", async (Guid productId, ISender sender) =>
        {
            var query = new AnalyzeProductQuery(productId);
            var analysis = await sender.Send(query);
            return analysis is not null ? Results.Ok(analysis) : Results.NotFound();
        })
        .WithName("AnalyzeProductSustainability")
        .Produces<SustainabilityAnalysisDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}

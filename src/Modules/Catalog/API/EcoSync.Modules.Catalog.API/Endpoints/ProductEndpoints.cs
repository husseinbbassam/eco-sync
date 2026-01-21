using EcoSync.Modules.Catalog.Application.Products.Commands.CreateProduct;
using EcoSync.Modules.Catalog.Application.Products.Commands.UpdateProductPrice;
using EcoSync.Modules.Catalog.Application.Products.Queries.GetProduct;
using EcoSync.Modules.Catalog.Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EcoSync.Modules.Catalog.API.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/catalog/products").WithTags("Products");

        group.MapPost("/", async (CreateProductCommand command, ISender sender) =>
        {
            var productId = await sender.Send(command);
            return Results.Created($"/api/catalog/products/{productId}", new { id = productId });
        })
        .WithName("CreateProduct")
        .Produces<Guid>(StatusCodes.Status201Created);

        group.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductQuery(id);
            var product = await sender.Send(query);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        })
        .WithName("GetProduct")
        .Produces<ProductDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/", async (ISender sender, bool? activeOnly, int? category) =>
        {
            var query = new GetProductsQuery(activeOnly, category);
            var products = await sender.Send(query);
            return Results.Ok(products);
        })
        .WithName("GetProducts")
        .Produces<IEnumerable<ProductDto>>(StatusCodes.Status200OK);

        group.MapPut("/{id:guid}/price", async (Guid id, UpdateProductPriceRequest request, ISender sender) =>
        {
            var command = new UpdateProductPriceCommand(id, request.NewPrice, request.Currency);
            await sender.Send(command);
            return Results.NoContent();
        })
        .WithName("UpdateProductPrice")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }

    private record UpdateProductPriceRequest(decimal NewPrice, string Currency);
}

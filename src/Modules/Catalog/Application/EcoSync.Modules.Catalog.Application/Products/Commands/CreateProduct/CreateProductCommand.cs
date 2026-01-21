using EcoSync.BuildingBlocks.Application;

namespace EcoSync.Modules.Catalog.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    string Material,
    decimal Price,
    string Currency,
    int Category,
    int StockQuantity) : ICommand<Guid>;

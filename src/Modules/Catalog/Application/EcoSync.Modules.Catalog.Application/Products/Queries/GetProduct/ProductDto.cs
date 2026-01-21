namespace EcoSync.Modules.Catalog.Application.Products.Queries.GetProduct;

public sealed record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    string Category,
    int StockQuantity,
    bool IsActive);

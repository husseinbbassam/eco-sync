using EcoSync.BuildingBlocks.Application;

namespace EcoSync.Modules.Catalog.Application.Products.Queries.GetProduct;

public sealed record GetProductQuery(Guid ProductId) : IQuery<ProductDto?>;

using EcoSync.BuildingBlocks.Application;
using EcoSync.Modules.Catalog.Application.Products.Queries.GetProduct;

namespace EcoSync.Modules.Catalog.Application.Products.Queries.GetProducts;

public sealed record GetProductsQuery(bool? ActiveOnly = null, int? Category = null) : IQuery<IEnumerable<ProductDto>>;

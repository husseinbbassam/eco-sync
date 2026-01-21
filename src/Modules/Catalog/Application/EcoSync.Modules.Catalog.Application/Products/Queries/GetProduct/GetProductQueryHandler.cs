using EcoSync.BuildingBlocks.Application;
using EcoSync.Modules.Catalog.Domain.Products;
using Mapster;

namespace EcoSync.Modules.Catalog.Application.Products.Queries.GetProduct;

public sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
            return null;

        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price.Amount,
            product.Price.Currency,
            product.Category.ToString(),
            product.StockQuantity,
            product.IsActive);
    }
}

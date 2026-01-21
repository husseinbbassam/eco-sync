using EcoSync.BuildingBlocks.Application;
using EcoSync.Modules.Catalog.Application.Products.Queries.GetProduct;
using EcoSync.Modules.Catalog.Domain.Products;

namespace EcoSync.Modules.Catalog.Application.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Product> products;

        if (request.ActiveOnly == true)
        {
            products = await _productRepository.GetActiveProductsAsync(cancellationToken);
        }
        else if (request.Category.HasValue)
        {
            var category = (ProductCategory)request.Category.Value;
            products = await _productRepository.GetByCategoryAsync(category, cancellationToken);
        }
        else
        {
            products = await _productRepository.GetActiveProductsAsync(cancellationToken);
        }

        return products.Select(p => new ProductDto(
            p.Id,
            p.Name,
            p.Description,
            p.Material,
            p.Price.Amount,
            p.Price.Currency,
            p.Category.ToString(),
            p.StockQuantity,
            p.IsActive));
    }
}

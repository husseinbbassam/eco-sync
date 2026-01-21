using EcoSync.BuildingBlocks.Domain;

namespace EcoSync.Modules.Catalog.Domain.Products;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetActiveProductsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetByCategoryAsync(ProductCategory category, CancellationToken cancellationToken = default);
}

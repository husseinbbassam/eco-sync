using EcoSync.Modules.Catalog.Domain.Products;
using EcoSync.Modules.Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EcoSync.Modules.Catalog.Infrastructure.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _dbContext;

    public ProductRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Product aggregate, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(aggregate, cancellationToken);
    }

    public void Update(Product aggregate)
    {
        _dbContext.Products.Update(aggregate);
    }

    public void Remove(Product aggregate)
    {
        _dbContext.Products.Remove(aggregate);
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .Where(p => p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(ProductCategory category, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .Where(p => p.Category == category && p.IsActive)
            .ToListAsync(cancellationToken);
    }
}

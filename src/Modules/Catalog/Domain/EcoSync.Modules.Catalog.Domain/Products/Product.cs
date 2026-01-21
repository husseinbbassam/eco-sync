using EcoSync.BuildingBlocks.Domain;

namespace EcoSync.Modules.Catalog.Domain.Products;

public sealed class Product : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public ProductCategory Category { get; private set; }
    public int StockQuantity { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Product(
        Guid id,
        string name,
        string description,
        Money price,
        ProductCategory category,
        int stockQuantity) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        StockQuantity = stockQuantity;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    private Product() : base()
    {
        Name = string.Empty;
        Description = string.Empty;
        Price = Money.Zero();
        Category = ProductCategory.General;
    }

    public static Product Create(
        string name,
        string description,
        Money price,
        ProductCategory category,
        int stockQuantity)
    {
        var product = new Product(
            Guid.NewGuid(),
            name,
            description,
            price,
            category,
            stockQuantity);

        product.RaiseDomainEvent(new ProductCreatedDomainEvent(product.Id, product.Name));

        return product;
    }

    public void UpdatePrice(Money newPrice)
    {
        if (newPrice.Amount <= 0)
            throw new InvalidOperationException("Price must be greater than zero");

        Price = newPrice;
        UpdatedAt = DateTime.UtcNow;

        RaiseDomainEvent(new ProductPriceChangedDomainEvent(Id, Price));
    }

    public void UpdateStock(int quantity)
    {
        if (quantity < 0)
            throw new InvalidOperationException("Stock quantity cannot be negative");

        StockQuantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}

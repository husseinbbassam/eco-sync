using EcoSync.BuildingBlocks.Application;
using EcoSync.IntegrationEvents.Catalog;
using EcoSync.Modules.Catalog.Domain.Products;
using MediatR;

namespace EcoSync.Modules.Catalog.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var money = Money.Create(request.Price, request.Currency);
        var category = (ProductCategory)request.Category;

        var product = Product.Create(
            request.Name,
            request.Description,
            money,
            category,
            request.StockQuantity);

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var integrationEvent = new ProductCreatedIntegrationEvent
        {
            ProductId = product.Id,
            ProductName = product.Name
        };

        await _publisher.Publish(integrationEvent, cancellationToken);

        return product.Id;
    }
}

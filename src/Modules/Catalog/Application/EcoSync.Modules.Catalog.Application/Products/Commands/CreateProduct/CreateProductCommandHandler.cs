using EcoSync.BuildingBlocks.Application;
using EcoSync.Modules.Catalog.Domain.Products;

namespace EcoSync.Modules.Catalog.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
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

        return product.Id;
    }
}

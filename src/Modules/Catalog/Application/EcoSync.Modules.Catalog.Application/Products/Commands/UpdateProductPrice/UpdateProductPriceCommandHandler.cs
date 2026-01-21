using EcoSync.BuildingBlocks.Application;
using EcoSync.Modules.Catalog.Domain.Products;

namespace EcoSync.Modules.Catalog.Application.Products.Commands.UpdateProductPrice;

public sealed class UpdateProductPriceCommandHandler : ICommandHandler<UpdateProductPriceCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductPriceCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
            throw new InvalidOperationException($"Product with ID {request.ProductId} not found");

        var newPrice = Money.Create(request.NewPrice, request.Currency);
        product.UpdatePrice(newPrice);

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

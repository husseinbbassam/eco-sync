using EcoSync.BuildingBlocks.Application;
using EcoSync.IntegrationEvents.Catalog;
using EcoSync.Modules.Logistics.Domain.Shipments;
using MediatR;

namespace EcoSync.Modules.Logistics.Application.IntegrationEventHandlers;

public sealed class ProductCreatedIntegrationEventHandler : INotificationHandler<ProductCreatedIntegrationEvent>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductCreatedIntegrationEventHandler(
        IShipmentRepository shipmentRepository,
        IUnitOfWork unitOfWork)
    {
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProductCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var shipment = Shipment.CreatePending(notification.ProductId, notification.ProductName);

        await _shipmentRepository.AddAsync(shipment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

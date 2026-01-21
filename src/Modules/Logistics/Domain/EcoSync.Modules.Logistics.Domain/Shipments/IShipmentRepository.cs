using EcoSync.BuildingBlocks.Domain;

namespace EcoSync.Modules.Logistics.Domain.Shipments;

public interface IShipmentRepository : IRepository<Shipment>
{
    Task<IEnumerable<Shipment>> GetByStatusAsync(ShipmentStatus status, CancellationToken cancellationToken = default);
    Task<Shipment?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
}

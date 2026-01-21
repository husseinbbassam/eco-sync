using EcoSync.BuildingBlocks.Domain;

namespace EcoSync.Modules.Logistics.Domain.Shipments;

public sealed class Shipment : AggregateRoot
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public ShipmentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Shipment(
        Guid id,
        Guid productId,
        string productName) : base(id)
    {
        ProductId = productId;
        ProductName = productName;
        Status = ShipmentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    private Shipment() : base()
    {
        ProductName = string.Empty;
    }

    public static Shipment CreatePending(Guid productId, string productName)
    {
        var shipment = new Shipment(
            Guid.NewGuid(),
            productId,
            productName);

        return shipment;
    }

    public void MarkInTransit()
    {
        if (Status != ShipmentStatus.Pending)
            throw new InvalidOperationException("Only pending shipments can be marked as in transit");

        Status = ShipmentStatus.InTransit;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkDelivered()
    {
        if (Status != ShipmentStatus.InTransit)
            throw new InvalidOperationException("Only in-transit shipments can be marked as delivered");

        Status = ShipmentStatus.Delivered;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == ShipmentStatus.Delivered)
            throw new InvalidOperationException("Delivered shipments cannot be cancelled");

        Status = ShipmentStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}

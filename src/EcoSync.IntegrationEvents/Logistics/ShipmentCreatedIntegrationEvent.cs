namespace EcoSync.IntegrationEvents.Logistics;

public record ShipmentCreatedIntegrationEvent : IntegrationEvent
{
    public Guid ShipmentId { get; init; }
    public string Origin { get; init; } = string.Empty;
    public string Destination { get; init; } = string.Empty;
    public double DistanceKm { get; init; }
    public string TransportMode { get; init; } = string.Empty;
    public double WeightKg { get; init; }
}

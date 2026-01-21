namespace EcoSync.IntegrationEvents.Catalog;

public record ProductCreatedIntegrationEvent : IntegrationEvent
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
}

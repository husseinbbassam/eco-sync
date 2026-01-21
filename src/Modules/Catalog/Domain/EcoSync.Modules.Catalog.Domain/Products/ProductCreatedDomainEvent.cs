using EcoSync.BuildingBlocks.Domain;

namespace EcoSync.Modules.Catalog.Domain.Products;

public sealed record ProductCreatedDomainEvent(Guid ProductId, string ProductName) : DomainEvent;

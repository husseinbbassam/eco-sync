using EcoSync.BuildingBlocks.Domain;

namespace EcoSync.Modules.Catalog.Domain.Products;

public sealed record ProductPriceChangedDomainEvent(Guid ProductId, Money NewPrice) : DomainEvent;

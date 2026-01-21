// This is a backwards compatibility shim - use EcoSync.SharedKernel.Domain.DomainEventBase directly
namespace EcoSync.BuildingBlocks.Domain;

public abstract record DomainEvent : EcoSync.SharedKernel.Domain.DomainEventBase
{
}

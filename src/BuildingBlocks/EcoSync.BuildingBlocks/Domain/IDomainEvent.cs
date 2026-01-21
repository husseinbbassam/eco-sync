// This is a backwards compatibility shim - use EcoSync.SharedKernel.Domain.IDomainEvent directly
using MediatR;

namespace EcoSync.BuildingBlocks.Domain;

public interface IDomainEvent : EcoSync.SharedKernel.Domain.IDomainEvent
{
}

using MediatR;

namespace EcoSync.BuildingBlocks.Domain;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
}

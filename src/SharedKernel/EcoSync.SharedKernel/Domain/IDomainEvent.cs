using MediatR;

namespace EcoSync.SharedKernel.Domain;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
}

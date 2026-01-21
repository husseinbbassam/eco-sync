// This is a backwards compatibility shim - use EcoSync.SharedKernel.Domain.AggregateRoot directly
namespace EcoSync.BuildingBlocks.Domain;

public abstract class AggregateRoot : EcoSync.SharedKernel.Domain.AggregateRoot
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }

    protected AggregateRoot() : base()
    {
    }
}

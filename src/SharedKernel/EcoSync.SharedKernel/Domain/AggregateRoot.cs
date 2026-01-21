namespace EcoSync.SharedKernel.Domain;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }

    protected AggregateRoot() : base()
    {
    }
}

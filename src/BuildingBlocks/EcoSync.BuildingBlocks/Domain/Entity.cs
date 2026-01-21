// This is a backwards compatibility shim - use EcoSync.SharedKernel.Domain.Entity directly
namespace EcoSync.BuildingBlocks.Domain;

public abstract class Entity : EcoSync.SharedKernel.Domain.Entity
{
    protected Entity(Guid id) : base(id)
    {
    }

    protected Entity() : base()
    {
    }
}

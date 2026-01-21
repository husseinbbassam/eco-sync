# EcoSync SharedKernel

The SharedKernel contains common domain primitives and abstractions that are shared across all modules in the EcoSync application.

## Contents

### Domain Building Blocks

- **Entity**: Base class for domain entities with identity
- **AggregateRoot**: Base class for aggregate roots that manage domain invariants
- **ValueObject**: Base class for value objects without identity
- **IDomainEvent**: Interface for domain events
- **DomainEventBase**: Base record for implementing domain events

### Functional Error Handling

- **Result<T>**: Generic result type for functional error handling
  - `Result<T>.Success(value)` - Creates a successful result
  - `Result<T>.Failure(error)` - Creates a failed result with error message
  - Properties: `IsSuccess`, `IsFailure`, `Value`, `Error`

## Usage

All modules should reference the SharedKernel through the BuildingBlocks project, which provides backward compatibility wrappers.

### Example: Creating a Domain Entity

```csharp
using EcoSync.SharedKernel.Domain;

public sealed class Product : AggregateRoot
{
    public string Name { get; private set; }
    
    private Product(Guid id, string name) : base(id)
    {
        Name = name;
    }
    
    public static Product Create(string name)
    {
        var product = new Product(Guid.NewGuid(), name);
        product.RaiseDomainEvent(new ProductCreatedEvent(product.Id));
        return product;
    }
}
```

### Example: Using Result<T>

```csharp
using EcoSync.SharedKernel;

public Result<Product> FindProduct(Guid id)
{
    var product = _repository.FindById(id);
    
    if (product == null)
        return Result<Product>.Failure("Product not found");
        
    return Result<Product>.Success(product);
}
```

### Example: Creating a Domain Event

```csharp
using EcoSync.SharedKernel.Domain;

public sealed record ProductCreatedEvent(Guid ProductId) : DomainEventBase;
```

## Architecture

The SharedKernel is referenced by:
- **BuildingBlocks**: Provides backward compatibility and additional infrastructure abstractions
- **All Domain Modules**: Through BuildingBlocks reference (transitive)

This ensures a consistent domain model across all bounded contexts while maintaining clear architectural boundaries.

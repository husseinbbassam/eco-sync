# EcoSync Project

EcoSync is a modular monolith backend application built with .NET 9 and C# 13, applying Domain-Driven Design (DDD) principles and Clean Architecture.

## Architecture Overview

### Modular Monolith
The application is organized into four distinct modules:
- **Catalog**: Product management and catalog functionality
- **Logistics**: Shipment and delivery management
- **Sustainability**: Environmental impact tracking and sustainability scoring
- **Identity**: User authentication and authorization

### Clean Architecture
Each module follows Clean Architecture with four layers:
- **Domain**: Core business logic, aggregates, value objects, and domain events
- **Application**: Use cases, commands, queries, and handlers
- **Infrastructure**: Data persistence, external services, and technical implementations
- **API**: HTTP endpoints and module registration

### DDD Patterns
- **Aggregates**: Encapsulate business rules and maintain consistency
- **Value Objects**: Immutable objects that describe domain concepts
- **Domain Events**: Capture important business events within aggregates
- **Repositories**: Abstract data access for aggregates
- **Outbox Pattern**: Ensure reliable event publishing for inter-module communication

## Technology Stack

- **.NET 9** with C# 13
- **PostgreSQL**: Primary database using EF Core
- **Redis**: Caching layer
- **MediatR**: In-process messaging for CQRS and event handling
- **FluentValidation**: Input validation
- **Mapster**: Object mapping
- **Serilog**: Structured logging
- **Swagger/OpenAPI**: API documentation

## Project Structure

```
src/
├── BuildingBlocks/
│   └── EcoSync.BuildingBlocks/          # Shared domain primitives and abstractions
├── EcoSync.IntegrationEvents/           # Shared integration event contracts
├── Modules/
│   ├── Catalog/
│   │   ├── Domain/                      # Product aggregate, value objects
│   │   ├── Application/                 # Commands, queries, handlers
│   │   ├── Infrastructure/              # EF Core, repositories
│   │   └── API/                         # Endpoints
│   ├── Logistics/
│   ├── Sustainability/
│   └── Identity/
└── API/
    └── EcoSync.API/                     # API host project
```

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) and Docker Compose
- PostgreSQL (if running without Docker)

### Running with Docker Compose

1. Clone the repository
```bash
git clone https://github.com/husseinbbassam/eco-sync.git
cd eco-sync
```

2. Start all services
```bash
docker-compose up -d
```

3. Access the API
- API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

### Running Locally

1. Ensure PostgreSQL is running

2. Update connection string in `src/API/EcoSync.API/appsettings.json`

3. Run the API
```bash
cd src/API/EcoSync.API
dotnet run
```

## API Endpoints

### Catalog Module

#### Products
- `POST /api/catalog/products` - Create a new product
- `GET /api/catalog/products/{id}` - Get product by ID
- `GET /api/catalog/products` - List products (with filters)
- `PUT /api/catalog/products/{id}/price` - Update product price

## Module Features

### Catalog Module
- Product management with categories
- Stock tracking
- Price management
- Product activation/deactivation

### Logistics Module
- Shipment creation and tracking
- Pending shipment creation via integration events
- Integration with Catalog module via ProductCreatedIntegrationEvent
- Shipment status management (Pending, InTransit, Delivered, Cancelled)

### Sustainability Module (Planned)
- Carbon footprint calculation
- Sustainability score computation
- Environmental impact reports
- Integration with logistics data

### Identity Module (Planned)
- User registration and authentication
- Role-based access control
- JWT token management

## Development

### Building the Solution
```bash
dotnet build
```

### Running Tests (when available)
```bash
dotnet test
```

### Database Migrations
```bash
cd src/Modules/Catalog/Infrastructure/EcoSync.Modules.Catalog.Infrastructure
dotnet ef migrations add InitialCreate --context CatalogDbContext
dotnet ef database update --context CatalogDbContext
```

## Architecture Principles

1. **Module Independence**: Each module can evolve independently
2. **Encapsulation**: Domain logic is protected within aggregates
3. **Event-Driven**: Modules communicate via integration events
4. **CQRS**: Separate read and write operations
5. **Clean Dependencies**: Dependencies point inward toward the domain

## Contributing

Contributions are welcome! Please follow the established patterns and architecture principles.

## License

This project is licensed under the MIT License.

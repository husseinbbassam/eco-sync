# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY EcoSync.sln ./
COPY src/BuildingBlocks/EcoSync.BuildingBlocks/*.csproj ./src/BuildingBlocks/EcoSync.BuildingBlocks/
COPY src/EcoSync.IntegrationEvents/*.csproj ./src/EcoSync.IntegrationEvents/

# Catalog module
COPY src/Modules/Catalog/Domain/EcoSync.Modules.Catalog.Domain/*.csproj ./src/Modules/Catalog/Domain/EcoSync.Modules.Catalog.Domain/
COPY src/Modules/Catalog/Application/EcoSync.Modules.Catalog.Application/*.csproj ./src/Modules/Catalog/Application/EcoSync.Modules.Catalog.Application/
COPY src/Modules/Catalog/Infrastructure/EcoSync.Modules.Catalog.Infrastructure/*.csproj ./src/Modules/Catalog/Infrastructure/EcoSync.Modules.Catalog.Infrastructure/
COPY src/Modules/Catalog/API/EcoSync.Modules.Catalog.API/*.csproj ./src/Modules/Catalog/API/EcoSync.Modules.Catalog.API/

# Logistics module
COPY src/Modules/Logistics/Domain/EcoSync.Modules.Logistics.Domain/*.csproj ./src/Modules/Logistics/Domain/EcoSync.Modules.Logistics.Domain/
COPY src/Modules/Logistics/Application/EcoSync.Modules.Logistics.Application/*.csproj ./src/Modules/Logistics/Application/EcoSync.Modules.Logistics.Application/
COPY src/Modules/Logistics/Infrastructure/EcoSync.Modules.Logistics.Infrastructure/*.csproj ./src/Modules/Logistics/Infrastructure/EcoSync.Modules.Logistics.Infrastructure/
COPY src/Modules/Logistics/API/EcoSync.Modules.Logistics.API/*.csproj ./src/Modules/Logistics/API/EcoSync.Modules.Logistics.API/

# Sustainability module
COPY src/Modules/Sustainability/Domain/EcoSync.Modules.Sustainability.Domain/*.csproj ./src/Modules/Sustainability/Domain/EcoSync.Modules.Sustainability.Domain/
COPY src/Modules/Sustainability/Application/EcoSync.Modules.Sustainability.Application/*.csproj ./src/Modules/Sustainability/Application/EcoSync.Modules.Sustainability.Application/
COPY src/Modules/Sustainability/Infrastructure/EcoSync.Modules.Sustainability.Infrastructure/*.csproj ./src/Modules/Sustainability/Infrastructure/EcoSync.Modules.Sustainability.Infrastructure/
COPY src/Modules/Sustainability/API/EcoSync.Modules.Sustainability.API/*.csproj ./src/Modules/Sustainability/API/EcoSync.Modules.Sustainability.API/

# Identity module
COPY src/Modules/Identity/Domain/EcoSync.Modules.Identity.Domain/*.csproj ./src/Modules/Identity/Domain/EcoSync.Modules.Identity.Domain/
COPY src/Modules/Identity/Application/EcoSync.Modules.Identity.Application/*.csproj ./src/Modules/Identity/Application/EcoSync.Modules.Identity.Application/
COPY src/Modules/Identity/Infrastructure/EcoSync.Modules.Identity.Infrastructure/*.csproj ./src/Modules/Identity/Infrastructure/EcoSync.Modules.Identity.Infrastructure/
COPY src/Modules/Identity/API/EcoSync.Modules.Identity.API/*.csproj ./src/Modules/Identity/API/EcoSync.Modules.Identity.API/

# API host
COPY src/API/EcoSync.API/*.csproj ./src/API/EcoSync.API/

# Restore dependencies
RUN dotnet restore

# Copy source code
COPY . .

# Build the application
WORKDIR /src/src/API/EcoSync.API
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80

ENTRYPOINT ["dotnet", "EcoSync.API.dll"]

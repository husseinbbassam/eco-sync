using EcoSync.Modules.Logistics.Domain.Shipments;
using EcoSync.Modules.Logistics.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EcoSync.Modules.Logistics.Infrastructure.Repositories;

public sealed class ShipmentRepository : IShipmentRepository
{
    private readonly LogisticsDbContext _dbContext;

    public ShipmentRepository(LogisticsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Shipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Shipments.FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(Shipment aggregate, CancellationToken cancellationToken = default)
    {
        await _dbContext.Shipments.AddAsync(aggregate, cancellationToken);
    }

    public void Update(Shipment aggregate)
    {
        _dbContext.Shipments.Update(aggregate);
    }

    public void Remove(Shipment aggregate)
    {
        _dbContext.Shipments.Remove(aggregate);
    }

    public async Task<IEnumerable<Shipment>> GetByStatusAsync(ShipmentStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Shipments
            .Where(s => s.Status == status)
            .ToListAsync(cancellationToken);
    }

    public async Task<Shipment?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Shipments
            .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);
    }
}

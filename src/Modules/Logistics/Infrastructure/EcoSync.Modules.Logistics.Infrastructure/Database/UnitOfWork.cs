using EcoSync.BuildingBlocks.Application;

namespace EcoSync.Modules.Logistics.Infrastructure.Database;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly LogisticsDbContext _dbContext;

    public UnitOfWork(LogisticsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

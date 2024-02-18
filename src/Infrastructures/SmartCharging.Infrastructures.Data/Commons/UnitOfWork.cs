using SmartCharging.Core.Domain.Commons;

namespace SmartCharging.Infrastructures.Data.Commons;

public class UnitOfWork : IUnitOfWork
{
    private readonly SmartChargingDbContext _dbContext;

    public UnitOfWork(SmartChargingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
    public void Save() => _dbContext.SaveChanges();
}
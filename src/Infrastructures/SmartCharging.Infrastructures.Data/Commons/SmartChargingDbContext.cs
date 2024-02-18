using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Domain.GroupItemAggregate;

namespace SmartCharging.Infrastructures.Data.Commons;
public class SmartChargingDbContext : DbContext
{
    public SmartChargingDbContext(DbContextOptions<SmartChargingDbContext> options) : base(options)
    {
    }

    public DbSet<GroupItem> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var changeCount = base.SaveChangesAsync(cancellationToken);
        return changeCount;
    }
}


using Microsoft.EntityFrameworkCore;
using SmartCharging.Infrastructures.Data.Commons;

namespace Test.SmartCharging.Infrastructure.Infrastructure;

public class DatabaseTestBase : IDisposable
{
    protected readonly SmartChargingDbContext Context;

    public DatabaseTestBase()
    {
        var options = new DbContextOptionsBuilder<SmartChargingDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        Context = new SmartChargingDbContext(options);

        Context.Database.EnsureCreated();

        DatabaseInitializer.Initialize(Context);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}

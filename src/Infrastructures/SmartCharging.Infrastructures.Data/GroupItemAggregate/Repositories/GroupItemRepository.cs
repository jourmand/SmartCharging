using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Domain.GroupItemAggregate;
using SmartCharging.Core.Domain.GroupItemAggregate.Contracts;
using SmartCharging.Infrastructures.Data.Commons;

namespace SmartCharging.Infrastructures.Data.GroupItemAggregate.Repositories;
public class GroupItemRepository : IGroupItemRepository
{
    private readonly SmartChargingDbContext _smartChargingDbContext;

    public GroupItemRepository(SmartChargingDbContext smartChargingDbContext)
    {
        _smartChargingDbContext = smartChargingDbContext;
    }

    public async Task CreateAsync(GroupItem group, CancellationToken cancellationToken = default) =>
        await _smartChargingDbContext.Groups.AddAsync(group, cancellationToken);

    public async Task<GroupItem> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _smartChargingDbContext.Groups
                .Include(o => o.ChargeStations)
                .ThenInclude(o => o.Connectors)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public void RemoveAsync(GroupItem group) =>
        _smartChargingDbContext.Groups.Remove(group);
}
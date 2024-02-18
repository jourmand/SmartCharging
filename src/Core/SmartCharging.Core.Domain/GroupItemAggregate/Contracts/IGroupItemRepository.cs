namespace SmartCharging.Core.Domain.GroupItemAggregate.Contracts;
public interface IGroupItemRepository
{
    Task CreateAsync(GroupItem group, CancellationToken cancellationToken = default);
    Task<GroupItem> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void RemoveAsync(GroupItem group);
}
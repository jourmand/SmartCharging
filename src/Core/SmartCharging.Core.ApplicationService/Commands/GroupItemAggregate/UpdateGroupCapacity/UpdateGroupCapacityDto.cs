namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.UpdateGroupCapacity;
public class UpdateGroupCapacityDto
{
    public Guid Id { get; set; }
    public int CapacityInAmps { get; set; }
    public string Name { get; set; }
}
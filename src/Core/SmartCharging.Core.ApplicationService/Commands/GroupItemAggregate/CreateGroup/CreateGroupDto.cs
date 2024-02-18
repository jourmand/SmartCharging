namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.CreateGroup;
public class CreateGroupDto
{
    public Guid Id { get; set; }
    public int CapacityInAmps { get; set; }
    public string Name { get; set; }
}
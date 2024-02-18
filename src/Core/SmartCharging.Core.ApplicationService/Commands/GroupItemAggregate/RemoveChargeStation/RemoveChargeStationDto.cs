namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.RemoveChargeStation;
public class RemoveChargeStationDto
{
    public Guid ChargeStationId { get; set; }
    public Guid GroupId { get; set; }
}
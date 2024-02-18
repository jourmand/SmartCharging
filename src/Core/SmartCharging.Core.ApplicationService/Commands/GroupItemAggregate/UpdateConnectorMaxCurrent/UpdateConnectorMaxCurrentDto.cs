namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.UpdateConnectorMaxCurrent;
public class UpdateConnectorMaxCurrentDto
{
    public Guid ChargeStationId { get; set; }
    public int ConnectorIdentifier { get; set; }
    public int NewMaxCurrent { get; set; }
}
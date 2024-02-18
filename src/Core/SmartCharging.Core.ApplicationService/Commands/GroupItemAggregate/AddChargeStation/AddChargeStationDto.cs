using static SmartCharging.Core.Domain.GroupItemAggregate.WebApiCommand;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.AddChargeStation;
public class AddChargeStationDto
{
    public Guid ChargeStationId { get; set; }
    public string Name { get; set; }
    public List<CreateConnectorCommand> Connectors { get; set; } = [];
}
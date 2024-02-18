using MediatR;
using System.ComponentModel.DataAnnotations;
using static SmartCharging.Core.Domain.GroupItemAggregate.WebApiCommand;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.AddChargeStation;
public class AddChargeStationCommand : IRequest<AddChargeStationDto>
{
    [Required]
    public Guid GroupId { get; set; }

    [Required]
    public Guid ChargeStationId { get; set; }

    [Required]
    public string Name { get; set; }
    public List<CreateConnectorCommand> Connectors { get; set; }
}


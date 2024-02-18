using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.UpdateConnectorMaxCurrent;
public class UpdateConnectorMaxCurrentCommand : IRequest<UpdateConnectorMaxCurrentDto>
{
    [Required]
    public Guid GroupId { get; set; }

    [Required]
    public Guid ChargeStationId { get; set; }

    [Required]
    public int ConnectorIdentifier { get; set; }

    [Required]
    public int NewMaxCurrent { get; set; }
}
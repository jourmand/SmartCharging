using MediatR;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.RemoveChargeStation;
public record RemoveChargeStationCommand(Guid GroupId, Guid ChargeStationId) : IRequest<RemoveChargeStationDto> { }
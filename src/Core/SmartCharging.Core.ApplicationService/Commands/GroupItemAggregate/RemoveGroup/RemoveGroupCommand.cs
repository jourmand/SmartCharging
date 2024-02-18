using MediatR;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.RemoveGroup;
public record RemoveGroupCommand(Guid GroupId) : IRequest<RemoveGroupDto> { }
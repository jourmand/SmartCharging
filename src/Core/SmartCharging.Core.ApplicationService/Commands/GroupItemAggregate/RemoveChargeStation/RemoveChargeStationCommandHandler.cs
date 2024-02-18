using MediatR;
using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate;
using SmartCharging.Core.Domain.GroupItemAggregate.Contracts;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.RemoveChargeStation;
public class RemoveChargeStationCommandHandler : IRequestHandler<RemoveChargeStationCommand, RemoveChargeStationDto>
{
    private readonly IGroupItemRepository _groupItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveChargeStationCommandHandler(IGroupItemRepository groupItemRepository,
        IUnitOfWork unitOfWork)
    {
        _groupItemRepository = groupItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RemoveChargeStationDto> Handle(RemoveChargeStationCommand request, CancellationToken cancellationToken)
    {
        var groupItem = await _groupItemRepository.GetAsync(request.GroupId, cancellationToken);
        groupItem.RemoveChargeStationFromGroup(new WebApiCommand.RemoveChargeStationCommand { ChargeStationId = request.ChargeStationId });

        await _unitOfWork.SaveAsync(cancellationToken);

        return new RemoveChargeStationDto
        {
            ChargeStationId = request.ChargeStationId,
            GroupId = request.GroupId
        };
    }
}
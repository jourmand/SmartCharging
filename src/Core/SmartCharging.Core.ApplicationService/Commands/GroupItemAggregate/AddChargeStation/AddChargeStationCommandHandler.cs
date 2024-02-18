using MediatR;
using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate.Contracts;
using static SmartCharging.Core.Domain.GroupItemAggregate.WebApiCommand;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.AddChargeStation;
public class AddChargeStationCommandHandler : IRequestHandler<AddChargeStationCommand, AddChargeStationDto>
{
    private readonly IGroupItemRepository _groupItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddChargeStationCommandHandler(IGroupItemRepository groupItemRepository,
        IUnitOfWork unitOfWork)
    {
        _groupItemRepository = groupItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AddChargeStationDto> Handle(AddChargeStationCommand request, CancellationToken cancellationToken)
    {
        var groupItem = await _groupItemRepository.GetAsync(request.GroupId, cancellationToken);
        groupItem.AddChargeStationToGroup(new CreateChargeStationCommand
        {
            Id = request.ChargeStationId,
            Name = request.Name,
            Connectors = request.Connectors
        });

        await _unitOfWork.SaveAsync(cancellationToken);

        return new AddChargeStationDto
        {
            ChargeStationId = request.ChargeStationId,
            Name = request.Name,
            Connectors = request.Connectors
        };
    }
}


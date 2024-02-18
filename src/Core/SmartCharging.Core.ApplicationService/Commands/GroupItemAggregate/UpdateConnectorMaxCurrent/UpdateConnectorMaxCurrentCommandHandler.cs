using MediatR;
using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate;
using SmartCharging.Core.Domain.GroupItemAggregate.Contracts;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.UpdateConnectorMaxCurrent;
public class UpdateConnectorMaxCurrentCommandHandler : IRequestHandler<UpdateConnectorMaxCurrentCommand, UpdateConnectorMaxCurrentDto>
{
    private readonly IGroupItemRepository _groupItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateConnectorMaxCurrentCommandHandler(IGroupItemRepository groupItemRepository,
        IUnitOfWork unitOfWork)
    {
        _groupItemRepository = groupItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateConnectorMaxCurrentDto> Handle(UpdateConnectorMaxCurrentCommand request, CancellationToken cancellationToken)
    {
        var groupItem = await _groupItemRepository.GetAsync(request.GroupId, cancellationToken);
        groupItem.UpdateConnectorMaxCurrent(new WebApiCommand.UpdateConnectorMaxCurrentCommand
        {
            ChargeStationId = request.ChargeStationId,
            ConnectorIdentifier = request.ConnectorIdentifier,
            NewMaxCurrent = request.NewMaxCurrent
        });

        await _unitOfWork.SaveAsync(cancellationToken);

        return new UpdateConnectorMaxCurrentDto
        {
            ChargeStationId = request.ChargeStationId,
            NewMaxCurrent = request.NewMaxCurrent,
            ConnectorIdentifier = request.ConnectorIdentifier
        };
    }
}


using MediatR;
using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate.Contracts;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.UpdateGroupCapacity;
public class UpdateGroupCapacityCommandHandler : IRequestHandler<UpdateGroupCapacityCommand, UpdateGroupCapacityDto>
{
    private readonly IGroupItemRepository _groupItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGroupCapacityCommandHandler(IGroupItemRepository groupItemRepository,
        IUnitOfWork unitOfWork)
    {
        _groupItemRepository = groupItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateGroupCapacityDto> Handle(UpdateGroupCapacityCommand request, CancellationToken cancellationToken)
    {
        var groupItem = await _groupItemRepository.GetAsync(request.Id, cancellationToken);
        groupItem.UpdateGroupCapacity(new Domain.GroupItemAggregate.WebApiCommand.UpdateGroupCapacityCommand
        {
            NewCapacityInAmps = request.CapacityInAmps
        });

        await _unitOfWork.SaveAsync(cancellationToken);

        return new UpdateGroupCapacityDto
        {
            Id = groupItem.Id,
            CapacityInAmps = groupItem.CapacityInAmps,
            Name = groupItem.Name
        };
    }
}


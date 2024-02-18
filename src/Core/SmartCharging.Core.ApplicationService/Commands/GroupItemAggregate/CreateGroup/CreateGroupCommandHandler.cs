using MediatR;
using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate;
using SmartCharging.Core.Domain.GroupItemAggregate.Contracts;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.CreateGroup;
public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, CreateGroupDto>
{
    private readonly IGroupItemRepository _groupItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGroupCommandHandler(IGroupItemRepository groupItemRepository,
        IUnitOfWork unitOfWork)
    {
        _groupItemRepository = groupItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateGroupDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var groupItem = GroupItem.Create(new WebApiCommand.CreateGroupCommand
        {
            Id = request.Id,
            CapacityInAmps = request.CapacityInAmps,
            Name = request.Name
        });

        await _groupItemRepository.CreateAsync(groupItem, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return new CreateGroupDto
        {
            Id = groupItem.Id,
            CapacityInAmps = groupItem.CapacityInAmps,
            Name = groupItem.Name
        };
    }
}


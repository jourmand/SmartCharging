using MediatR;
using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate.Contracts;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.RemoveGroup;
public class RemoveGroupCommandHandler : IRequestHandler<RemoveGroupCommand, RemoveGroupDto>
{
    private readonly IGroupItemRepository _groupItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveGroupCommandHandler(IGroupItemRepository groupItemRepository,
        IUnitOfWork unitOfWork)
    {
        _groupItemRepository = groupItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RemoveGroupDto> Handle(RemoveGroupCommand request, CancellationToken cancellationToken)
    {
        var groupItem = await _groupItemRepository.GetAsync(request.GroupId, cancellationToken);
        _groupItemRepository.RemoveAsync(groupItem);
        await _unitOfWork.SaveAsync(cancellationToken);

        return new RemoveGroupDto
        {
            GroupId = request.GroupId
        };
    }
}
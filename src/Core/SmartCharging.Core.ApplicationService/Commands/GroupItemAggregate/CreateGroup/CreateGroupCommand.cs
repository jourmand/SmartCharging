using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.CreateGroup;
public class CreateGroupCommand : IRequest<CreateGroupDto>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public int CapacityInAmps { get; set; }

    [Required]
    public string Name { get; set; }
}


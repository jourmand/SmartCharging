using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.UpdateGroupCapacity;
public class UpdateGroupCapacityCommand : IRequest<UpdateGroupCapacityDto>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public int CapacityInAmps { get; set; }
}


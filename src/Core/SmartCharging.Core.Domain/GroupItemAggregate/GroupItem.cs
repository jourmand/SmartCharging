using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate.ValueObjects;
using static SmartCharging.Core.Domain.GroupItemAggregate.Exceptions.SmartChargingExceptions;
using static SmartCharging.Core.Domain.GroupItemAggregate.WebApiCommand;

namespace SmartCharging.Core.Domain.GroupItemAggregate;

public class GroupItem : AggregateRoot
{
    public string Name { get; private set; }
    public CapacityInAmps CapacityInAmps { get; private set; }

    private List<ChargeStation> _chargeStations = [];
    public IEnumerable<ChargeStation> ChargeStations => _chargeStations;

    public static GroupItem Create(CreateGroupCommand groupCommand) =>
       new()
       {
           Id = groupCommand.Id,
           Name = groupCommand.Name,
           CapacityInAmps = CapacityInAmps.Create(groupCommand.CapacityInAmps)
       };

    public void UpdateGroupCapacity(UpdateGroupCapacityCommand command) =>
        Apply(command);

    public void AddChargeStationToGroup(CreateChargeStationCommand command) =>
        Apply(command);

    public void RemoveChargeStationFromGroup(RemoveChargeStationCommand command)
    {
        if (!_chargeStations.Any(o => o.Id == command.ChargeStationId))
            throw new RecordNotFound("Station not found!");
        Apply(command);
    }

    public void UpdateConnectorMaxCurrent(UpdateConnectorMaxCurrentCommand command) =>
        Apply(command);

    protected override void When(object @event)
    {
        switch (@event)
        {
            case UpdateGroupCapacityCommand e:
                CapacityInAmps = CapacityInAmps.Create(e.NewCapacityInAmps);
                break;
            case CreateChargeStationCommand e:
                _chargeStations.Add(ChargeStation.Create(new CreateChargeStationCommand
                {
                    Id = e.Id,
                    Name = e.Name,
                    Connectors = e.Connectors
                }));
                break;
            case RemoveChargeStationCommand e:
                _chargeStations.Remove(ChargeStation.Create(e.ChargeStationId));
                break;
            case UpdateConnectorMaxCurrentCommand e:
                var chargeStation = _chargeStations.FirstOrDefault(o => o.Id == e.ChargeStationId) ??
                            throw new RecordNotFound($"There is no charge station in the group with ID {e.ChargeStationId}");
                var connector = chargeStation.Connectors.FirstOrDefault(o => o.Identifier == e.ConnectorIdentifier) ??
                            throw new RecordNotFound($"There is no connector in the charge station with ID {e.ConnectorIdentifier}");
                ApplyToEntity(connector, e);
                //connector.UpdateMaxCurrent(e.NewMaxCurrent);
                break;
        }
    }

    protected override void EnsureValidState()
    {
        if (CapacityInAmps < _chargeStations.Sum(o => o.Connectors.Sum(o => o.MaxCurrentInAmps)))
            throw new InvalidEntityState("Capacity should be greater than or equal to the sum of the Max current in Amps");
    }
}

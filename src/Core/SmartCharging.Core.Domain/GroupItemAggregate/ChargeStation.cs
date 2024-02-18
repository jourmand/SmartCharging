using SmartCharging.Core.Domain.Commons;
using static SmartCharging.Core.Domain.GroupItemAggregate.Exceptions.SmartChargingExceptions;
using static SmartCharging.Core.Domain.GroupItemAggregate.WebApiCommand;

namespace SmartCharging.Core.Domain.GroupItemAggregate;

public class ChargeStation : Entity<Guid>
{
    public string Name { get; private set; }

    private List<Connector> _connectors = [];
    public IEnumerable<Connector> Connectors => _connectors;

    internal static ChargeStation Create(CreateChargeStationCommand chargeStationCommand)
    {
        if (chargeStationCommand.Connectors != null && chargeStationCommand.Connectors.Count > 0 && chargeStationCommand.Connectors.Count <= 5)
            return new()
            {
                Id = chargeStationCommand.Id,
                Name = chargeStationCommand.Name,
                _connectors = chargeStationCommand.Connectors.Select(o => Connector.Create(
                    new CreateConnectorCommand { Identifier = o.Identifier, MaxCurrentInAmps = o.MaxCurrentInAmps })).ToList()
            };

        throw new InvalidEntityState("The ChargeStation cannot have more than 5 connectors");
    }

    internal static ChargeStation Create(Guid id) =>
        new() { Id = id };

    protected override void When(object @event) { }
}

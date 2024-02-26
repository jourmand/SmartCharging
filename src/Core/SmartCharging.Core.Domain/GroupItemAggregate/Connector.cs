using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate.ValueObjects;
using static SmartCharging.Core.Domain.GroupItemAggregate.WebApiCommand;

namespace SmartCharging.Core.Domain.GroupItemAggregate;

public class Connector : Entity<Guid>
{
    public ConnectorIdentifier Identifier { get; private set; }
    public CapacityInAmps MaxCurrentInAmps { get; private set; }

    internal static Connector Create(CreateConnectorCommand connectorCommand) =>
       new()
       {
           Id = Guid.NewGuid(),
           Identifier = ConnectorIdentifier.Create(connectorCommand.Identifier),
           MaxCurrentInAmps = CapacityInAmps.Create(connectorCommand.MaxCurrentInAmps)
       };

    protected override void When(object @event)
    {
        switch (@event)
        {
            case UpdateConnectorMaxCurrentCommand e:
                MaxCurrentInAmps = CapacityInAmps.Create(e.NewMaxCurrent);
                break;
        }
    }
}
using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate.Exceptions;

namespace SmartCharging.Core.Domain.GroupItemAggregate.ValueObjects;

public class ConnectorIdentifier : Value<ConnectorIdentifier>
{
    public int Value { get; private set; }
    private ConnectorIdentifier(int value)
    {
        Value = value;
    }

    public static ConnectorIdentifier Create(int value)
    {
        if (value is < 1 or > 5)
            throw new SmartChargingExceptions.InvalidEntityState($"Connector identifier should be between 1 and 5");
        return new ConnectorIdentifier(value);
    }

    public static implicit operator int(ConnectorIdentifier value) => value.Value;
}
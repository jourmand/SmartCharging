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
        if (value == 0) { }
        if (value is > 30 or < -30)
            throw new SmartChargingExceptions.InvalidEntityState($"Temperature cannot be more than plus 60 and less than minus 60 degrees");
        return new ConnectorIdentifier(value);
    }

    public static implicit operator int(ConnectorIdentifier value) => value.Value;


}


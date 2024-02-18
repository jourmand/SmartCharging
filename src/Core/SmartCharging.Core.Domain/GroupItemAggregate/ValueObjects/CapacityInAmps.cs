using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate.Exceptions;

namespace SmartCharging.Core.Domain.GroupItemAggregate.ValueObjects;

public class CapacityInAmps : Value<CapacityInAmps>
{
    public int Value { get; private set; }
    private CapacityInAmps(int value)
    {
        Value = value;
    }

    public static CapacityInAmps Create(int value)
    {
        if (value <= 0)
            throw new SmartChargingExceptions.InvalidEntityState($"Capacity cannot be below 0");

        return new CapacityInAmps(value);
    }

    public static implicit operator int(CapacityInAmps value) => value.Value;
}

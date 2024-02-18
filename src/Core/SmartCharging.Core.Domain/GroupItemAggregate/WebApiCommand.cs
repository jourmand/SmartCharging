namespace SmartCharging.Core.Domain.GroupItemAggregate;

public static class WebApiCommand
{
    public class CreateGroupCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CapacityInAmps { get; set; }
    }

    public class UpdateGroupCapacityCommand
    {
        public int NewCapacityInAmps { get; set; }
    }

    public class CreateChargeStationCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CreateConnectorCommand> Connectors { get; set; }
    }

    public class RemoveChargeStationCommand
    {
        public Guid ChargeStationId { get; set; }
    }

    public class UpdateConnectorMaxCurrentCommand
    {
        public Guid ChargeStationId { get; set; }
        public int ConnectorIdentifier { get; set; }
        public int NewMaxCurrent { get; set; }
    }

    public class CreateConnectorCommand
    {
        public int Identifier { get; set; }
        public string Name { get; set; }
        public int MaxCurrentInAmps { get; set; }
    }
}


using SmartCharging.Core.Domain.GroupItemAggregate;
using Xunit;
using static SmartCharging.Core.Domain.GroupItemAggregate.Exceptions.SmartChargingExceptions;
using static SmartCharging.Core.Domain.GroupItemAggregate.WebApiCommand;

namespace Test.SmartCharging.Domain;
public class GroupItemTests
{
    [Fact]
    public void Create_ShouldCreateGroupItemWithGivenValues()
    {
        // Arrange
        var groupCommand = new CreateGroupCommand
        {
            Id = Guid.NewGuid(),
            Name = "Group1",
            CapacityInAmps = 100
        };

        // Act
        var groupItem = GroupItem.Create(groupCommand);

        // Assert
        Assert.Equal(groupCommand.Id, groupItem.Id);
        Assert.Equal(groupCommand.Name, groupItem.Name);
        Assert.Equal(groupCommand.CapacityInAmps, groupItem.CapacityInAmps.Value);
    }

    [Fact]
    public void UpdateGroupCapacity_ShouldUpdateCapacityInAmps()
    {
        // Arrange
        var groupItem = GroupItem.Create(new CreateGroupCommand
        {
            Id = Guid.NewGuid(),
            Name = "Group1",
            CapacityInAmps = 100
        });
        var updateGroupCommand = new UpdateGroupCapacityCommand
        {
            NewCapacityInAmps = 150
        };

        // Act
        groupItem.UpdateGroupCapacity(updateGroupCommand);

        // Assert
        Assert.Equal(updateGroupCommand.NewCapacityInAmps, groupItem.CapacityInAmps.Value);
    }

    [Fact]
    public void UpdateConnectorMaxCurrent_ShouldUpdateMaxCurrentForConnector()
    {
        // Arrange
        var updatedMaxCurrent = 9;
        var existingChargeStationId = Guid.NewGuid();
        var connectorId = 1;
        var groupItem = GroupItem.Create(new CreateGroupCommand { Id = Guid.NewGuid(), Name = "Group1", CapacityInAmps = 10 });
        groupItem.AddChargeStationToGroup(new CreateChargeStationCommand
        {
            Id = existingChargeStationId,
            Name = "ChargeStation1",
            Connectors =
            [
                new CreateConnectorCommand
                {
                    Identifier = connectorId,
                    Name = "Connector1",
                    MaxCurrentInAmps = 10
                }
            ]
        });

        // Act
        groupItem.UpdateConnectorMaxCurrent(new UpdateConnectorMaxCurrentCommand
        {
            ChargeStationId = existingChargeStationId,
            ConnectorIdentifier = connectorId,
            NewMaxCurrent = updatedMaxCurrent
        });

        // Assert
        var chargeStation = groupItem.ChargeStations.First();
        var connector = chargeStation.Connectors.First(c => c.Identifier == connectorId);
        Assert.Equal(updatedMaxCurrent, connector.MaxCurrentInAmps.Value);
    }

    [Fact]
    public void UpdateConnectorMaxCurrent_ShouldThrowExeption_UpdateMaxCurrentForConnector()
    {
        // Arrange
        var updatedMaxCurrent = 20;
        var existingChargeStationId = Guid.NewGuid();
        var connectorId = 1;
        var groupItem = GroupItem.Create(new CreateGroupCommand { Id = Guid.NewGuid(), Name = "Group1", CapacityInAmps = 10 });
        groupItem.AddChargeStationToGroup(new CreateChargeStationCommand
        {
            Id = existingChargeStationId,
            Name = "ChargeStation1",
            Connectors =
            [
                new CreateConnectorCommand
                {
                    Identifier = connectorId,
                    Name = "Connector1",
                    MaxCurrentInAmps = 10
                }
            ]
        });

        // Act
        Assert.Throws<InvalidEntityState>(() => groupItem.UpdateConnectorMaxCurrent(new UpdateConnectorMaxCurrentCommand
        {
            ChargeStationId = existingChargeStationId,
            ConnectorIdentifier = connectorId,
            NewMaxCurrent = updatedMaxCurrent
        }));
    }

    [Fact]
    public void AddChargeStationToGroup_ShouldAddChargeStation()
    {
        // Arrange
        var groupItem = CreateSampleGroupItem();
        var createChargeStationCommand = new CreateChargeStationCommand
        {
            Id = Guid.NewGuid(),
            Name = "Station1",
            Connectors =
                [
                    new CreateConnectorCommand { Identifier = 1, MaxCurrentInAmps = 10 },
                    // Add more connectors as necessary
                ]
        };

        // Act
        groupItem.AddChargeStationToGroup(createChargeStationCommand);

        // Assert
        Assert.Contains(groupItem.ChargeStations, cs => cs.Id == createChargeStationCommand.Id);
    }

    [Fact]
    public void AddChargeStationToGroup_ShouldThrowExceptionWhenOverMaxConnectors()
    {
        // Arrange
        var groupItem = CreateSampleGroupItem();
        var createChargeStationCommand = new CreateChargeStationCommand
        {
            Id = Guid.NewGuid(),
            Name = "Station1",
            Connectors =
                [
                    new() { Identifier = 1, MaxCurrentInAmps = 10 },
                    new() { Identifier = 2, MaxCurrentInAmps = 10 },
                    new() { Identifier = 3, MaxCurrentInAmps = 10 },
                    new() { Identifier = 4, MaxCurrentInAmps = 10 },
                    new() { Identifier = 5, MaxCurrentInAmps = 10 },
                    new() { Identifier = 6, MaxCurrentInAmps = 10 }
                ]
        };

        // Act
        Assert.Throws<InvalidEntityState>(() => groupItem.AddChargeStationToGroup(createChargeStationCommand));
    }

    [Fact]
    public void RemoveChargeStationFromGroup_ShouldThrowExceptionIfStationDoesNotExist()
    {
        // Arrange
        var groupItem = CreateSampleGroupItem();
        var removeChargeStationCommand = new RemoveChargeStationCommand
        {
            ChargeStationId = Guid.NewGuid()
        };

        // Act
        Assert.Throws<RecordNotFound>(() => groupItem.RemoveChargeStationFromGroup(removeChargeStationCommand));
    }

    [Fact]
    public void EnsureValidState_ShouldThrowExceptionWhenCapacityIsExceeded()
    {
        // Arrange
        var groupItem = CreateSampleGroupItem();
        Assert.Throws<InvalidEntityState>(() => groupItem.AddChargeStationToGroup(new CreateChargeStationCommand()));
    }

    private static GroupItem CreateSampleGroupItem() =>
        GroupItem.Create(new CreateGroupCommand
        {
            Id = Guid.NewGuid(),
            Name = "SampleGroup",
            CapacityInAmps = 50
        });
}

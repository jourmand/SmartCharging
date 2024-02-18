using SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.AddChargeStation;
using SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.CreateGroup;
using SmartCharging.Core.Domain.GroupItemAggregate;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Test.SmartCharging.WebApi;

public class ApiControllerTests
{
    private readonly ITestOutputHelper _outputHelper;

    public ApiControllerTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task Create_NewGroup_Responds_OK()
    {
        await using var application = new PlaygroundApplication();

        var jsonString = JsonSerializer.Serialize(new CreateGroupCommand
        {
            CapacityInAmps = 10,
            Id = Guid.NewGuid(),
            Name = "Group1"
        });
        using var jsonContent = new StringContent(jsonString);
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        using var client = application.CreateClient();
        using var response = await client.PostAsync("/group", jsonContent);

        _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(10,
            JsonSerializer.Deserialize<CreateGroupDto>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.CapacityInAmps);
    }

    [Fact]
    public async Task Create_NewGroup_With_Station_ResponseOk()
    {
        await using var application = new PlaygroundApplication();
        var groupId = Guid.NewGuid();
        using var jsonContent = new StringContent(JsonSerializer.Serialize(new CreateGroupCommand
        {
            CapacityInAmps = 10,
            Id = groupId,
            Name = "Group1"
        }));
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        using var client = application.CreateClient();
        await client.PostAsync("/group", jsonContent);

        using var chargeStationJsonContent = new StringContent(JsonSerializer.Serialize(new AddChargeStationCommand
        {
            ChargeStationId = Guid.NewGuid(),
            GroupId = groupId,
            Name = "ChargeStation1",
            Connectors =
            [
                new WebApiCommand.CreateConnectorCommand
                {
                    Identifier = 1,
                    Name = "Connector1",
                    MaxCurrentInAmps = 2
                },
                new WebApiCommand.CreateConnectorCommand
                {
                    Identifier = 2,
                    Name = "Connector2",
                    MaxCurrentInAmps = 2
                },
            ]
        }));
        chargeStationJsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        using var response = await client.PostAsync("/chargestation", chargeStationJsonContent);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }
}

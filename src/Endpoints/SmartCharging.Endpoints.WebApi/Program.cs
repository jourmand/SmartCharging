using MediatR;
using SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.AddChargeStation;
using SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.CreateGroup;
using SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.RemoveChargeStation;
using SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.RemoveGroup;
using SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.UpdateConnectorMaxCurrent;
using SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.UpdateGroupCapacity;
using SmartCharging.Endpoints.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .CustomizeUseSerilog();

builder.Services
    .AddServiceRegistry()
    .ConfigApiBehavior()
    .AddCustomizedDataStore(builder.Configuration)
    .ConfigMediatR()
    .ConfigSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseCustomizedSwagger();

await app.Services.EnsureDb();
app.CustomExceptionMiddleware();
app.UseHttpsRedirection();

app.MapPost("/group", async Task<IResult> (CreateGroupCommand model, IMediator mediator) =>
{
    var result = await mediator.Send(model);
    return Results.Created($"/group/{result.Id}", result);
})
.WithName("PostGroup");

app.MapPut("/group", async Task<IResult> (UpdateGroupCapacityCommand model, IMediator mediator) =>
{
    var result = await mediator.Send(model);
    return Results.Ok(result);
})
.WithName("PutGroup");

app.MapDelete("/group/{id}", async Task<IResult> (Guid id, IMediator mediator) =>
{
    var result = await mediator.Send(new RemoveGroupCommand(id));
    return Results.Ok(result);
})
.WithName("DeleteGroup");

app.MapPost("/chargestation", async Task<IResult> (AddChargeStationCommand model, IMediator mediator) =>
{
    var result = await mediator.Send(model);
    return Results.Created($"/chargeStation/{result.ChargeStationId}", result);
})
.WithName("PostChargeStation");

app.MapDelete("/chargestation/{groupId}/{chargeStationId}", async Task<IResult> (Guid groupId, Guid chargeStationId, IMediator mediator) =>
{
    var result = await mediator.Send(new RemoveChargeStationCommand(groupId, chargeStationId));
    return Results.Ok(result);
})
.WithName("DeleteChargeStation");

app.MapPut("/connectorMaxCurrent", async Task<IResult> (UpdateConnectorMaxCurrentCommand model, IMediator mediator) =>
{
    var result = await mediator.Send(model);
    return Results.Ok(result);
})
.WithName("PutConnectorMaxCurrent");

app.Run();

public partial class Program { }
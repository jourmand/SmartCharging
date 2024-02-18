using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using SmartCharging.Core.Domain.Commons;
using static SmartCharging.Core.Domain.GroupItemAggregate.Exceptions.SmartChargingExceptions;

namespace SmartCharging.Endpoints.WebApi.Extensions.Middleware;

public static class CustomErrorHandlerHelper
{
    public static void UseCustomErrors(this IApplicationBuilder app) => app.Use(WriteResponse);

    private static async Task WriteResponse(HttpContext httpContext, Func<Task> next)
    {
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        var ex = exceptionDetails?.Error;

        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        ErrorDetails result = new(
                    400,
                    "Validation Errors",
                    httpContext.Request.Path);

        switch (ex)
        {
            case InvalidEntityState stateEntity:

                result.SetErrors(stateEntity.Errors);
                break;
            default:
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = new ErrorDetails(
                      httpContext.Response.StatusCode,
                      ex.GetBaseException().Message,
                      httpContext.Request.Path);
                break;
        }
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        }));
    }
}

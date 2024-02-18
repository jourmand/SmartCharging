using Microsoft.EntityFrameworkCore;
using SmartCharging.Endpoints.WebApi.Extensions.Middleware;
using SmartCharging.Infrastructures.Data.Commons;

namespace SmartCharging.Endpoints.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void CustomExceptionMiddleware(this IApplicationBuilder app) =>
            app.UseExceptionHandler(err => err.UseCustomErrors());

    public static IApplicationBuilder UseCustomizedSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger()
            .UseSwaggerUI();
        return app;
    }

    public static async Task EnsureDb(this IServiceProvider service)
    {
        using var db = service.CreateScope().ServiceProvider.GetRequiredService<SmartChargingDbContext>();
        if (db.Database.IsRelational())
        {
            await db.Database.MigrateAsync();
        }
    }
}

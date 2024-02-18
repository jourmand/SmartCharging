using Serilog;

namespace SmartCharging.Endpoints.WebApi.Extensions;

public static class HostBuilderExtensions
{
    public static ConfigureHostBuilder CustomizeUseSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", "SmartCharging")
                .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);
        });
        return host;
    }
}

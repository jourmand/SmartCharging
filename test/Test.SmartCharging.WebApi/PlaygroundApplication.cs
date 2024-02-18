﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartCharging.Infrastructures.Data.Commons;

namespace Test.SmartCharging.WebApi;

internal class PlaygroundApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public PlaygroundApplication(string environment = "Development")
    {
        _environment = environment;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);

        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp =>
            {
                return new DbContextOptionsBuilder<SmartChargingDbContext>()
                .UseInMemoryDatabase("SmartChargingTest")
                .UseApplicationServiceProvider(sp)
                .Options;
            });
        });

        return base.CreateHost(builder);
    }
}

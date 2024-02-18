using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartCharging.Core.ApplicationService.Commands.GroupItemAggregate.CreateGroup;
using SmartCharging.Core.Domain.Commons;
using SmartCharging.Core.Domain.GroupItemAggregate.Contracts;
using SmartCharging.Infrastructures.Data.Commons;
using SmartCharging.Infrastructures.Data.GroupItemAggregate.Repositories;
using System.Reflection;

namespace SmartCharging.Endpoints.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceRegistry(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGroupItemRepository, GroupItemRepository>();

        return services;
    }

    public static IServiceCollection ConfigMediatR(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.Load(typeof(CreateGroupCommand).GetTypeInfo().Assembly.GetName().Name),
                Assembly.Load(typeof(SmartChargingDbContext).GetTypeInfo().Assembly.GetName().Name)));

        return services;
    }

    public static IServiceCollection ConfigApiBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
                                new BadRequestObjectResult(actionContext.ModelState);
        });
        return services;
    }

    public static IServiceCollection ConfigSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Smart Charging HTTP API",
                Version = "v1.0",
                Description = "The Smart Charging Service HTTP API",
            });

        });
        return services;
    }

    public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SmartChargingDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DatabaseConnection"),
            b => b.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name)));
        return services;
    }
}

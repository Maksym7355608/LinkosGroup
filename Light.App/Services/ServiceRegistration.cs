using Light.App.Handlers;
using Light.App.Handlers.Abstract;
using Light.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Light.App.Services;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterHandlers(this IServiceCollection service)
    {
        service.AddScoped<IImporter, ImportHandler>();
        service.AddScoped<IExporter, ExportHandler>();
        service.AddScoped<ILightHandler, LightHandler>();
        service.AddScoped<IAddressHandler, AddressHandler>();
        
        return service;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<SqlContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("Postgres")));
        return service;
    }
}
using WikiAPI.Application.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WikiAPI.Infrastructure.FileImport;

namespace WikiAPI.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IJsonImporter, JsonImporter>();
        
        return services;
    }
}

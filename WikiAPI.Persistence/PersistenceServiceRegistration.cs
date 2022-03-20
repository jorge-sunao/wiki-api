using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WikiAPI.Persistence.Connections;
using Dapper.Contrib.Extensions;

namespace WikiAPI.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WikiAPIDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<WikiAPIDbContext>());
        services.AddScoped<IApplicationWriteDbConnection, WikiAPIWriteDbConnection>();
        services.AddScoped<IApplicationReadDbConnection, WikiAPIReadDbConnection>();

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ISourceRepository, SourceRepository>();

        return services;    
    }
}

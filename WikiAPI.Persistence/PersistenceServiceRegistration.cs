using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GloboTicket.TicketManagement.Persistence;

namespace WikiAPI.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WikiAPIDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ISourceRepository, SourceRepository>();

            return services;    
        }
    }
}

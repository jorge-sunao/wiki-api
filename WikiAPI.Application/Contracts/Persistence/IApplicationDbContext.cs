using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Application.Contracts.Persistence
{
    public interface IApplicationDbContext
    {
        public IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Source> Sources { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

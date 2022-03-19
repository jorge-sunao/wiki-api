using WikiAPI.Application.Contracts;
using WikiAPI.Domain.Common;
using WikiAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using WikiAPI.Application.Contracts.Persistence;

namespace GloboTicket.TicketManagement.Persistence
{
    public class WikiAPIDbContext: DbContext, IApplicationDbContext
    {

        public WikiAPIDbContext(DbContextOptions<WikiAPIDbContext> options)
           : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Source> Sources { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WikiAPIDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

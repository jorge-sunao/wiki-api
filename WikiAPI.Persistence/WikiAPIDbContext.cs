using WikiAPI.Application.Contracts;
using WikiAPI.Domain.Common;
using WikiAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using WikiAPI.Application.Contracts.Persistence;

namespace WikiAPI.Persistence;

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
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WikiAPIDbContext).Assembly);
    }
}

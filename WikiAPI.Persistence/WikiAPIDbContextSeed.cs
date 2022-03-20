using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WikiAPI.Domain.Entities;
using Microsoft.Extensions.Logging;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Contracts.Infrastructure;

namespace WikiAPI.Persistence;

public class WikiAPIDbContextSeed
{
    public static async Task SeedAsync(IApplicationDbContext wikiApiDbContext,
        IJsonImporter jsonImporter,
        IAsyncRepository<Article> articleRepository,
        IAsyncRepository<Source> sourceRepository)
    {        
        try
        {
            if (wikiApiDbContext.Database.IsSqlServer())
            {
                wikiApiDbContext.Database.Migrate();
            }

            if (!(await articleRepository.ListAllAsync()).Any())
            {
                await articleRepository.AddRangeAsync(await 
                    GetPreconfiguredArticlesAsync(jsonImporter));
            }

            if (!(await sourceRepository.ListAllAsync()).Any())
            {
                await sourceRepository.AddRangeAsync(await
                    GetPreconfiguredSourcesAsync(jsonImporter));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    static async Task<IEnumerable<Article>> GetPreconfiguredArticlesAsync(IJsonImporter jsonImporter)
    {
        var filePath = Path.Combine("Data", "Articles.json");
        var articles = await jsonImporter.ConvertJsonToEntityAsync<Article>(filePath);
        return articles;
    }

    static async Task<IEnumerable<Source>> GetPreconfiguredSourcesAsync(IJsonImporter jsonImporter)
    {
        var filePath = Path.Combine("Data", "Sources.json");
        var sources = await jsonImporter.ConvertJsonToEntityAsync<Source>(filePath);
        return sources;
    }
}

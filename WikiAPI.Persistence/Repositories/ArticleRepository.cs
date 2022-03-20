using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Persistence.Repositories;

public class ArticleRepository : BaseRepository<Article>, IArticleRepository
{
    private IApplicationReadDbConnection _readDbConnection;
    private IApplicationWriteDbConnection _writeDbConnection;

    public ArticleRepository(IApplicationDbContext dbContext, IApplicationReadDbConnection readDbConnection, IApplicationWriteDbConnection writeDbConnection) : base(dbContext)
    {        
        _readDbConnection = readDbConnection;
        _writeDbConnection = writeDbConnection;
    }

    public async Task<Article> GetArticleWithSources(int articleId)
    {
        var query = "select * from Articles left outer join Sources on(Sources.ArticleId = Articles.Id) where Articles.Id = @articleId";

        var articleDic = new Dictionary<int, Article>();

        var articles = await _readDbConnection.QueryAsync<Article, Source, Article>(query, (article, source) =>
        {
            if (!articleDic.TryGetValue(article.Id, out var currentArticle))
            {
                currentArticle = article;

                if (currentArticle.Sources == null)
                    currentArticle.Sources = new List<Source>();
                
                articleDic.Add(currentArticle.Id, currentArticle);
            }

            if (source != null)
                currentArticle.Sources.Add(source);

            return currentArticle;
        }, new { articleId });
        return articles.Distinct().FirstOrDefault();
    }

    public async Task<bool> IsArticleTitleAndAuthorUnique(string title, string author, int? excludeId)
    {
        var query = $"select count(*) from Articles where Title = @title and Author = @author";

        if (excludeId != null && excludeId > 0)
            query += $" and Id <> {excludeId}";

        var resultCount = await _readDbConnection.QueryFirstOrDefaultAsync<int>(query, new { title, author });

        return resultCount == 0;
    }

    public async Task<bool> IsSlugUnique(string slug, int? excludeId)
    {
        var query = $"select count(*) from Articles where Slug = @slug";

        if (excludeId != null && excludeId > 0)
            query += $" and Id <> {excludeId}";

        var resultCount = await _readDbConnection.QueryFirstOrDefaultAsync<int>(query, new { slug });

        return resultCount == 0;
    }
}

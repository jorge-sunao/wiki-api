using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Persistence.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        private IApplicationDbContext _dbContext;
        private IApplicationReadDbConnection _readDbConnection;
        private IApplicationWriteDbConnection _writeDbConnection;

        public ArticleRepository(IApplicationDbContext dbContext, IApplicationReadDbConnection readDbConnection, IApplicationWriteDbConnection writeDbConnection) : base(dbContext)
        {
            _dbContext = dbContext;
            _readDbConnection = readDbConnection;
            _writeDbConnection = writeDbConnection;
        }

        public Task<List<Article>> GetArticlesWithSources(int articleId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsArticleTitleAndAuthorUnique(string title, string author, int? excludeId)
        {
            var query = $"select count(*) from Article where Title = @title and Author = @author";

            if (excludeId != null && excludeId > 0)
                query += $" and Id <> {excludeId}";

            var resultCount = await _readDbConnection.QueryFirstOrDefaultAsync<int>(query, new { title, author });

            return resultCount == 0;
        }

        public async Task<bool> IsSlugUnique(string slug, int? excludeId)
        {
            var query = $"select count(*) from Article where Slug = @slug";

            if (excludeId != null && excludeId > 0)
                query += $" and Id <> {excludeId}";

            var resultCount = await _readDbConnection.QueryFirstOrDefaultAsync<int>(query, new { slug });

            return resultCount == 0;
        }
    }
}

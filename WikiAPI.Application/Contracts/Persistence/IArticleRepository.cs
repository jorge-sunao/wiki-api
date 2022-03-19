using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Application.Contracts.Persistence
{
    public interface IArticleRepository:IAsyncRepository<Article>
    {
        Task<List<Article>> GetArticlesWithSources(Guid articleId);
        Task<bool> IsArticleTitleAndAuthorUnique(string title, string author, Guid? excludeId);
        Task<bool> IsSlugUnique(string slug, Guid? excludeId);
    }
}

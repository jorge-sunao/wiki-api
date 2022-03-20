using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Responses;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticlesList;

public class GetArticlesListQueryResponse: BaseResponse
{
    public GetArticlesListQueryResponse() : base()
    {
    }

    public List<ArticleListViewModel> Articles { get; set; }
}

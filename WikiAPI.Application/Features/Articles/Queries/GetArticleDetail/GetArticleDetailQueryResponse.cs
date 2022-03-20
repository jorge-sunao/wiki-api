using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Responses;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticleDetail;

public class GetArticleDetailQueryResponse: BaseResponse
{
    public GetArticleDetailQueryResponse() : base()
    {
    }

    public ArticleDetailViewModel Article { get; set; }
}

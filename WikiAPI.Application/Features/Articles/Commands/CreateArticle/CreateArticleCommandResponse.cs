using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Responses;

namespace WikiAPI.Application.Features.Articles.Commands.CreateArticle;

public class CreateArticleCommandResponse: BaseResponse
{
    public CreateArticleCommandResponse() : base()
    {

    }

    public ArticleDto Article { get; set; }
}

using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Responses;

namespace WikiAPI.Application.Features.Articles.Commands.UpdateArticle;

public class UpdateArticleCommandResponse: BaseResponse
{
    public UpdateArticleCommandResponse() : base()
    {

    }

    public ArticleDto Article { get; set; }
}

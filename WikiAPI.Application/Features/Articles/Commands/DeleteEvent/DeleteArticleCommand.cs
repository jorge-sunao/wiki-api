using MediatR;
using System;

namespace WikiAPI.Application.Features.Articles.Commands.DeleteArticle;

public class DeleteArticleCommand: IRequest<DeleteArticleCommandResponse>
{
    public int Id { get; set; }
}

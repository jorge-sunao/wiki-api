using MediatR;
using System;

namespace WikiAPI.Application.Features.Articles.Commands.UpdateArticle;

public class UpdateArticleCommand: IRequest<UpdateArticleCommandResponse>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public DateTime DatePublished { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

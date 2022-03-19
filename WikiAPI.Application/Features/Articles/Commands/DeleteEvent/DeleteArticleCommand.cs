using MediatR;
using System;

namespace WikiAPI.Application.Features.Articles.Commands.DeleteArticle
{
    public class DeleteArticleCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}

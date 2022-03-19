using MediatR;
using System;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailQuery: IRequest<ArticleDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}

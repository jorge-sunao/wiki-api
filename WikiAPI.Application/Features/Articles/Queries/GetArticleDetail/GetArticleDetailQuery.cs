using MediatR;
using System;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticleDetail;

public class GetArticleDetailQuery: IRequest<GetArticleDetailQueryResponse>
{
    public int Id { get; set; }
}

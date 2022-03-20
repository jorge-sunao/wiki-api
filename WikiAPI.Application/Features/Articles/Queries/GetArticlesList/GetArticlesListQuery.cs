using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Application.Features.Articles;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticlesList;

public class GetArticlesListQuery : IRequest<GetArticlesListQueryResponse>
{
}

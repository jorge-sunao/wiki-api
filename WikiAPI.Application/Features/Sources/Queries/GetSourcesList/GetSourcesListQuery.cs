using MediatR;
using System.Collections.Generic;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourcesList
{
    public class GetSourcesListQuery : IRequest<List<SourceListViewModel>>
    {
    }
}

using WikiAPI.Application.Responses;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourcesList;

public class GetSourcesListQueryResponse: BaseResponse
{
    public GetSourcesListQueryResponse() : base()
    {
    }

    public List<SourceListViewModel> Sources { get; set; }
}

using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Responses;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourceDetail;

public class GetSourceDetailQueryResponse: BaseResponse
{
    public GetSourceDetailQueryResponse() : base()
    {
    }

    public SourceDetailViewModel Source { get; set; }
}

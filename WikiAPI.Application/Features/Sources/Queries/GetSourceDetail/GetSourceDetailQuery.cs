using MediatR;
using System;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourceDetail;

public class GetSourceDetailQuery: IRequest<GetSourceDetailQueryResponse>
{
    public int Id { get; set; }
}

using MediatR;
using System;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourceDetail
{
    public class GetSourceDetailQuery: IRequest<SourceDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}

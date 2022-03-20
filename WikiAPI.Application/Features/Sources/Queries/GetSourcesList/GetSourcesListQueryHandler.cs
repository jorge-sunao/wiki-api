using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourcesList;

public class GetSourcesListQueryHandler : IRequestHandler<GetSourcesListQuery, GetSourcesListQueryResponse>
{
    private readonly IAsyncRepository<Source> _sourceRepository;
    private readonly IMapper _mapper;

    public GetSourcesListQueryHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository)
    {
        _mapper = mapper;
        _sourceRepository = sourceRepository;
    }

    public async Task<GetSourcesListQueryResponse> Handle(GetSourcesListQuery request, CancellationToken cancellationToken)
    {
        var getSourcesListResponse = new GetSourcesListQueryResponse();

        try
        {
            var allSources = (await _sourceRepository.ListAllAsync()).OrderBy(x => x.Author);
            getSourcesListResponse.Sources = _mapper.Map<List<SourceListViewModel>>(allSources);

            return getSourcesListResponse;
        }
        catch (Exception ex)
        {
            getSourcesListResponse.Success = false;
            getSourcesListResponse.ValidationErrors = new List<string>();
            getSourcesListResponse.ValidationErrors.Add(ex.Message);

            return getSourcesListResponse;
        }
    }
}

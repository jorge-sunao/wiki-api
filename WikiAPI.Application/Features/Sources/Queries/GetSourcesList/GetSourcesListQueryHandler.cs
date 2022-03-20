using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourcesList;

public class GetSourcesListQueryHandler : IRequestHandler<GetSourcesListQuery, GetSourcesListQueryResponse>
{
    private readonly IAsyncRepository<Source> _sourceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSourcesListQueryHandler> _logger;

    public GetSourcesListQueryHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository, ILogger<GetSourcesListQueryHandler> logger)
    {
        _mapper = mapper;
        _sourceRepository = sourceRepository;
        _logger = logger;
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
            _logger.LogError($"Getting list of sources failed due to an error: {ex.Message}");

            throw;
        }
    }
}

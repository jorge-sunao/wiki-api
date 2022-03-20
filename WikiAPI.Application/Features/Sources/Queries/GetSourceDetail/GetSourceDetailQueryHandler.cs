using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourceDetail;

public class GetSourceDetailQueryHandler : IRequestHandler<GetSourceDetailQuery, GetSourceDetailQueryResponse>
{
    private readonly IAsyncRepository<Source> _sourceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSourceDetailQueryHandler> _logger;

    public GetSourceDetailQueryHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository, ILogger<GetSourceDetailQueryHandler> logger)
    {
        _mapper = mapper;
        _sourceRepository = sourceRepository;
        _logger = logger;
    }

    public async Task<GetSourceDetailQueryResponse> Handle(GetSourceDetailQuery request, CancellationToken cancellationToken)
    {
        var getSourceResponse = new GetSourceDetailQueryResponse();

        try
        {
            var source = await _sourceRepository.GetByIdAsync(request.Id);
            var sourceDetailDto = _mapper.Map<SourceDetailViewModel>(source);

            getSourceResponse.Source = sourceDetailDto;

            return getSourceResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Getting source detail '{request.Id}' failed due to an error: {ex.Message}");

            throw;
        }
    }
}

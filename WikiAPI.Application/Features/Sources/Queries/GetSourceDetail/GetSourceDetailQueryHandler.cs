using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourceDetail;

public class GetSourceDetailQueryHandler : IRequestHandler<GetSourceDetailQuery, GetSourceDetailQueryResponse>
{
    private readonly IAsyncRepository<Source> _sourceRepository;
    private readonly IMapper _mapper;

    public GetSourceDetailQueryHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository)
    {
        _mapper = mapper;
        _sourceRepository = sourceRepository;
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
            getSourceResponse.Success = false;
            getSourceResponse.ValidationErrors = new List<string>();
            getSourceResponse.ValidationErrors.Add(ex.Message);

            return getSourceResponse;
        }
    }
}

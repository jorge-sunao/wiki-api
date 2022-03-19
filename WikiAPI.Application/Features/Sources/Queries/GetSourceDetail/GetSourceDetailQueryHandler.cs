using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourceDetail
{
    public class GetSourceDetailQueryHandler : IRequestHandler<GetSourceDetailQuery, SourceDetailViewModel>
    {
        private readonly IAsyncRepository<Source> _sourceRepository;
        private readonly IMapper _mapper;

        public GetSourceDetailQueryHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository)
        {
            _mapper = mapper;
            _sourceRepository = sourceRepository;
        }

        public async Task<SourceDetailViewModel> Handle(GetSourceDetailQuery request, CancellationToken cancellationToken)
        {
            var source = await _sourceRepository.GetByIdAsync(request.Id);
            var sourceDetailDto = _mapper.Map<SourceDetailViewModel>(source);

            return sourceDetailDto;
        }
    }
}

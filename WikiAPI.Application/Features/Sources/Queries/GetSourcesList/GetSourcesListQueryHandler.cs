using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Sources.Queries.GetSourcesList
{
    public class GetSourcesListQueryHandler : IRequestHandler<GetSourcesListQuery, List<SourceListViewModel>>
    {
        private readonly IAsyncRepository<Source> _sourceRepository;
        private readonly IMapper _mapper;

        public GetSourcesListQueryHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository)
        {
            _mapper = mapper;
            _sourceRepository = sourceRepository;
        }

        public async Task<List<SourceListViewModel>> Handle(GetSourcesListQuery request, CancellationToken cancellationToken)
        {
            var allSources = (await _sourceRepository.ListAllAsync()).OrderBy(x => x.Author);
            return _mapper.Map<List<SourceListViewModel>>(allSources);
        }
    }
}

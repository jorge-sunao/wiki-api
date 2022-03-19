using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailQueryHandler : IRequestHandler<GetArticleDetailQuery, ArticleDetailViewModel>
    {
        private readonly IAsyncRepository<Article> _articleRepository;
        private readonly IAsyncRepository<Source> _sourceRepository;
        private readonly IMapper _mapper;

        public GetArticleDetailQueryHandler(IMapper mapper, IAsyncRepository<Article> articleRepository, IAsyncRepository<Source> sourceRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _sourceRepository = sourceRepository;
        }

        public async Task<ArticleDetailViewModel> Handle(GetArticleDetailQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(request.Id);
            var articleDetailDto = _mapper.Map<ArticleDetailViewModel>(article);

            if (article == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }

            articleDetailDto.Sources = _mapper.Map<List<SourceDto>>(article.Sources);

            return articleDetailDto;
        }
    }
}

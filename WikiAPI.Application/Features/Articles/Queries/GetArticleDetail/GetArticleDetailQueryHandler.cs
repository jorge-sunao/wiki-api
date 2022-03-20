using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Common.Dtos;
using Microsoft.Extensions.Logging;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticleDetail;

public class GetArticleDetailQueryHandler : IRequestHandler<GetArticleDetailQuery, GetArticleDetailQueryResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetArticleDetailQueryHandler> _logger;

    public GetArticleDetailQueryHandler(IMapper mapper, IArticleRepository articleRepository, ILogger<GetArticleDetailQueryHandler> logger)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
        _logger = logger;
    }

    public async Task<GetArticleDetailQueryResponse> Handle(GetArticleDetailQuery request, CancellationToken cancellationToken)
    {
        var getArticleResponse = new GetArticleDetailQueryResponse();

        try
        {
            var article = await _articleRepository.GetArticleWithSources(request.Id);
            var articleDetailDto = _mapper.Map<ArticleDetailViewModel>(article);

            if (article == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }

            articleDetailDto.Sources = _mapper.Map<List<SourceDto>?>(article.Sources);
            getArticleResponse.Article = articleDetailDto;

            return getArticleResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Getting article detail '{request.Id}' failed due to an error: {ex.Message}");

            throw;
        }
    }
}

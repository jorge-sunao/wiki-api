using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticlesList;

public class GetArticlesListQueryHandler : IRequestHandler<GetArticlesListQuery, GetArticlesListQueryResponse>
{
    private readonly IAsyncRepository<Article> _articleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetArticlesListQueryHandler> _logger;

    public GetArticlesListQueryHandler(IMapper mapper, IAsyncRepository<Article> articleRepository, ILogger<GetArticlesListQueryHandler> logger)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
        _logger = logger;
    }

    public async Task<GetArticlesListQueryResponse> Handle(GetArticlesListQuery request, CancellationToken cancellationToken)
    {
        var getArticlesListResponse = new GetArticlesListQueryResponse();

        try
        {
            var allArticles = (await _articleRepository.ListAllAsync()).OrderByDescending(a => a.DatePublished);
            getArticlesListResponse.Articles = _mapper.Map<List<ArticleListViewModel>>(allArticles);

            return getArticlesListResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Getting list of articles failed due to an error: {ex.Message}");

            throw;
        }
    }
}

using AutoMapper;
using MediatR;
using System;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticlesList;

public class GetArticlesListQueryHandler : IRequestHandler<GetArticlesListQuery, GetArticlesListQueryResponse>
{
    private readonly IAsyncRepository<Article> _articleRepository;
    private readonly IMapper _mapper;

    public GetArticlesListQueryHandler(IMapper mapper, IAsyncRepository<Article> articleRepository)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
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
            getArticlesListResponse.Success = false;
            getArticlesListResponse.ValidationErrors = new List<string>();
            getArticlesListResponse.ValidationErrors.Add(ex.Message);

            return getArticlesListResponse;
        }
    }
}

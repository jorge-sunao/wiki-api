using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticlesList
{
    public class GetArticlesListQueryHandler : IRequestHandler<GetArticlesListQuery, List<ArticleListViewModel>>
    {
        private readonly IAsyncRepository<Article> _articleRepository;
        private readonly IMapper _mapper;

        public GetArticlesListQueryHandler(IMapper mapper, IAsyncRepository<Article> articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public async Task<List<ArticleListViewModel>> Handle(GetArticlesListQuery request, CancellationToken cancellationToken)
        {
            var allArticles = (await _articleRepository.ListAllAsync()).OrderByDescending(a => a.DatePublished);
            return _mapper.Map<List<ArticleListViewModel>>(allArticles);
        }
    }
}

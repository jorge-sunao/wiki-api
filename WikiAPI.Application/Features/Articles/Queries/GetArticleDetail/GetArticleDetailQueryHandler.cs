using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Common.Dtos;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticleDetail;

public class GetArticleDetailQueryHandler : IRequestHandler<GetArticleDetailQuery, GetArticleDetailQueryResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleDetailQueryHandler(IMapper mapper, IArticleRepository articleRepository)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
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
            getArticleResponse.Success = false;
            getArticleResponse.ValidationErrors = new List<string>();
            getArticleResponse.ValidationErrors.Add(ex.Message);

            return getArticleResponse;
        }
    }
}

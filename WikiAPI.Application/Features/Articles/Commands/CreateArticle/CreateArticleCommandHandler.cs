using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace WikiAPI.Application.Features.Articles.Commands.CreateArticle;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreateArticleCommandResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateArticleCommandHandler> _logger;

    public CreateArticleCommandHandler(IMapper mapper, IArticleRepository articleRepository, ILogger<CreateArticleCommandHandler> logger)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
        _logger = logger;
    }

    public async Task<CreateArticleCommandResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var createArticleResponse = new CreateArticleCommandResponse();

        try
        {
            var validator = new CreateArticleCommandValidator(_articleRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var article = _mapper.Map<Article>(request);

            article.DatePublished = DateTime.UtcNow;

            var articleId = await _articleRepository.AddAsync(article);

            article.Id = articleId;

            createArticleResponse.Article = _mapper.Map<ArticleDto>(article);

            return createArticleResponse;
        }
        catch (ValidationException ex)
        {
            createArticleResponse.Success = false;
            createArticleResponse.ValidationErrors = new List<string>();
            createArticleResponse.ValidationErrors.AddRange(ex.ValidationErrors);

            return createArticleResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Creating article {request.Title} by {request.Author} failed due to an error: {ex.Message}");

            throw;
        }
    }
}

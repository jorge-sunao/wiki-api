using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Common.Dtos;
using Microsoft.Extensions.Logging;

namespace WikiAPI.Application.Features.Articles.Commands.UpdateArticle;

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, UpdateArticleCommandResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateArticleCommandHandler> _logger;

    public UpdateArticleCommandHandler(IMapper mapper, IArticleRepository articleRepository, ILogger<UpdateArticleCommandHandler> logger)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
        _logger = logger;
    }

    public async Task<UpdateArticleCommandResponse> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var updateArticleResponse = new UpdateArticleCommandResponse();

        try
        {
            var articleToUpdate = await _articleRepository.GetByIdAsync(request.Id);

            if (articleToUpdate == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }

            var validator = new UpdateArticleCommandValidator(_articleRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, articleToUpdate, typeof(UpdateArticleCommand), typeof(Article));

            articleToUpdate.Version += 1;

            await _articleRepository.UpdateAsync(articleToUpdate);

            updateArticleResponse.Article = _mapper.Map<ArticleDto>(articleToUpdate);

            return updateArticleResponse;
        }
        catch (ValidationException ex)
        {
            updateArticleResponse.Success = false;
            updateArticleResponse.ValidationErrors = new List<string>();
            updateArticleResponse.ValidationErrors.AddRange(ex.ValidationErrors);

            return updateArticleResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Updating article '{request.Id}' failed due to an error: {ex.Message}");

            throw;
        }
    }
}

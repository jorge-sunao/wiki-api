using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WikiAPI.Application.Features.Articles.Commands.DeleteArticle;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, DeleteArticleCommandResponse>
{
    private readonly IAsyncRepository<Article> _articleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteArticleCommandHandler> _logger;

    public DeleteArticleCommandHandler(IMapper mapper, IAsyncRepository<Article> articleRepository, ILogger<DeleteArticleCommandHandler> logger)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
        _logger = logger;
    }

    public async Task<DeleteArticleCommandResponse> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var deleteArticleResponse = new DeleteArticleCommandResponse();

        try
        {
            var eventToDelete = await _articleRepository.GetByIdAsync(request.Id);

            if (eventToDelete == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }

            await _articleRepository.DeleteAsync(eventToDelete);

            return deleteArticleResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Deleting article '{request.Id}' failed due to an error: {ex.Message}");

            throw;
        }
    }
}

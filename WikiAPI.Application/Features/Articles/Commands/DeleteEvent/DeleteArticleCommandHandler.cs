using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Articles.Commands.DeleteArticle;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, DeleteArticleCommandResponse>
{
    private readonly IAsyncRepository<Article> _articleRepository;
    private readonly IMapper _mapper;
    
    public DeleteArticleCommandHandler(IMapper mapper, IAsyncRepository<Article> articleRepository)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
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
            deleteArticleResponse.Success = false;
            deleteArticleResponse.ValidationErrors = new List<string>();
            deleteArticleResponse.ValidationErrors.Add(ex.Message);

            return deleteArticleResponse;
        }
    }
}

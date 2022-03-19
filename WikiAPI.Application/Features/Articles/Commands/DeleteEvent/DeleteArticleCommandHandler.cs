using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Articles.Commands.DeleteArticle
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
    {
        private readonly IAsyncRepository<Article> _articleRepository;
        private readonly IMapper _mapper;
        
        public DeleteArticleCommandHandler(IMapper mapper, IAsyncRepository<Article> articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _articleRepository.GetByIdAsync(request.Id);

            if (eventToDelete == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }

            await _articleRepository.DeleteAsync(eventToDelete);

            return Unit.Value;
        }
    }
}

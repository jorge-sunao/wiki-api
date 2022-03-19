using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public UpdateArticleCommandHandler(IMapper mapper, IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
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

            await _articleRepository.UpdateAsync(articleToUpdate);

            return Unit.Value;
        }
    }
}
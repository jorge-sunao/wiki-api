using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Articles.Commands.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, int>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;


        public CreateArticleCommandHandler(IMapper mapper, IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateArticleCommandValidator(_articleRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var article = _mapper.Map<Article>(request);

            var articleId = await _articleRepository.AddAsync(article);

            return articleId;
        }
    }
}
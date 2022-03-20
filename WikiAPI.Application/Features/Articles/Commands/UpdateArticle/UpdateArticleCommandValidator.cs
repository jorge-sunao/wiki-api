using FluentValidation;
using WikiAPI.Application.Contracts.Persistence;

namespace WikiAPI.Application.Features.Articles.Commands.UpdateArticle;

public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    private readonly IArticleRepository _articleRepository;
    public UpdateArticleCommandValidator(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;

        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Author)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.Slug)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(e => e)
           .MustAsync(ArticleTitleAndAuthorUnique)
           .WithMessage("An event with the same name and date already exists.");

        RuleFor(e => e)
           .MustAsync(SlugUnique)
           .WithMessage("A slug with the same value already exists.");
    }

    private async Task<bool> ArticleTitleAndAuthorUnique(UpdateArticleCommand e, CancellationToken token)
    {
        return await _articleRepository.IsArticleTitleAndAuthorUnique(e.Title, e.Author, e.Id);
    }

    private async Task<bool> SlugUnique(UpdateArticleCommand e, CancellationToken token)
    {
        return await _articleRepository.IsSlugUnique(e.Slug, e.Id);
    }
}

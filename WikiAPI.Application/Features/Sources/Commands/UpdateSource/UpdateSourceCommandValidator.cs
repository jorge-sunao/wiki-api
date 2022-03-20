using FluentValidation;
using WikiAPI.Application.Contracts.Persistence;

namespace WikiAPI.Application.Features.Sources.Commands.UpdateSource;

public class UpdateSourceCommandValidator : AbstractValidator<UpdateSourceCommand>
{
    public UpdateSourceCommandValidator()
    {
        RuleFor(s => s.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.")
            .NotNull();

        RuleFor(s => s.Author)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(s => s.ArticleId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}

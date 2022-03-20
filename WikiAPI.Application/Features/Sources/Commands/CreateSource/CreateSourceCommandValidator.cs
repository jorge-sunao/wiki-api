using FluentValidation;
using WikiAPI.Application.Contracts.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Sources.Commands.CreateSource;

public class CreateSourceCommandValidator : AbstractValidator<CreateSourceCommand>
{
    public CreateSourceCommandValidator()
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

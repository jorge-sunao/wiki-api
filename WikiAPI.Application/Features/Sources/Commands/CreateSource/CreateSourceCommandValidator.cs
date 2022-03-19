using FluentValidation;
using WikiAPI.Application.Contracts.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Sources.Commands.CreateSource
{
    public class CreateSourceCommandValidator : AbstractValidator<CreateSourceCommand>
    {
        public CreateSourceCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Author)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}

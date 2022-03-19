using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Sources.Commands.CreateSource
{
    public class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, CreateSourceCommandResponse>
    {
        private readonly IAsyncRepository<Source> _sourceRepository;
        private readonly IMapper _mapper;


        public CreateSourceCommandHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository)
        {
            _mapper = mapper;
            _sourceRepository = sourceRepository;
        }

        public async Task<CreateSourceCommandResponse> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
        {
            var createSourceCommandResponse = new CreateSourceCommandResponse();

            var validator = new CreateSourceCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createSourceCommandResponse.Success = false;
                createSourceCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createSourceCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createSourceCommandResponse.Success)
            {
                var source = _mapper.Map<Source>(request);
                source = await _sourceRepository.AddAsync(source);

                createSourceCommandResponse.Source = _mapper.Map<CreateSourceDto>(source);
            }

            return createSourceCommandResponse;
        }
    }
}
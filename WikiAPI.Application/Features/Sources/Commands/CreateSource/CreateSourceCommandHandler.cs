using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Exceptions;

namespace WikiAPI.Application.Features.Sources.Commands.CreateSource;

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

        try
        {
            var validator = new CreateSourceCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var source = _mapper.Map<Source>(request);
            var sourceId = await _sourceRepository.AddAsync(source);
            source.Id = sourceId;

            createSourceCommandResponse.Source = _mapper.Map<SourceDto>(source);

            return createSourceCommandResponse;
        }
        catch (ValidationException ex)
        {
            createSourceCommandResponse.Success = false;
            createSourceCommandResponse.ValidationErrors = new List<string>();
            createSourceCommandResponse.ValidationErrors.AddRange(ex.ValidationErrors);

            return createSourceCommandResponse;
        }
        catch (Exception ex)
        {
            createSourceCommandResponse.Success = false;
            createSourceCommandResponse.ValidationErrors = new List<string>();
            createSourceCommandResponse.ValidationErrors.Add(ex.Message);

            return createSourceCommandResponse;
        }
    }
}

using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace WikiAPI.Application.Features.Sources.Commands.CreateSource;

public class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, CreateSourceCommandResponse>
{
    private readonly IAsyncRepository<Source> _sourceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSourceCommandHandler> _logger;

    public CreateSourceCommandHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository, ILogger<CreateSourceCommandHandler> logger)
    {
        _mapper = mapper;
        _sourceRepository = sourceRepository;
        _logger = logger;
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
            _logger.LogError($"Creating source {request.Title} by {request.Author} failed due to an error: {ex.Message}");
            throw;
        }
    }
}

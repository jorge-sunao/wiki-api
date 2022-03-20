﻿using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Common.Dtos;

namespace WikiAPI.Application.Features.Sources.Commands.UpdateSource;

public class UpdateSourceCommandHandler : IRequestHandler<UpdateSourceCommand, UpdateSourceCommandResponse>
{
    private readonly ISourceRepository _sourceRepository;
    private readonly IMapper _mapper;

    public UpdateSourceCommandHandler(IMapper mapper, ISourceRepository sourceRepository)
    {
        _mapper = mapper;
        _sourceRepository = sourceRepository;
    }

    public async Task<UpdateSourceCommandResponse> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
    {
        var updateSourceResponse = new UpdateSourceCommandResponse();

        try
        {
            var sourceToUpdate = await _sourceRepository.GetByIdAsync(request.Id);

            if (sourceToUpdate == null)
            {
                throw new NotFoundException(nameof(Source), request.Id);
            }

            var validator = new UpdateSourceCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, sourceToUpdate, typeof(UpdateSourceCommand), typeof(Source));

            await _sourceRepository.UpdateAsync(sourceToUpdate);

            updateSourceResponse.Source = _mapper.Map<SourceDto>(sourceToUpdate);

            return updateSourceResponse;
        }
        catch (ValidationException ex)
        {
            updateSourceResponse.Success = false;
            updateSourceResponse.ValidationErrors = new List<string>();
            updateSourceResponse.ValidationErrors.AddRange(ex.ValidationErrors);

            return updateSourceResponse;
        }
        catch (Exception ex)
        {
            updateSourceResponse.Success = false;
            updateSourceResponse.ValidationErrors = new List<string>();
            updateSourceResponse.ValidationErrors.Add(ex.Message);

            return updateSourceResponse;
        }
    }
}

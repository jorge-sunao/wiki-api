using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WikiAPI.Application.Features.Sources.Commands.DeleteSource;

public class DeleteSourceCommandHandler : IRequestHandler<DeleteSourceCommand, DeleteSourceCommandResponse>
{
    private readonly IAsyncRepository<Source> _sourceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteSourceCommandHandler> _logger;

    public DeleteSourceCommandHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository, ILogger<DeleteSourceCommandHandler> logger)
    {
        _mapper = mapper;
        _sourceRepository = sourceRepository;
        _logger = logger;
    }

    public async Task<DeleteSourceCommandResponse> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
    {
        var deleteSourceResponse = new DeleteSourceCommandResponse();

        try
        {
            var eventToDelete = await _sourceRepository.GetByIdAsync(request.Id);

            if (eventToDelete == null)
            {
                throw new NotFoundException(nameof(Source), request.Id);
            }

            await _sourceRepository.DeleteAsync(eventToDelete);

            return deleteSourceResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Deleting source '{request.Id}' failed due to an error: {ex.Message}");
            throw;
        }
    }
}

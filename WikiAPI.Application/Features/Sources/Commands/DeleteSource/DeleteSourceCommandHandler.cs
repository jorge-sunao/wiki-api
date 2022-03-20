using AutoMapper;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Exceptions;
using WikiAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WikiAPI.Application.Features.Sources.Commands.DeleteSource;

public class DeleteSourceCommandHandler : IRequestHandler<DeleteSourceCommand, DeleteSourceCommandResponse>
{
    private readonly IAsyncRepository<Source> _sourceRepository;
    private readonly IMapper _mapper;
    
    public DeleteSourceCommandHandler(IMapper mapper, IAsyncRepository<Source> sourceRepository)
    {
        _mapper = mapper;
        _sourceRepository = sourceRepository;
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
            deleteSourceResponse.Success = false;
            deleteSourceResponse.ValidationErrors = new List<string>();
            deleteSourceResponse.ValidationErrors.Add(ex.Message);

            return deleteSourceResponse;
        }
    }
}

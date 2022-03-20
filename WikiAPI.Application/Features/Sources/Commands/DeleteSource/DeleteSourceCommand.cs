using MediatR;
using System;

namespace WikiAPI.Application.Features.Sources.Commands.DeleteSource;

public class DeleteSourceCommand: IRequest<DeleteSourceCommandResponse>
{
    public int Id { get; set; }
}

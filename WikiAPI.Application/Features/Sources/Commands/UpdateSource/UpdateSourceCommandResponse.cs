using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Responses;

namespace WikiAPI.Application.Features.Sources.Commands.UpdateSource;

public class UpdateSourceCommandResponse: BaseResponse
{
    public UpdateSourceCommandResponse() : base()
    {
    }

    public SourceDto Source { get; set; }
}

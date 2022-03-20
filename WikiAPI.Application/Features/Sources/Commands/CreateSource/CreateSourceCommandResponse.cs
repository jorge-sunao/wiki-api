using WikiAPI.Application.Common.Dtos;
using WikiAPI.Application.Responses;

namespace WikiAPI.Application.Features.Sources.Commands.CreateSource;

public class CreateSourceCommandResponse: BaseResponse
{
    public CreateSourceCommandResponse() : base()
    {
    }

    public SourceDto Source { get; set; }
}

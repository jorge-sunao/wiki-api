using WikiAPI.Application.Responses;

namespace WikiAPI.Application.Features.Sources.Commands.CreateSource
{
    public class CreateSourceCommandResponse: BaseResponse
    {
        public CreateSourceCommandResponse() : base()
        {

        }

        public CreateSourceDto Source { get; set; }
    }
}
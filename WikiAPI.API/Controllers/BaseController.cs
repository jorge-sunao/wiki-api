using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WikiAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator mediator;
    protected IMediator _mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}

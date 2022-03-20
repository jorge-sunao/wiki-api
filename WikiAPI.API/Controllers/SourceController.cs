using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WikiAPI.Application.Extensions;
using WikiAPI.Application.Features.Sources.Commands.CreateSource;
using WikiAPI.Application.Features.Sources.Commands.DeleteSource;
using WikiAPI.Application.Features.Sources.Commands.UpdateSource;
using WikiAPI.Application.Features.Sources.Queries.GetSourceDetail;
using WikiAPI.Application.Features.Sources.Queries.GetSourcesList;

namespace WikiAPI.API.Controllers;

[Route("api/Sources")]
public class SourceController : BaseController
{
    private readonly IDistributedCache _cache;

    public SourceController(IDistributedCache cache)
    {
        _cache = cache;
    }

    [HttpGet(Name = "GetListOfSources")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Any, Duration = 30)]
    public async Task<ActionResult<GetSourcesListQueryResponse>> GetAllSources()
    {
        var cacheKey = "GET_ALL_SOURCES";
        var response = await _cache.GetRecordAsync<GetSourcesListQueryResponse>(cacheKey);
        if (response is null)
        {
            response = await _mediator.Send(new GetSourcesListQuery());
            await _cache.SetRecordAsync(cacheKey, response);
        }
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetSourceById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Any, Duration = 30)]
    public async Task<ActionResult<GetSourceDetailQueryResponse>> GetSourceById(int id)
    {
        var cacheKey = $"GET_SOURCE_{id}";
        var getSourceDetailQuery = new GetSourceDetailQuery() { Id = id };

        var response = await _cache.GetRecordAsync<GetSourceDetailQueryResponse>(cacheKey);
        if (response is null)
        {
            response = await _mediator.Send(getSourceDetailQuery);
            await _cache.SetRecordAsync(cacheKey, response);
        }
        return Ok(response);
    }

    [HttpPost(Name = "AddSource")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateSourceCommandResponse>> Create([FromBody] CreateSourceCommand createSourceCommand)
    {
        var response = await _mediator.Send(createSourceCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateSource")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateSourceCommandResponse>> Update([FromBody] UpdateSourceCommand updateSourceCommand)
    {
        var response = await _mediator.Send(updateSourceCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteSource")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteSourceCommandResponse>> Delete(int id)
    {
        var deleteSourceCommand = new DeleteSourceCommand() { Id = id };
        var response = await _mediator.Send(deleteSourceCommand);
        return Ok(response);
    }
}

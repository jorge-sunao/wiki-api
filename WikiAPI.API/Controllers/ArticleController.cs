using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WikiAPI.Application.Extensions;
using WikiAPI.Application.Features.Articles.Commands.CreateArticle;
using WikiAPI.Application.Features.Articles.Commands.DeleteArticle;
using WikiAPI.Application.Features.Articles.Commands.UpdateArticle;
using WikiAPI.Application.Features.Articles.Queries.GetArticleDetail;
using WikiAPI.Application.Features.Articles.Queries.GetArticlesList;

namespace WikiAPI.API.Controllers;

[Route("api/Articles")]
public class ArticleController : BaseController
{
    private readonly IDistributedCache _cache;

    public ArticleController(IDistributedCache cache)
    {
        _cache = cache;
    }

    [HttpGet(Name = "GetListOfArticles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Any, Duration = 30)]
    public async Task<ActionResult<GetArticlesListQueryResponse>> GetList()
    {
        var cacheKey = "GET_ALL_ARTICLES";
        var response = await _cache.GetRecordAsync<GetArticlesListQueryResponse>(cacheKey);
        if (response is null)
        {
            response = await _mediator.Send(new GetArticlesListQuery());
            await _cache.SetRecordAsync(cacheKey, response);
        }
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetArticleById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Any, Duration = 30)]
    public async Task<ActionResult<GetArticleDetailQueryResponse>> GetById(int id)
    {
        var cacheKey = $"GET_ARTICLE_{id}";
        var getArticleDetailQuery = new GetArticleDetailQuery() { Id = id };

        var response = await _cache.GetRecordAsync<GetArticleDetailQueryResponse>(cacheKey);
        if (response is null)
        {
            response = await _mediator.Send(getArticleDetailQuery);
            await _cache.SetRecordAsync(cacheKey, response);
        }
        return Ok(response);
    }

    [HttpPost(Name = "AddArticle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateArticleCommandResponse>> Create([FromBody] CreateArticleCommand createArticleCommand)
    {
        var response = await _mediator.Send(createArticleCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateArticle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateArticleCommandResponse>> Update([FromBody] UpdateArticleCommand updateArticleCommand)
    {
        var response = await _mediator.Send(updateArticleCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteArticle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteArticleCommandResponse>> Delete(int id)
    {
        var deleteArticleCommand = new DeleteArticleCommand() { Id = id };
        var response = await _mediator.Send(deleteArticleCommand);
        return Ok(response);
    }
}

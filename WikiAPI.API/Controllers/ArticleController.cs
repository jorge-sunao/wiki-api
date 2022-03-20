using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WikiAPI.Application.Features.Articles.Commands.CreateArticle;
using WikiAPI.Application.Features.Articles.Commands.DeleteArticle;
using WikiAPI.Application.Features.Articles.Commands.UpdateArticle;
using WikiAPI.Application.Features.Articles.Queries.GetArticleDetail;
using WikiAPI.Application.Features.Articles.Queries.GetArticlesList;

namespace WikiAPI.API.Controllers;

[Route("api/Articles")]
public class ArticleController : BaseController
{
    [HttpGet(Name = "GetListOfArticles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetArticlesListQueryResponse>> GetList()
    {
        var response = await _mediator.Send(new GetArticlesListQuery());
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetArticleById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetArticleDetailQueryResponse>> GetById(int id)
    {
        var getArticleDetailQuery = new GetArticleDetailQuery() { Id = id };
        return Ok(await _mediator.Send(getArticleDetailQuery));
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
    public async Task<ActionResult<UpdateArticleCommandResponse>> Update([FromBody] UpdateArticleCommand updateArticleCommand)
    {
        var response = await _mediator.Send(updateArticleCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteArticle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteArticleCommandResponse>> Delete(int id)
    {
        var deleteArticleCommand = new DeleteArticleCommand() { Id = id };
        var response = await _mediator.Send(deleteArticleCommand);
        return Ok(response);
    }
}

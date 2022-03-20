using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Features.Articles.Queries.GetArticleDetail;
using WikiAPI.Application.Features.Articles.Queries.GetArticlesList;
using WikiAPI.Application.Profiles;
using WikiAPI.Application.UnitTests.Mocks;
using WikiAPI.Domain.Entities;
using Xunit;

namespace WikiAPI.Application.UnitTests.Articles.Queries;

public class GetArticleDetailQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IArticleRepository> _mockCategoryRepository;
    private readonly Mock<ILogger<GetArticleDetailQueryHandler>> _mockLogger;

    public GetArticleDetailQueryHandlerTest()
    {
        _mockCategoryRepository = RepositoryMocks.GetArticleRepository();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();

        _mockLogger = new Mock<ILogger<GetArticleDetailQueryHandler>>();
    }

    [Fact]
    public async Task GetArticleDetailTest()
    {
        var handler = new GetArticleDetailQueryHandler(_mapper, _mockCategoryRepository.Object, _mockLogger.Object);

        var result = await handler.Handle(new GetArticleDetailQuery(), CancellationToken.None);

        result.ShouldBeOfType<GetArticleDetailQueryResponse>();
        result.Article.Id.ShouldBe(4);
        result.Article.Title.ShouldBe("Shadows and Fog");
        result.Article.Slug.ShouldBe("/magnis/dis/parturient/montes/nascetur/ridiculus");
        result.Article.Version.ShouldBe(59);
        result.Article.DatePublished.ShouldBe(DateTimeOffset.Parse("2021-05-17T01:41:31Z").UtcDateTime);
        result.Article.Author.ShouldBe("Corey Guenther");
        result.Article.Content.ShouldBe("vestibulum velit id pretium iaculis diam erat fermentum justo nec condimentum neque sapien placerat ante nulla justo aliquam");
        result.Article.Sources.Count.ShouldBe(2);
        result.Success.ShouldBeTrue();
    }
}

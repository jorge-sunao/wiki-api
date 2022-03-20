using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Application.Features.Articles.Commands.CreateArticle;
using WikiAPI.Application.Profiles;
using WikiAPI.Application.UnitTests.Mocks;
using WikiAPI.Domain.Entities;
using Xunit;
using Microsoft.Extensions.Logging;

namespace WikiAPI.Application.UnitTests.Articles.Commands;

public class CreateArticleTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IArticleRepository> _mockArticleRepository;
    private readonly Mock<ILogger<CreateArticleCommandHandler>> _mockLogger;

    public CreateArticleTests()
    {
        _mockArticleRepository = RepositoryMocks.GetArticleRepository();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();

        _mockLogger = new Mock<ILogger<CreateArticleCommandHandler>>();
    }

    [Fact]
    public async Task Handle_ValidArticle_AddedToArticlesRepo()
    {
        var handler = new CreateArticleCommandHandler(_mapper, _mockArticleRepository.Object, _mockLogger.Object);

        await handler.Handle(new CreateArticleCommand() {
            Title = "Company II, The",
            Slug = "/cubilia/curae/donec/pharetra/magna/vestibulum/aliquet2",
            Version = 1,
            DatePublished = DateTimeOffset.Parse("2020-10-22T18:46:33Z").UtcDateTime,
            Author = "Garey Musk",
            Content = "et tempus semper est quam pharetra magna ac consequat metus sapien ut nunc vestibulum ante ipsum primis in"
        }, CancellationToken.None);

        var allArticles = await _mockArticleRepository.Object.ListAllAsync();
        allArticles.Count().ShouldBe(5);
    }
}

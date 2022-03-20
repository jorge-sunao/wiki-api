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
using WikiAPI.Application.Features.Articles.Commands.CreateArticle;
using WikiAPI.Application.Features.Articles.Commands.DeleteArticle;
using WikiAPI.Application.Profiles;
using WikiAPI.Application.UnitTests.Mocks;
using WikiAPI.Domain.Entities;
using Xunit;

namespace WikiAPI.Application.UnitTests.Articles.Commands;

public class DeleteArticleTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IArticleRepository> _mockArticleRepository;
    private readonly Mock<ILogger<DeleteArticleCommandHandler>> _mockLogger;

    public DeleteArticleTests()
    {
        _mockArticleRepository = RepositoryMocks.GetArticleRepository();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();

        _mockLogger = new Mock<ILogger<DeleteArticleCommandHandler>>();
    }

    [Fact]
    public async Task Handle_ValidArticle_DeletedArticlesRepo()
    {
        var handler = new DeleteArticleCommandHandler(_mapper, _mockArticleRepository.Object, _mockLogger.Object);

        await handler.Handle(new DeleteArticleCommand() { Id = 4}, CancellationToken.None);

        var allArticles = await _mockArticleRepository.Object.ListAllAsync();
        allArticles.Count().ShouldBe(4);
    }
}

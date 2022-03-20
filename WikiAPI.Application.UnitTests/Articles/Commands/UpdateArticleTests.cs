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
using WikiAPI.Application.Features.Articles.Commands.UpdateArticle;
using WikiAPI.Application.Profiles;
using WikiAPI.Application.UnitTests.Mocks;
using WikiAPI.Domain.Entities;
using Xunit;

namespace WikiAPI.Application.UnitTests.Articles.Commands;

public class UpdateArticleTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IArticleRepository> _mockArticleRepository;

    public UpdateArticleTests()
    {
        _mockArticleRepository = RepositoryMocks.GetArticleRepository();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task Handle_ValidArticle_UpdatedToArticlesRepo()
    {
        var handler = new UpdateArticleCommandHandler(_mapper, _mockArticleRepository.Object);

        await handler.Handle(new UpdateArticleCommand() {
            Id = 4,
            Title = "Company II, The",
            Slug = "/cubilia/curae/donec/pharetra/magna/vestibulum/aliquet2",
            DatePublished = DateTimeOffset.Parse("2020-10-22T18:46:33Z").UtcDateTime,
            Author = "Garey Musk",
            Content = "et tempus semper est quam pharetra magna ac consequat metus sapien ut nunc vestibulum ante ipsum primis in"
        }, CancellationToken.None);

        var article = await _mockArticleRepository.Object.GetByIdAsync(4);
        article.Title.ShouldBe("Company II, The");
        article.Slug.ShouldBe("/cubilia/curae/donec/pharetra/magna/vestibulum/aliquet2");
        article.Author.ShouldBe("Garey Musk");
        article.Content.ShouldBe("et tempus semper est quam pharetra magna ac consequat metus sapien ut nunc vestibulum ante ipsum primis in");
        article.DatePublished.ShouldBe(DateTimeOffset.Parse("2020-10-22T18:46:33Z").UtcDateTime);
    }
}

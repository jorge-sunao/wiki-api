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
using WikiAPI.Application.Features.Articles.Queries.GetArticlesList;
using WikiAPI.Application.Profiles;
using WikiAPI.Application.UnitTests.Mocks;
using WikiAPI.Domain.Entities;
using Xunit;

namespace WikiAPI.Application.UnitTests.Articles.Queries;

public class GetArticlesListQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IArticleRepository> _mockCategoryRepository;

    public GetArticlesListQueryHandlerTest()
    {
        _mockCategoryRepository = RepositoryMocks.GetArticleRepository();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task GetArticlesListTest()
    {
        var handler = new GetArticlesListQueryHandler(_mapper, _mockCategoryRepository.Object);

        var result = await handler.Handle(new GetArticlesListQuery(), CancellationToken.None);

        result.ShouldBeOfType<GetArticlesListQueryResponse>();

        result.Articles.Count.ShouldBe(4);
        result.Success.ShouldBeTrue();
    }
}

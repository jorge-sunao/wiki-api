using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WikiAPI.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IArticleRepository> GetArticleRepository()
        {
            var articles = new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "Immensee",
                    Slug = "/vel/accumsan/tellus/nisi/eu/orci",
                    Version = 35,
                    DatePublished = DateTimeOffset.Parse("2021-11-24T17:39:09Z").UtcDateTime,
                    Author = "Ailene Lamberto",
                    Content = "sapien cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus etiam vel",
                    CreatedDate = DateTimeOffset.Parse("2021-03-04T01:15:55Z").UtcDateTime
                },
                new Article
                {
                    Id = 2,
                    Title = "Mountains of the Moon",
                    Slug = "/diam/cras/pellentesque/volutpat/dui",
                    Version = 79,
                    DatePublished = DateTimeOffset.Parse("2021-02-19T05:51:48Z").UtcDateTime,
                    Author = "Nickie Stanistreet",
                    Content = "ipsum ac tellus semper interdum mauris ullamcorper purus sit amet nulla quisque arcu libero",
                    CreatedDate = DateTimeOffset.Parse("2020-03-21T15:02:54Z").UtcDateTime,
                    LastModifiedDate = DateTimeOffset.Parse("2020-02-22T04:30:43Z").UtcDateTime,
                },
                new Article
                {
                    Id = 3,
                    Title = "Piled Higher and Deeper",
                    Slug = "/odio/elementum/eu/interdum/eu/tincidunt",
                    Version = 53,
                    DatePublished = DateTimeOffset.Parse("2021-09-22T02:45:55Z").UtcDateTime,
                    Author = "Kalinda Gosneye",
                    Content = "elit proin risus praesent lectus vestibulum quam sapien varius ut blandit non interdum in ante",
                    CreatedDate = DateTimeOffset.Parse("2020-03-12T14:02:32Z").UtcDateTime,
                    LastModifiedDate = DateTimeOffset.Parse("2020-02-22T04:30:43Z").UtcDateTime,
                },
                new Article
                {
                    Id = 4,
                    Title = "Shadows and Fog",
                    Slug = "/magnis/dis/parturient/montes/nascetur/ridiculus",
                    Version = 59,
                    DatePublished = DateTimeOffset.Parse("2021-05-17T01:41:31Z").UtcDateTime,
                    Author = "Corey Guenther",
                    Content = "vestibulum velit id pretium iaculis diam erat fermentum justo nec condimentum neque sapien placerat ante nulla justo aliquam",
                    CreatedDate = DateTimeOffset.Parse("2021-03-17T17:45:52Z").UtcDateTime
                },
            };

            var articleDetail = new Article
            {
                Id = 4,
                Title = "Shadows and Fog",
                Slug = "/magnis/dis/parturient/montes/nascetur/ridiculus",
                Version = 59,
                DatePublished = DateTimeOffset.Parse("2021-05-17T01:41:31Z").UtcDateTime,
                Author = "Corey Guenther",
                Content = "vestibulum velit id pretium iaculis diam erat fermentum justo nec condimentum neque sapien placerat ante nulla justo aliquam",
                CreatedDate = DateTimeOffset.Parse("2021-03-17T17:45:52Z").UtcDateTime,
                Sources = new List<Source>
                {
                    new Source
                    {
                    Id = 1,
                    Author = "Lottie Joseph",
                    Title = "Blood Out",
                    Year = 2019,
                    ArticleId = 4,
                    CreatedDate = DateTimeOffset.Parse("2021-11-18T22:10:25Z").UtcDateTime
                    },
                    new Source
                    {
                    Id = 2,
                    Author = "Neilla Joddens",
                    Title = "Sube y Baja",
                    Year = 2001,
                    Country = "Colombia",
                    Publisher = "Gabvine",
                    ArticleId = 4,
                    CreatedDate = DateTimeOffset.Parse("2021-11-18T22:10:25Z").UtcDateTime,
                    LastModifiedDate = DateTimeOffset.Parse("2021-09-11T18:35:57Z").UtcDateTime
                    },
                }
            };

            var articleInfo = new Article
            {
                Id = 4,
                Title = "Shadows and Fog",
                Slug = "/magnis/dis/parturient/montes/nascetur/ridiculus",
                Version = 59,
                DatePublished = DateTimeOffset.Parse("2021-05-17T01:41:31Z").UtcDateTime,
                Author = "Corey Guenther",
                Content = "vestibulum velit id pretium iaculis diam erat fermentum justo nec condimentum neque sapien placerat ante nulla justo aliquam",
                CreatedDate = DateTimeOffset.Parse("2021-03-17T17:45:52Z").UtcDateTime
            };

            var articleReturn = new Article
            {
                Id = 4,
                Title = "Company II, The",
                Slug = "/cubilia/curae/donec/pharetra/magna/vestibulum/aliquet2",
                Version = 2,
                DatePublished = DateTimeOffset.Parse("2020-10-22T18:46:33Z").UtcDateTime,
                Author = "Garey Musk",
                Content = "et tempus semper est quam pharetra magna ac consequat metus sapien ut nunc vestibulum ante ipsum primis in"
            };

            var mockArticleRepository = new Mock<IArticleRepository>();

            mockArticleRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(articles);

            mockArticleRepository.Setup(repo => repo.GetArticleWithSources(It.IsAny<int>())).ReturnsAsync(articleDetail);

            mockArticleRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(articleInfo);

            mockArticleRepository.Setup(repo => repo.IsArticleTitleAndAuthorUnique(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>())).ReturnsAsync(true);
            mockArticleRepository.Setup(repo => repo.IsSlugUnique(It.IsAny<string>(), It.IsAny<int?>())).ReturnsAsync(true);

            mockArticleRepository.Setup(repo => repo.AddAsync(It.IsAny<Article>())).ReturnsAsync(
                (Article article) =>
                {
                    articles.Add(article);
                    return article.Id;
                });

            mockArticleRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Article>()))
                .Callback((Article article) => articles = articles.GetRange(0, articles.Count - 1))
                .Returns(Task.FromResult((object)null)); ;

            mockArticleRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Article>()))
                .Callback((Article article) => articleDetail = articleReturn)
                .Returns(Task.FromResult((object)null));

            return mockArticleRepository;
        }
    }
}

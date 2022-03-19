using System;

namespace WikiAPI.Application.Features.Articles.Queries.GetArticleDetail
{
    public class ArticleDetailViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int Version { get; set; }
        public DateTime DatePublished { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public ICollection<SourceDto> Sources { get; set; }
    }
}

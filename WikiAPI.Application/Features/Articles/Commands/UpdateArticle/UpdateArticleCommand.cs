using MediatR;
using System;

namespace WikiAPI.Application.Features.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommand: IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int Version { get; set; }
        public DateTime DatePublished { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public ICollection<int> SourceIds { get; set; }
    }
}

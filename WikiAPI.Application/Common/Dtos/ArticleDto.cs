using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiAPI.Application.Common.Dtos;

public class ArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public int? Version { get; set; }
    public DateTime DatePublished { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

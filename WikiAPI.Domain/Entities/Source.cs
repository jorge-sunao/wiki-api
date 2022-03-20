using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Domain.Common;

namespace WikiAPI.Domain.Entities;

public class Source: AuditEntity
{
    public int Id { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public int? Year { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? Publisher { get; set; }
    public string? Pages { get; set; }
    public string? URL { get; set; }
    public int ArticleId { get; set; }
    [Write(false)]
    public Article Article { get; set; }

}

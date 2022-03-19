﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Domain.Common;

namespace WikiAPI.Domain.Entities
{
    public class Article: AuditEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int Version { get; set; }
        public DateTime DatePublished { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public ICollection<Source> Sources { get; set; }
    }
}

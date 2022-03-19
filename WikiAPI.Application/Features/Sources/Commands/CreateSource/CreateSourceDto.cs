using System;

namespace WikiAPI.Application.Features.Sources.Commands.CreateSource
{
    public class CreateSourceDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Publisher { get; set; }
        public string Pages { get; set; }
        public string URL { get; set; }
    }
}

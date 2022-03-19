using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Application.Features.Articles.Commands.CreateArticle;
using WikiAPI.Application.Features.Articles.Commands.UpdateArticle;
using WikiAPI.Application.Features.Articles.Queries.GetArticleDetail;
using WikiAPI.Application.Features.Articles.Queries.GetArticlesList;
using WikiAPI.Application.Features.Sources.Queries.GetSourceDetail;
using WikiAPI.Application.Features.Sources.Queries.GetSourcesList;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleListViewModel>().ReverseMap();
            CreateMap<Article, ArticleDetailViewModel>().ReverseMap();
            CreateMap<Article, CreateArticleCommand>().ReverseMap();
            CreateMap<Article, UpdateArticleCommand>().ReverseMap();
            CreateMap<Source, SourceDto>();
            CreateMap<Source, SourceListViewModel>().ReverseMap();
            CreateMap<Source, SourceDetailViewModel>().ReverseMap();
        }
    }
}

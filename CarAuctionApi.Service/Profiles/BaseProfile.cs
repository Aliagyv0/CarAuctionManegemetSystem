using AutoMapper;
using CarAuctionApi.Core.Models;
using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.DTOs.Response;
using CarAuctionApi.Service.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Profiles
{
    public class BaseProfile : Profile
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseProfile(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();

            CreateMap<TagDto, Tag>().ReverseMap();
            CreateMap<Tag, TagGetDto>().ReverseMap();

            CreateMap<Slider, SliderDto>().ReverseMap();
            CreateMap<SliderGetDto, Slider>().ReverseMap()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(
                src => _httpContextAccessor.HttpContext.GetImageUrl($"/images/sliders/{src.ImageUrl}")));

            CreateMap<NewsImageGetDto, NewsImage>().ReverseMap()
                 .ForMember(x => x.ImageUrl, opt => opt.MapFrom(src => _httpContextAccessor.HttpContext.GetImageUrl($"/images/news/{src.ImageUrl}")));

            CreateMap<NewsDto, News>().ReverseMap();
            CreateMap<NewsGetDto, News>().ReverseMap()
           .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.NewsTags.Select(x=>x.Tag).ToList()));

        }
        public BaseProfile()
        {
            
        }
    }
}

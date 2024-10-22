using AutoMapper;
using CarAuctionApi.Core.Models;
using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Profiles
{
    public class SliderProfile : Profile
    {
        public SliderProfile()
        {
            CreateMap<SliderProfile,SliderDto>().ReverseMap();
            CreateMap<SliderGetDto,Slider>().ReverseMap();
        }
    }
}

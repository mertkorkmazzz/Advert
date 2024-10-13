using Advert.Entity.DTOs.AdvertsDTOs;
using Advert.Entity.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Services.AutoMapper.Advertes
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertDto , AdvertsEntity>().ReverseMap();
            CreateMap<AdvertUpdateDto, AdvertsEntity>().ReverseMap();
            CreateMap<AdvertUpdateDto, AdvertDto>().ReverseMap();
            CreateMap<AdvertAddDto, AdvertsEntity>().ReverseMap();
         
        }
    }
}

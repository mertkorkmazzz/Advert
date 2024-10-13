using Advert.Entity.DTOs.AdvertsDTOs;
using Advert.Entity.DTOs.CategoryDTOs;
using Advert.Entity.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Services.AutoMapper.Categories
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, CategoryEntity>().ReverseMap();
            CreateMap<CategoryAddDto, CategoryEntity>().ReverseMap();
            CreateMap<CategoryUpdateDto, CategoryEntity>().ReverseMap();
           
        }
    }
}

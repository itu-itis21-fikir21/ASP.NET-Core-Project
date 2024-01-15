using AutoMapper;
using Entity.DTOs.Categories;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapper.Categories
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryAddDto, Category>().ReverseMap(); 
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();  
        }
    }
}

using AutoMapper;
using Onion.JwpApp.Application.Dtos;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<Category, CreatedCategoryDto>().ReverseMap();
        }
    }
}

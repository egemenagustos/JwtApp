using AutoMapper;
using Onion.JwpApp.Application.Dtos;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<Product, CreatedProductDto>().ReverseMap();
        }
    }
}

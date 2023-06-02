using AutoMapper;
using Onion.JwpApp.Application.Dtos;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Mappings
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, CreatedUserDto>().ReverseMap();
            CreateMap<AppUser, UserListDto>().ReverseMap();
        }
    }
}

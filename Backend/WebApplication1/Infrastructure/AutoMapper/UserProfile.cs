using AutoMapper;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Infrastructure.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserEntity, UserResponse>()
                .ForMember(dest => dest.Id,         src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Password,   src => src.MapFrom(x => x.Password))
                .ForMember(dest => dest.FirstName,  src => src.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.SurName,    src => src.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Email,      src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.Company,    src => src.MapFrom(x => x.Company))
                .ForMember(dest => dest.Status,     src => src.MapFrom(x => x.Status));
        }
    }
}

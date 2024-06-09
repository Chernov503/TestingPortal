using AutoMapper;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Infrastructure.AutoMapper
{
    public class TestResultProfile : Profile
    { 
        public TestResultProfile() 
        {
            CreateMap<TestResultEntity, TestResultResponse>()
                .ForMember(dest => dest.Id,             src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.testTitle,      src => src.MapFrom(x => String.Empty))
                .ForMember(dest => dest.UserId,         src => src.MapFrom(x => x.UserId))
                .ForMember(dest => dest.TestId,         src => src.MapFrom(x => x.TestId))
                .ForMember(dest => dest.ResultAnswers,  src => src.MapFrom(x => x.ResultAnswers))
                .ForMember(dest => dest.ResultPercent,  src => src.MapFrom(x => x.ResultPercent))
                .ForMember(dest => dest.ResultDateTime, scr => scr.MapFrom(x => x.ResultDateTime));
                
        }
    }
}

using AutoMapper;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Infrastructure.AutoMapper
{
    public class QuestionCorrectInfoProfile : Profile
    {
        public QuestionCorrectInfoProfile() 
        {

            CreateMap<CreateQuestionCorrectInfoRequest, QuestionCorrectInfoEntity>()
                .MaxDepth(1)
                .ForMember(dest => dest.Id, src => src.MapFrom(x => Guid.NewGuid()))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
                .ForMember(dest => dest.ImageLink, src => src.MapFrom(x => x.ImageLink))
                .ForMember(dest => dest.VideoLink, src => src.MapFrom(x => x.VideoLink));


            CreateMap<QuestionCorrectInfoEntity, QuestionCorrectInfoResponse>()
                .MaxDepth(1)
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.QuestionId, src => src.MapFrom(x => x.QuestionId))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
                .ForMember(dest => dest.VideoLink, src => src.MapFrom(x => x.VideoLink))
                .ForMember(dest => dest.ImageLink, src => src.MapFrom(x => x.ImageLink));
        }
    }
}

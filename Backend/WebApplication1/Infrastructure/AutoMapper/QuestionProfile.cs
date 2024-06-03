using AutoMapper;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Infrastructure.AutoMapper
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionEntity, QuestionResponse>()
                .ForMember(dest => dest.Id,             src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.TestId,         src => src.MapFrom(x => x.TestId))
                .ForMember(dest => dest.QuestionTitle,  src => src.MapFrom(x => x.QuestionTitle))
                .ForMember(dest => dest.OptionCount,    src => src.MapFrom(x => x.OptionCount))
                .ForMember(dest => dest.CorrectOptionCount,src => src.MapFrom(x => x.CorrectOptionCount));

            CreateMap<CreateQuestionRequest, QuestionEntity>()
                .MaxDepth(1)
                .ForMember(dest => dest.Id,             src => src.MapFrom(x => Guid.NewGuid()))
                .ForMember(dest => dest.QuestionTitle,  src => src.MapFrom(x => x.QuestionTitle))
                .ForMember(dest => dest.OptionCount,    src => src.MapFrom(x => x.OptionCount))
                .ForMember(dest => dest.CorrectOptionCount, src => src.MapFrom(x => x.CorrectOptionCount));
                
        }
    }
}

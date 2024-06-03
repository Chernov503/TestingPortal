using AutoMapper;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Infrastructure.AutoMapper
{
    public class QuestionOptionProfile : Profile
    {
        public QuestionOptionProfile()
        {
            CreateMap<QuestionOptionEntity, QuestionOptionResponse>()
                .ForMember(dest => dest.Id,         src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.QuestionId, src => src.MapFrom(x => x.QuestionId))
                .ForMember(dest => dest.Text,       src => src.MapFrom(x => x.Text));

            CreateMap<CreateQuestionOptionRequest, QuestionOptionEntity>()
                .MaxDepth(1)
                .ForMember(dest => dest.Id,         src => src.MapFrom(x => Guid.NewGuid()))
                .ForMember(dest => dest.Text,       src => src.MapFrom(x => x.Text))
                .ForMember(dest => dest.IsCorrect,  src => src.MapFrom(x => x.IsCorrect));
        }
    }
}

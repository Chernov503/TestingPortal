using AutoMapper;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Infrastructure.AutoMapper
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {

            CreateMap<TestEntity, TestToDoingResponse>()
                .ForMember(dest => dest.Id,         src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Questions,  opt => opt.Ignore());


            CreateMap<CreateTestRequest, TestEntity>()
                .MaxDepth(1)
                .ForMember(dest => dest.Id, src => src.MapFrom(x => Guid.NewGuid()))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.Category, src => src.MapFrom(x => x.Category))
                .ForMember(dest => dest.CreatedDate, src => src.MapFrom(x => DateTimeOffset.UtcNow))
                .ForMember(dest => dest.IsPrivate, src => src.MapFrom(x => x.IsPrivate))
                .ForMember(dest => dest.CompanyOwners, src => src.MapFrom(x => x.CompanyOwners))
                .ForMember(dest => dest.Questions, src => src.Ignore())
                .ForMember(dest => dest.TestResults, src => src.Ignore())
                .ForMember(dest => dest.AccesToTests, src => src.Ignore());

            CreateMap<TestEntity, TestFullInfo>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .MaxDepth(1);



        }
    }
}

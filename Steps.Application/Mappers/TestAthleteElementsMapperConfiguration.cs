using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.AthleteElements.ViewModels;

namespace Steps.Application.Mappers;

public class TestAthleteElementsMapperConfiguration : Profile
{
    public TestAthleteElementsMapperConfiguration()
    {
        CreateMap<TestAthleteElementsViewModel, TestAthleteElement>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.Degree, x => x.MapFrom(m => m.Degree))
            .ForMember(x => x.AgeCategory, x => x.MapFrom(m => m.AgeCategory))
            .ForMember(x => x.Type, x => x.MapFrom(m => m.Type))
            .ForMember(x => x.Element1, x => x.MapFrom(m => m.Element1))
            .ForMember(x => x.Element2, x => x.MapFrom(m => m.Element2))
            .ForMember(x => x.Element3, x => x.MapFrom(m => m.Element3))
            .ForMember(x => x.Element4, x => x.MapFrom(m => m.Element4))
            .ForMember(x => x.Element5, x => x.MapFrom(m => m.Element5));

        CreateMap<TestAthleteElement, TestAthleteElementsViewModel>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.Degree, x => x.MapFrom(m => m.Degree))
            .ForMember(x => x.AgeCategory, x => x.MapFrom(m => m.AgeCategory))
            .ForMember(x => x.Type, x => x.MapFrom(m => m.Type))
            .ForMember(x => x.Element1, x => x.MapFrom(m => m.Element1))
            .ForMember(x => x.Element2, x => x.MapFrom(m => m.Element2))
            .ForMember(x => x.Element3, x => x.MapFrom(m => m.Element3))
            .ForMember(x => x.Element4, x => x.MapFrom(m => m.Element4))
            .ForMember(x => x.Element5, x => x.MapFrom(m => m.Element5));
    }
}
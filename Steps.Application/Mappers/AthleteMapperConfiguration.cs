using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Application.Mappers;

public class AthleteMapperConfiguration : Profile
{
    public AthleteMapperConfiguration()
    {
        CreateMap<AthleteViewModel, Athlete>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.FullName, x => x.MapFrom(m => m.FullName))
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.Team, x => x.Ignore())
            .ForMember(x => x.BirthDate, x => x.MapFrom(m => m.BirthDate))
            .ForMember(x => x.AthleteType, x => x.MapFrom(m => m.AthleteType))
            .ForMember(x => x.AgeCategory, x => x.MapFrom(m => m.AgeCategory))
            .ForMember(x => x.AthleteElements, x => x.Ignore())
            .ForMember(x => x.AthleteElementsId, x => x.Ignore());

        CreateMap<Athlete, AthleteViewModel>()
            .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
            .ForMember(x => x.FullName, x => x.MapFrom(m => m.FullName))
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.BirthDate, x => x.MapFrom(m => m.BirthDate))
            .ForMember(x => x.AthleteType, x => x.MapFrom(m => m.AthleteType))
            .ForMember(x => x.AgeCategory, x => x.MapFrom(m => m.AgeCategory));

        CreateMap<CreateAthleteViewModel, Athlete>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.FullName, x => x.MapFrom(m => m.FullName))
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.Team, x => x.Ignore())
            .ForMember(x => x.BirthDate, x => x.MapFrom(m => m.BirthDate))
            .ForMember(x => x.AthleteType, x => x.MapFrom(m => m.AthleteType))
            .ForMember(x => x.AgeCategory, x => x.MapFrom(m => m.AgeCategory))
            .ForMember(x => x.AthleteElements, x => x.Ignore())
            .ForMember(x => x.AthleteElementsId, x => x.Ignore());

        CreateMap<Athlete, CreateAthleteViewModel>()
            .ForMember(x => x.FullName, x => x.MapFrom(m => m.FullName))
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.BirthDate, x => x.MapFrom(m => m.BirthDate))
            .ForMember(x => x.AthleteType, x => x.MapFrom(m => m.AthleteType))
            .ForMember(x => x.AgeCategory, x => x.MapFrom(m => m.AgeCategory));
    }
}
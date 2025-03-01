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
            .ForMember(x => x.BirthDate, x => x.MapFrom(m => m.BirthDate))
            .ForMember(x => x.GroupBlocks, o => o.Ignore())
            .ForMember(x => x.Entries, o => o.Ignore())
            .ForMember(x => x.PreAthletesLists, o => o.Ignore())
            .ForMember(x => x.ExitTime, o => o.Ignore())
            .ForMember(x => x.IsAppeared, o => o.Ignore())
            .ForMember(x => x.Entries, o => o.Ignore());

        CreateMap<Athlete, AthleteViewModel>()
            .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
            .ForMember(x => x.FullName, x => x.MapFrom(m => m.FullName))
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.BirthDate, x => x.MapFrom(m => m.BirthDate));

        CreateMap<CreateAthleteViewModel, Athlete>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.FullName, x => x.MapFrom(m => m.FullName))
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.BirthDate, x => x.MapFrom(m => m.BirthDate))
            .ForMember(x => x.ExitTime, x => x.Ignore())
            .ForMember(x => x.IsAppeared, x => x.Ignore())
            .ForMember(x => x.Entries, x => x.Ignore()) 
            .ForMember(x => x.PreAthletesLists, x => x.Ignore()) 
            .ForMember(x => x.GroupBlocks, x => x.Ignore())
            .ForMember(x => x.Entries, o => o.Ignore());

        CreateMap<Athlete, CreateAthleteViewModel>()
            .ForMember(x => x.FullName, x => x.MapFrom(m => m.FullName))
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.BirthDate, x => x.MapFrom(m => m.BirthDate));
    }
}
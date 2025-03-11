using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Application.Mappers;

public class EntryMapperConfiguration : Profile
{
    public EntryMapperConfiguration()
    {
        // Из Entry в EntryViewModel
        CreateMap<Entry, EntryViewModel>()
            .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
            .ForMember(x => x.Number, x => x.MapFrom(m => m.Number))
            .ForMember(x => x.IsSuccess, x => x.MapFrom(m => m.IsSuccess))
            .ForMember(x => x.ContestId, x => x.MapFrom(m => m.ContestId))
            .ForMember(x => x.Contest, x => x.MapFrom(m => m.Contest))
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.Team, x => x.MapFrom(m => m.Team))
            .ForMember(x => x.Athletes, x => x.MapFrom(m => m.Athletes));

        // Из EntryViewModel в Entry
        CreateMap<EntryViewModel, Entry>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.Number, x => x.MapFrom(m => m.Number))
            .ForMember(x => x.IsSuccess, x => x.MapFrom(m => m.IsSuccess))
            .ForMember(x => x.SubmissionDate, x => x.Ignore())
            .ForMember(x => x.ContestId, x => x.MapFrom(m => m.ContestId))
            .ForMember(x => x.Contest, x => x.Ignore())
            .ForMember(x => x.CreatorId, x => x.Ignore())
            .ForMember(x => x.Creator, x => x.Ignore())
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.Team, x => x.MapFrom(m => m.Team))
            .ForMember(x => x.Athletes, x => x.MapFrom(m => m.Athletes));

        // Из CreateEntryViewModel в Entry
        CreateMap<CreateEntryViewModel, Entry>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.Contest, x => x.Ignore())
            .ForMember(x => x.Creator, x => x.Ignore())
            .ForMember(x => x.Number, x => x.Ignore())
            .ForMember(x => x.IsSuccess, x => x.Ignore())
            .ForMember(x => x.SubmissionDate, x => x.Ignore())
            .ForMember(x => x.ContestId, x => x.MapFrom(m => m.ContestId))
            .ForMember(x => x.CreatorId, x => x.Ignore())
            .ForMember(x => x.TeamId, x => x.MapFrom(m => m.TeamId))
            .ForMember(x => x.Team, x => x.Ignore())
            .ForMember(x => x.Athletes, x => x.Ignore());
        
        // Из Entry в CreateEntryViewModel
        CreateMap<Entry, CreateEntryViewModel>()
            .ForMember(x => x.ContestId, x => x.MapFrom(m => m.ContestId))
            .ForMember(x => x.AthletesIds, x => x.Ignore());
    }
}
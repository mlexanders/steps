using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application.Mappers;

public class ContestMapperConfiguration : Profile
{
    public ContestMapperConfiguration()
    {
        //CreateContestViewModel
        CreateMap<CreateContestViewModel, Contest>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
            .ForMember(x => x.Judjes, o => o.MapFrom(m => m.Judjes))
            .ForMember(x => x.Counters, o => o.MapFrom(m => m.Counters))
            .ForMember(x => x.Entries, o => o.Ignore()) 
            .ForMember(x => x.GeneratedAthletesListId, o => o.Ignore())
            .ForMember(x => x.GeneratedAthletesList, o => o.Ignore())
            .ForMember(x => x.LateAthletesListId, o => o.Ignore())
            .ForMember(x => x.LateAthletesList, o => o.Ignore())
            .ForMember(x => x.PreAthletesListId, o => o.Ignore())
            .ForMember(x => x.PreAthletesList, o => o.Ignore())
            .ForMember(x => x.GroupBlocks, o => o.Ignore());


        CreateMap<Contest, CreateContestViewModel>()
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
            .ForMember(x => x.Judjes, o => o.MapFrom(m => m.Judjes))
            .ForMember(x => x.Counters, o => o.MapFrom(m => m.Counters));

        //UpdateContestViewModel
        CreateMap<UpdateContestViewModel, Contest>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
            .ForMember(x => x.Judjes, o => o.Ignore()) 
            .ForMember(x => x.Counters, o => o.Ignore())
            .ForMember(x => x.Entries, o => o.Ignore())
            .ForMember(x => x.GeneratedAthletesListId, o => o.Ignore())
            .ForMember(x => x.GeneratedAthletesList, o => o.Ignore())
            .ForMember(x => x.LateAthletesListId, o => o.Ignore())
            .ForMember(x => x.LateAthletesList, o => o.Ignore())
            .ForMember(x => x.PreAthletesListId, o => o.Ignore())
            .ForMember(x => x.PreAthletesList, o => o.Ignore())
            .ForMember(x => x.GroupBlocks, o => o.Ignore());

        CreateMap<Contest, UpdateContestViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));
    }
}
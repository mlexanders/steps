using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application.Mappers;

public class ContestMapperConfiguration : Profile
{
    public ContestMapperConfiguration()
    {
        //ContestViewModel
        CreateMap<ContestViewModel, Contest>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.Judges, o => o.Ignore())
            .ForMember(x => x.Counters, o => o.Ignore())
            .ForMember(x => x.Entries, o => o.Ignore()) 
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Type, o => o.MapFrom(m => m.Type));

        CreateMap<Contest, ContestViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.JudjesIds, o => o.MapFrom(m => m.Judges.Select(j => j.Id).ToList()))
            .ForMember(x => x.CountersIds, o => o.MapFrom(m => m.Counters.Select(c => c.Id).ToList()))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate));

        //CreateContestViewModel
        CreateMap<CreateContestViewModel, Contest>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
            .ForMember(x => x.Judges, o => o.Ignore())
            .ForMember(x => x.Counters, o => o.Ignore())
            .ForMember(x => x.Entries, o => o.Ignore()) 
            .ForMember(x => x.Status, o => o.Ignore()) 
            .ForMember(x => x.Type, o => o.MapFrom(m => m.Type));


        CreateMap<Contest, CreateContestViewModel>()
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
            .ForMember(x => x.Judjes, o => o.Ignore())
            .ForMember(x => x.Counters, o => o.Ignore())
            .ForMember(x => x.Type, o => o.MapFrom(m => m.Type));

        //UpdateContestViewModel
        CreateMap<UpdateContestViewModel, Contest>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
            .ForMember(x => x.Judges, o => o.Ignore())
            .ForMember(x => x.Counters, o => o.Ignore())
            .ForMember(x => x.Type, o => o.Ignore())
            .ForMember(x => x.Entries, o => o.Ignore());

        CreateMap<Contest, UpdateContestViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
            .ForMember(x => x.JudjesIds, o => o.Ignore()) 
            .ForMember(x => x.CountersIds, o => o.Ignore()) ;
    }
}
using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Application.Mappers;

public class EventMapperConfiguration : Profile
{
    public EventMapperConfiguration()
    {
        CreateMap<CreateContestViewModel, Contest>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));

        CreateMap<Contest, CreateContestViewModel>()
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));
        
        CreateMap<UpdateContestViewModel, Contest>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));

        CreateMap<Contest, UpdateContestViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));
    }
}
using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Application.Mappers;

public class EventMapperConfiguration : Profile
{
    public EventMapperConfiguration()
    {
        CreateMap<CreateEventViewModel, Event>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));

        CreateMap<Event, CreateEventViewModel>()
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));
        
        CreateMap<UpdateEventViewModel, Event>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));

        CreateMap<Event, UpdateEventViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.StartDate, o => o.MapFrom(m => m.StartDate))
            .ForMember(x => x.EndDate, o => o.MapFrom(m => m.EndDate))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));
    }
}
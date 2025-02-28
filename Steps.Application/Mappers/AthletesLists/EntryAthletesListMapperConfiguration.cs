using AutoMapper;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared.Contracts.AthletesLists.EntryAthletesList.ViewModels;
using Steps.Shared.Contracts.AthletesLists.PreAthletesList.ViewModels;

namespace Steps.Application.Mappers.AthletesLists
{
    public class EntryAthletesListMapperConfiguration : Profile
    {
        public EntryAthletesListMapperConfiguration()
        {
            CreateMap<EntryAthletesListViewModel, EntryAthletesList>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.EntryId, o => o.MapFrom(m => m.EntryId))
                .ForMember(x => x.Entry, o => o.MapFrom(m => m.Entry))
                .ForMember(x => x.Athletes, o => o.MapFrom(m => m.Athletes));

            CreateMap<EntryAthletesList, EntryAthletesListViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.EntryId, o => o.MapFrom(m => m.EntryId))
                .ForMember(x => x.Entry, o => o.MapFrom(m => m.Entry))
                .ForMember(x => x.Athletes, o => o.MapFrom(m => m.Athletes));
        }
    }
}

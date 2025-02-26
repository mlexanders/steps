using AutoMapper;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared.Contracts.AthletesLists.PreAthletesList.ViewModels;

namespace Steps.Application.Mappers.AthletesLists
{
    public class PreAthletesListMapperConfiguration : Profile
    {
        public PreAthletesListMapperConfiguration()
        {
            CreateMap<PreAthletesListViewModel, PreAthletesList>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
                .ForMember(x => x.Contest, o => o.MapFrom(m => m.Contest))
                .ForMember(x => x.Athletes, o => o.MapFrom(m => m.Athletes));

            CreateMap<PreAthletesList, PreAthletesListViewModel>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
                .ForMember(x => x.Contest, o => o.MapFrom(m => m.Contest))
                .ForMember(x => x.Athletes, o => o.MapFrom(m => m.Athletes));
        }
    }
}

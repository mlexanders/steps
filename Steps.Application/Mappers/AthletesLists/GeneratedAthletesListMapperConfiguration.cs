using AutoMapper;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared.Contracts.AthletesLists.GeneratedAthletesList.ViewModels;

namespace Steps.Application.Mappers.AthletesLists
{
    public class GeneratedAthletesListMapperConfiguration : Profile
    {
        public GeneratedAthletesListMapperConfiguration()
        {
            CreateMap<GeneratedAthletesListViewModel, GeneratedAthletesList>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.GroupBlocks, o => o.MapFrom(m => m.GroupBlocks))
                .ForMember(x => x.Contest, o => o.MapFrom(m => m.Contest))
                .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId));

            CreateMap<GeneratedAthletesList, GeneratedAthletesListViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.GroupBlocks, o => o.MapFrom(m => m.GroupBlocks))
                .ForMember(x => x.Contest, o => o.MapFrom(m => m.Contest))
                .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId));
        }
    }
}

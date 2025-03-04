using AutoMapper;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared.Contracts.AthletesLists.LateAthletesList.ViewModels;

namespace Steps.Application.Mappers.AthletesLists
{
    public class LateAthletesListMapperConfiguration : Profile
    {
        public LateAthletesListMapperConfiguration()
        {
            CreateMap<LateAthletesListViewModel, LateAthletesList>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Athletes, opt => opt.MapFrom(src => src.Athletes))
                .ForMember(dest => dest.ContestId, opt => opt.MapFrom(src => src.ContestId))
                .ForMember(dest => dest.Contest, opt => opt.MapFrom(src => src.Contest));

            CreateMap<LateAthletesList, LateAthletesListViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Athletes, opt => opt.MapFrom(src => src.Athletes))
                .ForMember(dest => dest.ContestId, opt => opt.MapFrom(src => src.ContestId))
                .ForMember(dest => dest.Contest, opt => opt.MapFrom(src => src.Contest));
        }
    }
}

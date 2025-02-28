using AutoMapper;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared.Contracts.AthletesLists.GeneratedAthletesList.ViewModels;
using Steps.Shared.Contracts.AthletesLists.GroupBlock.ViewModels;

namespace Steps.Application.Mappers.AthletesLists
{
    public class GroupBlockMapperConfiguration : Profile
    {
        public GroupBlockMapperConfiguration()
        {
            CreateMap<GroupBlockViewModel, GroupBlock>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Numbers, opt => opt.MapFrom(src => src.Numbers))
                .ForMember(dest => dest.ExitTime, opt => opt.MapFrom(src => src.ExitTime))
                .ForMember(dest => dest.Athletes, opt => opt.MapFrom(src => src.Athletes))
                .ForMember(dest => dest.ContestId, opt => opt.MapFrom(src => src.ContestId))
                .ForMember(dest => dest.Contest, opt => opt.MapFrom(src => src.Contest))
                .ForMember(dest => dest.GeneratedAthletesListId, opt => opt.MapFrom(src => src.GeneratedAthletesListId))
                .ForMember(dest => dest.GeneratedAthletesList, opt => opt.MapFrom(src => src.GeneratedAthletesList))
                .ForMember(dest => dest.LateAthletesListId, opt => opt.MapFrom(src => src.LateAthletesListId))
                .ForMember(dest => dest.LateAthletesList, opt => opt.MapFrom(src => src.LateAthletesList));

            CreateMap<GroupBlock, GroupBlockViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(dest => dest.Numbers, opt => opt.MapFrom(src => src.Numbers))
                .ForMember(dest => dest.ExitTime, opt => opt.MapFrom(src => src.ExitTime))
                .ForMember(dest => dest.Athletes, opt => opt.MapFrom(src => src.Athletes))
                .ForMember(dest => dest.ContestId, opt => opt.MapFrom(src => src.ContestId))
                .ForMember(dest => dest.Contest, opt => opt.MapFrom(src => src.Contest))
                .ForMember(dest => dest.GeneratedAthletesListId, opt => opt.MapFrom(src => src.GeneratedAthletesListId))
                .ForMember(dest => dest.GeneratedAthletesList, opt => opt.MapFrom(src => src.GeneratedAthletesList))
                .ForMember(dest => dest.LateAthletesListId, opt => opt.MapFrom(src => src.LateAthletesListId))
                .ForMember(dest => dest.LateAthletesList, opt => opt.MapFrom(src => src.LateAthletesList));
        }
    }
}

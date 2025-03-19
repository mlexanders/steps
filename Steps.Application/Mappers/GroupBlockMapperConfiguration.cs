using AutoMapper;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;

namespace Steps.Application.Mappers;

public class GroupBlockMapperConfiguration : Profile
{
    public GroupBlockMapperConfiguration()
    {
        //GroupBlockViewModel
        CreateMap<GroupBlockViewModel, GroupBlock>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
            .ForMember(x => x.Contest, o => o.Ignore())
            .ForMember(x => x.EndTime, o => o.MapFrom(m => m.EndTime))
            .ForMember(x => x.StartTime, o => o.MapFrom(m => m.StartTime))
            .ForMember(x => x.FinalSchedule, o => o.Ignore())
            .ForMember(x => x.PreSchedule, o => o.Ignore());

        CreateMap<GroupBlock, GroupBlockViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
            .ForMember(x => x.EndTime, o => o.MapFrom(m => m.EndTime))
            .ForMember(x => x.StartTime, o => o.MapFrom(m => m.StartTime))
            .ForMember(x => x.IsHaveFinalBlock, o => o.MapFrom(m => m.FinalSchedule.Count > 0));
        // .ForMember(x => x.PreSchedule, o => o.MapFrom(m => m.PreSchedule));
    }
}
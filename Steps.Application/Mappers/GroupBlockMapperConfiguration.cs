using AutoMapper;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.Schedules.ViewModels;

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
            .ForMember(x => x.Schedule, o => o.Ignore());
        
        CreateMap<GroupBlock, GroupBlockViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
            .ForMember(x => x.EndTime, o => o.MapFrom(m => m.EndTime))
            .ForMember(x => x.StartTime, o => o.MapFrom(m => m.StartTime))
            .ForMember(x => x.ScheduledCells, o => o.MapFrom(m => m.Schedule));
        
        // //GroupBlockCellViewModel
        // CreateMap<ScheduledCell, ScheduledCellViewModel>()
        //     .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
        //     .ForMember(x => x.SequenceNumber, o => o.MapFrom(m => m.SequenceNumber))
        //     .ForMember(x => x.ExitTime, o => o.MapFrom(m => m.ExitTime))
        //     .ForMember(x => x.GroupBlockId, o => o.MapFrom(m => m.GroupBlockId));
    }
}

public class ScheduledCellMapperConfiguration : Profile
{
    public ScheduledCellMapperConfiguration()
    {
        CreateMap<ScheduledCell, ScheduledCellViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.SequenceNumber, o => o.MapFrom(m => m.SequenceNumber))
            .ForMember(x => x.ExitTime, o => o.MapFrom(m => m.ExitTime))
            .ForMember(x => x.GroupBlockId, o => o.MapFrom(m => m.GroupBlockId))
            .ForMember(x => x.AthleteFullName, o => o.MapFrom(m => m.Athlete.FullName))
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId));
        
        
        CreateMap<ScheduledCellViewModel, ScheduledCell>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.SequenceNumber, o => o.MapFrom(m => m.SequenceNumber))
            .ForMember(x => x.ExitTime, o => o.MapFrom(m => m.ExitTime))
            .ForMember(x => x.GroupBlockId, o => o.MapFrom(m => m.GroupBlockId))
            .ForMember(x => x.GroupBlock, o => o.Ignore())
            .ForMember(x => x.Athlete, o => o.Ignore())
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId));
    }
}
using AutoMapper;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.FinalSchedules.ViewModels;
using Steps.Shared.Contracts.Schedules.PreSchedules.ViewModels;

namespace Steps.Application.Mappers;

public class ScheduledCellMapperConfiguration : Profile
{
    public ScheduledCellMapperConfiguration()
    {
        
        //BaseSchedule
        CreateMap<ScheduledCellBase, ScheduledCellViewModelBase>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.SequenceNumber, o => o.MapFrom(m => m.SequenceNumber))
            .ForMember(x => x.ExitTime, o => o.MapFrom(m => m.ExitTime))
            .ForMember(x => x.GroupBlockId, o => o.MapFrom(m => m.GroupBlockId))
            .ForMember(x => x.Athlete, o => o.MapFrom(m => m.Athlete))
            .ForMember(x => x.TeamName, o => o.MapFrom(m => m.Athlete.Team.Name))
            .ForMember(x => x.ClubName, o => o.MapFrom(m => m.Athlete.Team.Club.Name))
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId));
        
        CreateMap<ScheduledCellViewModelBase, ScheduledCellBase>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.SequenceNumber, o => o.MapFrom(m => m.SequenceNumber))
            .ForMember(x => x.ExitTime, o => o.MapFrom(m => m.ExitTime))
            .ForMember(x => x.GroupBlockId, o => o.MapFrom(m => m.GroupBlockId))
            .ForMember(x => x.GroupBlock, o => o.Ignore())
            .ForMember(x => x.Athlete, o => o.Ignore())
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId));


        //FinalScheduled
        CreateMap<FinalScheduledCell, FinalScheduledCellViewModel>()
            .IncludeBase<ScheduledCellBase, ScheduledCellViewModelBase>();
        
        CreateMap<FinalScheduledCellViewModel, FinalScheduledCell>()
            .IncludeBase<ScheduledCellViewModelBase, ScheduledCellBase>();
        
        //PreSchedule
        CreateMap<PreScheduledCell, PreScheduledCellViewModel>()
            .IncludeBase<ScheduledCellBase, ScheduledCellViewModelBase>()
            .ForMember(x => x.IsConfirmed, o => o.MapFrom(m => m.IsConfirmed));

        CreateMap<PreScheduledCellViewModel, PreScheduledCell>()
            .IncludeBase<ScheduledCellViewModelBase, ScheduledCellBase>()
            .ForMember(x => x.IsConfirmed, o => o.MapFrom(m => m.IsConfirmed));


    }
}
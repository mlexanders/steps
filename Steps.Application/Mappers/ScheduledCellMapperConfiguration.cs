using AutoMapper;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules.PreSchedules.ViewModels;

namespace Steps.Application.Mappers;

public class ScheduledCellMapperConfiguration : Profile
{
    public ScheduledCellMapperConfiguration()
    {
        CreateMap<PreScheduledCell, PreScheduledCellViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.SequenceNumber, o => o.MapFrom(m => m.SequenceNumber))
            .ForMember(x => x.ExitTime, o => o.MapFrom(m => m.ExitTime))
            .ForMember(x => x.GroupBlockId, o => o.MapFrom(m => m.GroupBlockId))
            .ForMember(x => x.Athlete, o => o.MapFrom(m => m.Athlete))
            .ForMember(x => x.TeamName, o => o.MapFrom(m => m.Athlete.Team.Name))
            .ForMember(x => x.ClubName, o => o.MapFrom(m => m.Athlete.Team.Club.Name))
            .ForMember(x => x.IsConfirmed, o => o.MapFrom(m => m.IsConfirmed))
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId));
        
        
        CreateMap<PreScheduledCellViewModel, PreScheduledCell>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.SequenceNumber, o => o.MapFrom(m => m.SequenceNumber))
            .ForMember(x => x.ExitTime, o => o.MapFrom(m => m.ExitTime))
            .ForMember(x => x.GroupBlockId, o => o.MapFrom(m => m.GroupBlockId))
            .ForMember(x => x.GroupBlock, o => o.Ignore())
            .ForMember(x => x.Athlete, o => o.Ignore())
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId));
    }
}
using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Application.Mappers;

public class TeamMapperConfiguration : Profile
{
    public TeamMapperConfiguration()
    {
        
        //TeamViewModel
        CreateMap<TeamViewModel, Team>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Address, o => o.MapFrom(m => m.Address))
            .ForMember(x => x.Athletes,o => o.Ignore())
            .ForMember(x => x.ClubId,o => o.MapFrom(m => m.ClubId))
            .ForMember(x => x.Club,o => o.Ignore())
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId))
            .ForMember(x => x.Owner, o => o.MapFrom(m => m.Owner));
        
        CreateMap<Team, TeamViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Address, o => o.MapFrom(m => m.Address))
            .ForMember(x => x.ClubId,o => o.MapFrom(m => m.ClubId))
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId))
            .ForMember(x => x.Owner, o => o.MapFrom(m => m.Owner));

        
        //CreateTeamViewModel
        CreateMap<CreateTeamViewModel, Team>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Address, o => o.MapFrom(m => m.Address))
            .ForMember(x => x.Athletes,o => o.Ignore())
            .ForMember(x => x.ClubId,o => o.MapFrom(m => m.ClubId))
            .ForMember(x => x.Club,o => o.Ignore())
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId))
            .ForMember(x => x.Owner, o => o.Ignore());
        
        //UpdateClubViewModel
        CreateMap<UpdateTeamViewModel, Team>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Address, o => o.MapFrom(m => m.Address))
            .ForMember(x => x.Athletes,o => o.Ignore())
            .ForMember(x => x.ClubId,o => o.MapFrom(m => m.ClubId))
            .ForMember(x => x.Club,o => o.Ignore())
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId))
            .ForMember(x => x.Owner, o => o.Ignore());

        CreateMap<Team, UpdateTeamViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId))
            .ForMember(x => x.Address, o => o.MapFrom(m => m.Address))
            .ForMember(x => x.ClubId, o => o.MapFrom(m => m.ClubId));
    }
}
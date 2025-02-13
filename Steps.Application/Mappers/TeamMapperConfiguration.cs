using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Application.Mappers;

public class TeamMapperConfiguration : Profile
{
    public TeamMapperConfiguration()
    {
        CreateMap<CreateTeamViewModel, Team>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId))
            .ForMember(x => x.Address, o => o.MapFrom(m => m.Address))
            .ForMember(x => x.Owner, o => o.Ignore())
            .ForMember(x => x.Athletes,o => o.Ignore());
    }
}
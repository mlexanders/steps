using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Application.Mappers;

public class ClubsMapperConfiguration : Profile
{
    public ClubsMapperConfiguration()
    {
        CreateMap<CreateClubViewModel, Club>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Teams, o => o.Ignore())
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId))
            .ForMember(x => x.Owner, o => o.Ignore());

        //ClubViewModel
        CreateMap<ClubViewModel, Club>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Teams, o => o.Ignore())
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId))
            .ForMember(x => x.Owner, o => o.Ignore());

        CreateMap<Club, ClubViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId));


        //UpdateClubViewModel
        CreateMap<UpdateClubViewModel, Club>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.Teams, o => o.Ignore())
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId))
            .ForMember(x => x.Owner, o => o.Ignore());

        CreateMap<Club, UpdateClubViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
            .ForMember(x => x.OwnerId, o => o.MapFrom(m => m.OwnerId));
    }
}
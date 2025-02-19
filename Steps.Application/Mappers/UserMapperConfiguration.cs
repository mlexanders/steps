using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Application.Mappers;

public class UserMapperConfiguration : Profile
{
    public UserMapperConfiguration()
    {
        CreateMap<RegistrationViewModel, User>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.PasswordHash, o => o.Ignore())
            .ForMember(x => x.Login, o => o.MapFrom(m => m.Login))
            .ForMember(x => x.Role, o => o.MapFrom(m => m.Role));

        CreateMap<User, RegistrationViewModel>()
            .ForMember(x => x.Password, o => o.Ignore())
            .ForMember(x => x.PasswordConfirm, o => o.Ignore())
            .ForMember(x => x.Login, o => o.MapFrom(m => m.Login))
            .ForMember(x => x.Name, o => o.MapFrom(m => m.Login));
        
        CreateMap<UserViewModel, User>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.PasswordHash, o => o.Ignore())
            .ForMember(x => x.Login, o => o.MapFrom(m => m.Login))
            .ForMember(x => x.Role, o => o.MapFrom(m => m.Role));

        CreateMap<User, UserViewModel>()
            .ForMember(x => x.Login, o => o.MapFrom(m => m.Login))
            .ForMember(x => x.Role, o => o.MapFrom(m => m.Role))
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id));
    }
}
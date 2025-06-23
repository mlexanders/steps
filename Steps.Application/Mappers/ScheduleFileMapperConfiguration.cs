using AutoMapper;
using Steps.Domain.Entities;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;

namespace Steps.Application.Mappers
{
    public class ScheduleFileMapperConfiguration : Profile
    {
        public ScheduleFileMapperConfiguration()
        {
            //ScheduleFileViewModel
            CreateMap<ScheduleFileViewModel, ScheduleFile>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.FileName, o => o.MapFrom(m => m.FileName))
                .ForMember(x => x.Data, o => o.MapFrom(m => m.Data));

            CreateMap<ScheduleFile, ScheduleFileViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.FileName, o => o.MapFrom(m => m.FileName))
                .ForMember(x => x.Data, o => o.MapFrom(m => m.Data));
        }
    }
}

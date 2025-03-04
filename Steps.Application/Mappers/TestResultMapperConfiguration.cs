using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Application.Mappers;

public class TestResultMapperConfiguration : Profile
{
     public TestResultMapperConfiguration()
    {
        //TeamViewModel
        CreateMap<TestResultViewModel, TestResult>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId))
            .ForMember(x => x.Athlete, o => o.Ignore())
            
            .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
            .ForMember(x => x.Contest, o => o.Ignore())
            
            .ForMember(x => x.JudgeId, o => o.Ignore())
            .ForMember(x => x.Judge, o => o.Ignore())
            
            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores));
        
        CreateMap<TestResult, TestResultViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId))
            .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores));

        
        //CreateTeamViewModel
        CreateMap<CreateTestResultViewModel, TestResult>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId))
            .ForMember(x => x.Athlete, o => o.Ignore())
            
            .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
            .ForMember(x => x.Contest, o => o.Ignore())
            
            .ForMember(x => x.JudgeId, o => o.Ignore())
            .ForMember(x => x.Judge, o => o.Ignore())
            
            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores));
        
        
        //UpdateClubViewModel
        CreateMap<UpdateTestResultViewModel, TestResult>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.AthleteId, o => o.Ignore())
            .ForMember(x => x.Athlete, o => o.Ignore())
            
            .ForMember(x => x.ContestId, o => o.Ignore())
            .ForMember(x => x.Contest, o => o.Ignore())
            
            .ForMember(x => x.JudgeId, o => o.Ignore())
            .ForMember(x => x.Judge, o => o.Ignore())
            
            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores));
        
        CreateMap<TestResult, UpdateTestResultViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores));
    }
}
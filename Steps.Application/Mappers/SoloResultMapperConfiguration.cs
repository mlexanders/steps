using AutoMapper;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Application.Mappers;

public class SoloResultMapperConfiguration : Profile
{
    public SoloResultMapperConfiguration()
    {
        // SoloResultViewModel
        CreateMap<SoloResultViewModel, SoloResult>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId))
            .ForMember(x => x.Athlete, o => o.MapFrom(m => m.Athlete))
            
            .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
            .ForMember(x => x.Contest, o => o.Ignore())
            
            .ForMember(x => x.JudgeId, o => o.Ignore())
            .ForMember(x => x.Judge, o => o.Ignore())
            
            .ForMember(x => x.Rating, o => o.Ignore())
            .ForMember(x => x.RatingId, o => o.MapFrom(m => m.ScoredDegreeId))

            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores))
            .ForMember(x => x.DanceTechniqueComment, o => o.MapFrom(m => m.DanceTechniqueComment))
            .ForMember(x => x.ElementsTechniqueComment, o => o.MapFrom(m => m.ElementsTechniqueComment))
            .ForMember(x => x.ChoreographyComment, o => o.MapFrom(m => m.ChoreographyComment))
            .ForMember(x => x.CommunicationComment, o => o.MapFrom(m => m.CommunicationComment))
            .ForMember(x => x.GeneralImpressionComment, o => o.MapFrom(m => m.GeneralImpressionComment));
        
        CreateMap<SoloResult, SoloResultViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId))
            .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
            .ForMember(x => x.ScoredDegreeId, o => o.MapFrom(m => m.RatingId))
            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores))
            .ForMember(x => x.DanceTechniqueComment, o => o.MapFrom(m => m.DanceTechniqueComment))
            .ForMember(x => x.ElementsTechniqueComment, o => o.MapFrom(m => m.ElementsTechniqueComment))
            .ForMember(x => x.ChoreographyComment, o => o.MapFrom(m => m.ChoreographyComment))
            .ForMember(x => x.CommunicationComment, o => o.MapFrom(m => m.CommunicationComment))
            .ForMember(x => x.GeneralImpressionComment, o => o.MapFrom(m => m.GeneralImpressionComment))
            .ForMember(x => x.Athlete, o => o.MapFrom(m => m.Athlete));

        
        // CreateSoloResultViewModel
        CreateMap<CreateSoloResultViewModel, SoloResult>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.AthleteId, o => o.MapFrom(m => m.AthleteId))
            .ForMember(x => x.Athlete, o => o.Ignore())
            
            .ForMember(x => x.ContestId, o => o.MapFrom(m => m.ContestId))
            .ForMember(x => x.Contest, o => o.Ignore())
            
            .ForMember(x => x.Rating, o => o.Ignore())
            .ForMember(x => x.RatingId, o => o.Ignore())
            
            .ForMember(x => x.JudgeId, o => o.Ignore())
            .ForMember(x => x.Judge, o => o.Ignore())
            
            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores))
            .ForMember(x => x.DanceTechniqueComment, o => o.MapFrom(m => m.DanceTechniqueComment))
            .ForMember(x => x.ElementsTechniqueComment, o => o.MapFrom(m => m.ElementsTechniqueComment))
            .ForMember(x => x.ChoreographyComment, o => o.MapFrom(m => m.ChoreographyComment))
            .ForMember(x => x.CommunicationComment, o => o.MapFrom(m => m.CommunicationComment))
            .ForMember(x => x.GeneralImpressionComment, o => o.MapFrom(m => m.GeneralImpressionComment));
        
        
        // UpdateSoloResultViewModel
        CreateMap<UpdateSoloResultViewModel, SoloResult>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.AthleteId, o => o.Ignore())
            .ForMember(x => x.Athlete, o => o.Ignore())
            
            .ForMember(x => x.Rating, o => o.Ignore())
            .ForMember(x => x.RatingId, o => o.Ignore())
            
            .ForMember(x => x.ContestId, o => o.Ignore())
            .ForMember(x => x.Contest, o => o.Ignore())
            
            .ForMember(x => x.JudgeId, o => o.Ignore())
            .ForMember(x => x.Judge, o => o.Ignore())
            
            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores))
            .ForMember(x => x.DanceTechniqueComment, o => o.MapFrom(m => m.DanceTechniqueComment))
            .ForMember(x => x.ElementsTechniqueComment, o => o.MapFrom(m => m.ElementsTechniqueComment))
            .ForMember(x => x.ChoreographyComment, o => o.MapFrom(m => m.ChoreographyComment))
            .ForMember(x => x.CommunicationComment, o => o.MapFrom(m => m.CommunicationComment))
            .ForMember(x => x.GeneralImpressionComment, o => o.MapFrom(m => m.GeneralImpressionComment));
        
        CreateMap<SoloResult, UpdateSoloResultViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
            .ForMember(x => x.Scores, o => o.MapFrom(m => m.Scores))
            .ForMember(x => x.DanceTechniqueComment, o => o.MapFrom(m => m.DanceTechniqueComment))
            .ForMember(x => x.ElementsTechniqueComment, o => o.MapFrom(m => m.ElementsTechniqueComment))
            .ForMember(x => x.ChoreographyComment, o => o.MapFrom(m => m.ChoreographyComment))
            .ForMember(x => x.CommunicationComment, o => o.MapFrom(m => m.CommunicationComment))
            .ForMember(x => x.GeneralImpressionComment, o => o.MapFrom(m => m.GeneralImpressionComment));
    }
}

using Steps.Domain.Entities;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Shared.Contracts.Athletes;
public interface IAthleteService : ICrudService<Athlete, AthleteViewModel, CreateAthleteViewModel, UpdateAthleteViewModel>;

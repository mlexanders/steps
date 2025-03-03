using Steps.Domain.Entities;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Shared.Contracts.Clubs;

public interface IClubsService : ICrudService<Club, ClubViewModel, CreateClubViewModel, UpdateClubViewModel>;
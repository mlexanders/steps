using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Shared.Contracts.Clubs;

public interface IClubsService : ICrudService<ClubViewModel, CreateClubViewModel, UpdateClubViewModel>;
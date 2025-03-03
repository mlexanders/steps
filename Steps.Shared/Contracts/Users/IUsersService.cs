using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Shared.Contracts.Users;

public interface IUsersService : ICrudService<User, UserViewModel, CreateUserViewModel, UpdateUserViewModel>;
using Steps.Domain.Base;

namespace Steps.Application.Interfaces.Base;

public interface IUserManager<TUser> where TUser : IUser
{
    Task<Guid> CreateAsync(TUser user, string password);
    Task<IUser> Login(string email, string password);
    Task<IUser?> FindByEmailAsync(string email);
    Task<string> GenerateEmailConfirmationTokenAsync(TUser user);
    Task ConfirmEmailAsync(TUser user, string token, string newPassword);
    Task<string> GenerateChangeEmailTokenAsync(TUser user, string newEmail);
    Task ChangeEmailAsync(TUser user, string token, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(TUser user);
    Task ResetPasswordAsync(TUser user, string token, string newPassword);
}
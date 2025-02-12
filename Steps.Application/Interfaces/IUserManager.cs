namespace Steps.Application.Interfaces;

public interface IUserManager<TUser>
{
    Task<Guid> CreateAsync(TUser user, string password);
    Task<TUser> Login(string email, string password);
    Task<TUser?> FindByEmailAsync(string email);
    Task<string> GenerateEmailConfirmationTokenAsync(TUser user);
    Task ConfirmEmailAsync(TUser user, string token, string newPassword);
    Task<string> GenerateChangeEmailTokenAsync(TUser user, string newEmail);
    Task ChangeEmailAsync(TUser user, string token, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(TUser user);
    Task ResetPasswordAsync(TUser user, string token, string newPassword);
}
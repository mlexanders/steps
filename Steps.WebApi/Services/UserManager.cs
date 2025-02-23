using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;
using Steps.Shared.Exceptions;

namespace Steps.Services.WebApi.Services;

public class UserManager : IUserManager<User>
{
    private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserManager(IPasswordHasher passwordHasher,
        IUnitOfWork<ApplicationDbContext> unitOfWork)
    {
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateAsync(User model, string password)
    {
        model.PasswordHash = _passwordHasher.HashPassword(password);
        var repository = _unitOfWork.GetRepository<User>();

        var userIsExist = await repository.ExistsAsync(u => u.Login.Equals(model.Login));

        if (userIsExist)
            throw new StepsBusinessException("Пользователь с таким email уже зарегистрирован");

        var entry = await repository.InsertAsync(model);
        await _unitOfWork.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task<IUser> Login(string email, string password)
    {
        var user = await _unitOfWork.GetRepository<User>()
            .GetFirstOrDefaultAsync(
                predicate: u => u.Login.Equals(email),
                trackingType: TrackingType.NoTracking);

        if (user == null) throw new AppUserNotFoundException(email);

        var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);
        return result is PasswordVerificationResult.Success ? user : throw new AppInvalidCredentialsException();
    }

    public async Task<IUser?> FindByEmailAsync(string email)
    {
        var user = await _unitOfWork.GetRepository<User>()
            .GetFirstOrDefaultAsync(
                predicate: x => x.Login.Equals(email),
                trackingType: TrackingType.NoTracking);

        return user;
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task ConfirmEmailAsync(User user, string token, string newPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail)
    {
        throw new NotImplementedException();
    }

    public async Task ChangeEmailAsync(User user, string token, string newPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task ResetPasswordAsync(User user, string token, string newPassword)
    {
        throw new NotImplementedException();
    }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;
using Steps.Shared.Exceptions;

namespace Steps.Services.WebApi.Services;

public class SignInManager : SecurityService, ISignInManager
{
    private readonly IUserManager<User> _userManager;

    public SignInManager(IUserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<bool> IsSignedIn()
    {
        return HttpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public async Task SignOut()
    {
        await HttpContextAccessor!.HttpContext!.SignOutAsync();
    }

    public async Task<IUser> SignInAsync(string email, string password)
    {
        var user = await _userManager.Login(email, password) ?? throw new AppUserNotFoundException(email);

        await HttpContextAccessor!.HttpContext!.SignInAsync(
            AppData.Identity.AuthenticationType,
            new ClaimsPrincipal(CreateClaimsFrom(user)),
            new AuthenticationProperties
            {
                IsPersistent = true
            });

        return user;
    }
}
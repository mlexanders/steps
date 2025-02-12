using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;
using Steps.Shared.Exceptions;

namespace Steps.Services.WebApi.Services;

public class SignInManager : ISignInManager
{
    private readonly IUserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public SignInManager(IUserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<bool> IsSignedIn()
    {
        return _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public async Task SignOut()
    {
        await _httpContextAccessor!.HttpContext!.SignOutAsync();
    }

    public async Task SignInAsync(string email, string password)
    {
        var user = await _userManager.Login(email, password) ?? throw new UserNotFoundException(email);

        await _httpContextAccessor!.HttpContext!.SignInAsync(
            AppData.Identity.AuthenticationType,
            new ClaimsPrincipal(CreateClaimsFrom(user)),
            new AuthenticationProperties
            {
                IsPersistent = true
            });
    }

    private static ClaimsIdentity CreateClaimsFrom(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Email, user.Login),
            new(ClaimTypes.Role, user.Role.ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(claims, AppData.Identity.AuthenticationType);
        return claimsIdentity;
    }
}
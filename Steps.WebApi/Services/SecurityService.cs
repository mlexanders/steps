using System.Security.Claims;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;
using Steps.Shared.Exceptions;

namespace Steps.Services.WebApi.Services;

public class SecurityService : ISecurityService
{
    protected readonly IHttpContextAccessor HttpContextAccessor;

    public SecurityService(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    public Task<User> GetCurrentUser()
    {
        var userClaims = HttpContextAccessor.HttpContext?.User;
        if (userClaims == null || !userClaims.Identity?.IsAuthenticated == true)
        {
            throw new AppUnauthorizedAccessException();
        }

        var user = new User
        {
            Login = userClaims.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            Id = Guid.TryParse(userClaims.FindFirstValue(ClaimTypes.Sid), out Guid id) ? id : Guid.Empty,
            Role = Enum.TryParse(userClaims.FindFirstValue(ClaimTypes.Role), out Role role) ? role : Role.Undefined,
        };

        return Task.FromResult<User>(user);
    }

    protected static ClaimsIdentity CreateClaimsFrom(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Sid, user.Id.ToString()),
            new(ClaimTypes.Email, user.Login),
            new(ClaimTypes.Role, user.Role.ToString()),
        };

        return new ClaimsIdentity(claims, AppData.Identity.AuthenticationType);
    }
}
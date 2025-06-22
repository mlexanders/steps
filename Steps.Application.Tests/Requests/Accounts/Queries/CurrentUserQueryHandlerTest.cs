using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Steps.Application.Interfaces.Base;
using Steps.Application.Requests.Accounts.Queries;
using Steps.Application.Tests;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Xunit;

namespace Steps.Application.Tests.Requests.Accounts.Queries;

[TestSubject(typeof(CurrentUserQueryHandler))]
public class CurrentUserQueryHandlerTest
{
    private readonly IServiceScope _scope;

    public CurrentUserQueryHandlerTest()
    {
        var builder = WebApplication.CreateBuilder();
        builder.AddApplication();

        builder.Services.AddTransient<ISecurityService, SecurityServiceMock>();
        builder.Services.AddTransient<IUserManager<User>, UserManagerMock>();
        builder.Services.AddTransient<ISignInManager, SignInManagerMock>();

        var app = builder.Build();
        app.UseApplication();

        _scope = app.Services.CreateScope();
    }

    [Fact]
    public async Task Handle_AuthenticatedUser_ReturnsUserData()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var query = new CurrentUserQuery();

        // Act
        var result = await mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("test@example.com", result.Value.Login);
        Assert.Equal(Role.Organizer, result.Value.Role);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
    }

    [Fact]
    public async Task Handle_UserDataMapping_ReturnsCorrectViewModel()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var query = new CurrentUserQuery();

        // Act
        var result = await mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        var user = result.Value;
        Assert.IsType<UserViewModel>(user);
        Assert.Equal("test@example.com", user.Login);
        Assert.Equal(Role.Organizer, user.Role);
    }
} 
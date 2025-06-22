using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Steps.Application.Exceptions;
using Steps.Application.Requests.Users.Commands;
using Steps.Application.Tests;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users.ViewModels;
using Steps.Shared.Exceptions;
using Xunit;

namespace Steps.Application.Tests.Requests.Users.Commands;

[TestSubject(typeof(CreateUserCommandHandler))]
public class CreateUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task Handle_ValidUserData_ReturnsSuccessResult()
    {
        // Arrange
        await SeedTestData();
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createUserModel = new CreateUserViewModel 
        { 
            Login = "newuser@example.com"
        };
        var command = new CreateUserCommand(createUserModel);

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("newuser@example.com", result.Value.Login);
        Assert.Equal("Пользователь создан", result.Message);
    }

    [Fact]
    public async Task Handle_UserAlreadyExists_ThrowsAppHandledException()
    {
        // Arrange
        await SeedTestData();
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createUserModel = new CreateUserViewModel 
        { 
            Login = "existinguser@example.com"
        };
        var command = new CreateUserCommand(createUserModel);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.IsType<StepsBusinessException>(exception.InnerException);
        Assert.Contains("Пользователь с таким email уже зарегистрирован", exception.Message);
    }

    [Fact]
    public async Task Handle_EmptyLogin_ThrowsAppHandledException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createUserModel = new CreateUserViewModel 
        { 
            Login = null!
        };
        var command = new CreateUserCommand(createUserModel);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.Contains("Заполните логин", exception.Message);
    }

    [Fact]
    public async Task Handle_NullModel_ThrowsAppHandledException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = new CreateUserCommand(null!);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.Contains("Все поля должны быть заполнены", exception.Message);
    }

    [Fact]
    public async Task CanAccess_OrganizerRole_ReturnsTrue()
    {
        // Arrange
        await SeedTestData();
        var createUserModel = new CreateUserViewModel { Login = "test@example.com" };
        var command = new CreateUserCommand(createUserModel);
        var organizerUser = new User { Role = Role.Organizer };

        // Act
        var canAccess = await command.CanAccess(organizerUser);

        // Assert
        Assert.True(canAccess);
    }

    [Fact]
    public async Task CanAccess_NonOrganizerRole_ReturnsFalse()
    {
        // Arrange
        var createUserModel = new CreateUserViewModel { Login = "test@example.com" };
        var command = new CreateUserCommand(createUserModel);
        var regularUser = new User { Role = Role.User };

        // Act
        var canAccess = await command.CanAccess(regularUser);

        // Assert
        Assert.False(canAccess);
    }
} 
using System;
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
using Steps.Shared.Contracts.Users.ViewModels;
using Steps.Shared.Exceptions;
using Xunit;

namespace Steps.Application.Tests.Requests.Users.Commands;

[TestSubject(typeof(UpdateUserCommandHandler))]
public class UpdateUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task Handle_ValidUserData_ReturnsSuccessResult()
    {
        // Arrange
        await SeedTestData();
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var updateUserModel = new UpdateUserViewModel 
        { 
            Id = Guid.NewGuid(),
            Login = "updateduser@example.com",
            Role = Role.Judge
        };
        var command = new UpdateUserCommand(updateUserModel);

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(updateUserModel.Id, result.Value);
        Assert.Equal("Данные пользователя обновлены", result.Message);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsStepsBusinessException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var updateUserModel = new UpdateUserViewModel 
        { 
            Id = Guid.NewGuid(),
            Login = "nonexistent@example.com",
            Role = Role.User
        };
        var command = new UpdateUserCommand(updateUserModel);

        // Act & Assert
        await Assert.ThrowsAsync<StepsBusinessException>(() => mediator.Send(command));
    }

    [Fact]
    public async Task Handle_EmptyLogin_ThrowsAppHandledException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var updateUserModel = new UpdateUserViewModel 
        { 
            Id = Guid.NewGuid(),
            Login = "",
            Role = Role.User
        };
        var command = new UpdateUserCommand(updateUserModel);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.Contains("Введите логин", exception.Message);
    }

    [Fact]
    public async Task Handle_NullModel_ThrowsAppHandledException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = new UpdateUserCommand(null!);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.Contains("Model", exception.Message);
    }

    [Fact]
    public async Task Handle_EmptyId_ThrowsAppHandledException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var updateUserModel = new UpdateUserViewModel 
        { 
            Id = Guid.Empty,
            Login = "test@example.com",
            Role = Role.User
        };
        var command = new UpdateUserCommand(updateUserModel);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.Contains("Id", exception.Message);
    }

    [Fact]
    public async Task CanAccess_OrganizerRole_ReturnsTrue()
    {
        // Arrange
        await SeedTestData();
        var updateUserModel = new UpdateUserViewModel 
        { 
            Id = Guid.NewGuid(),
            Login = "test@example.com",
            Role = Role.User
        };
        var command = new UpdateUserCommand(updateUserModel);
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
        var updateUserModel = new UpdateUserViewModel 
        { 
            Id = Guid.NewGuid(),
            Login = "test@example.com",
            Role = Role.User
        };
        var command = new UpdateUserCommand(updateUserModel);
        var regularUser = new User { Role = Role.User };

        // Act
        var canAccess = await command.CanAccess(regularUser);

        // Assert
        Assert.False(canAccess);
    }

    [Fact]
    public async Task Handle_UpdateUserRole_ReturnsSuccessResult()
    {
        // Arrange
        await SeedTestData();
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var updateUserModel = new UpdateUserViewModel 
        { 
            Id = Guid.NewGuid(),
            Login = "user@example.com",
            Role = Role.Counter
        };
        var command = new UpdateUserCommand(updateUserModel);

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(updateUserModel.Id, result.Value);
    }
} 
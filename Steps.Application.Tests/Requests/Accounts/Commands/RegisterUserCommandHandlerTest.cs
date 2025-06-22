using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Steps.Application.Requests.Accounts.Commands;
using Steps.Application.Tests;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Exceptions;
using Xunit;

namespace Steps.Application.Tests.Requests.Accounts.Commands;

[TestSubject(typeof(RegisterUserCommandHandler))]
public class RegisterUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task Handle_ValidRegistrationData_ReturnsSuccessResult()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var registrationModel = new RegistrationViewModel 
        { 
            Name = "Test User",
            Login = "newuser@example.com",
            Password = "password123",
            PasswordConfirm = "password123",
            Role = Role.User
        };
        var command = new RegisterUserCommand(registrationModel);

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
        Assert.Equal("Вы успешно зарегистрированы", result.Message);
    }

    [Fact]
    public async Task Handle_UserAlreadyExists_ThrowsStepsBusinessException()
    {
        // Arrange
        await SeedTestData();
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var registrationModel = new RegistrationViewModel 
        { 
            Name = "Existing User",
            Login = "existinguser@example.com",
            Password = "password123",
            PasswordConfirm = "password123",
            Role = Role.User
        };
        var command = new RegisterUserCommand(registrationModel);

        // Act & Assert
        await Assert.ThrowsAsync<StepsBusinessException>(() => mediator.Send(command));
    }

    [Fact]
    public async Task Handle_PasswordsDoNotMatch_ThrowsValidationException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var registrationModel = new RegistrationViewModel 
        { 
            Name = "Test User",
            Login = "test@example.com",
            Password = "password123",
            PasswordConfirm = "differentpassword",
            Role = Role.User
        };
        var command = new RegisterUserCommand(registrationModel);

        // Act & Assert
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => mediator.Send(command));
    }

    [Fact]
    public async Task Handle_EmptyName_ThrowsValidationException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var registrationModel = new RegistrationViewModel 
        { 
            Name = "",
            Login = "test@example.com",
            Password = "password123",
            PasswordConfirm = "password123",
            Role = Role.User
        };
        var command = new RegisterUserCommand(registrationModel);

        // Act & Assert
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => mediator.Send(command));
    }

    [Fact]
    public async Task Handle_EmptyLogin_ThrowsValidationException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var registrationModel = new RegistrationViewModel 
        { 
            Name = "Test User",
            Login = "",
            Password = "password123",
            PasswordConfirm = "password123",
            Role = Role.User
        };
        var command = new RegisterUserCommand(registrationModel);

        // Act & Assert
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => mediator.Send(command));
    }

    [Fact]
    public async Task Handle_WeakPassword_ThrowsValidationException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var registrationModel = new RegistrationViewModel 
        { 
            Name = "Test User",
            Login = "test@example.com",
            Password = "123",
            PasswordConfirm = "123",
            Role = Role.User
        };
        var command = new RegisterUserCommand(registrationModel);

        // Act & Assert
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => mediator.Send(command));
    }

    [Fact]
    public async Task Handle_InvalidEmailFormat_ThrowsValidationException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var registrationModel = new RegistrationViewModel 
        { 
            Name = "Test User",
            Login = "invalid-email",
            Password = "password123",
            PasswordConfirm = "password123",
            Role = Role.User
        };
        var command = new RegisterUserCommand(registrationModel);

        // Act & Assert
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => mediator.Send(command));
    }

    [Fact]
    public async Task Handle_RegistrationWithOrganizerRole_ReturnsSuccessResult()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var registrationModel = new RegistrationViewModel 
        { 
            Name = "Organizer User",
            Login = "organizer@example.com",
            Password = "password123",
            PasswordConfirm = "password123",
            Role = Role.Organizer
        };
        var command = new RegisterUserCommand(registrationModel);

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
    }
} 
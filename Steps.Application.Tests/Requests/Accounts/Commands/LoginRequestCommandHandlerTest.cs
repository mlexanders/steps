using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Steps.Application.Exceptions;
using Steps.Application.Requests.Accounts.Commands;
using Steps.Application.Tests;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Exceptions;
using Xunit;

namespace Steps.Application.Tests.Requests.Accounts.Commands;

[TestSubject(typeof(LoginRequestCommandHandler))]
public class LoginRequestCommandHandlerTest : TestBase
{
    [Fact]
    public async Task Handle_ValidCredentials_ReturnsSuccessResult()
    {
        // Arrange
        await SeedTestData();
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var loginModel = new LoginViewModel 
        { 
            Login = "test@example.com", 
            Password = "password123" 
        };
        var command = new LoginRequestCommand(loginModel);

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("test@example.com", result.Value.Login);
        Assert.Equal("Вход выполнен успешно", result.Message);
    }

    [Fact]
    public async Task Handle_InvalidCredentials_ThrowsAppHandledException()
    {
        // Arrange
        await SeedTestData();
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var loginModel = new LoginViewModel 
        { 
            Login = "invalid@example.com", 
            Password = "wrongpassword" 
        };
        var command = new LoginRequestCommand(loginModel);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.IsType<AppInvalidCredentialsException>(exception.InnerException);
        Assert.Contains("Неверный логин или пароль", exception.Message);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsAppHandledException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var loginModel = new LoginViewModel 
        { 
            Login = "nonexistent@example.com", 
            Password = "password123" 
        };
        var command = new LoginRequestCommand(loginModel);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.IsType<AppUserNotFoundException>(exception.InnerException);
        Assert.Contains("Пользователь c email: nonexistent@example.com не найден", exception.Message);
    }

    [Fact]
    public async Task Handle_EmptyCredentials_ThrowsAppHandledException()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var loginModel = new LoginViewModel 
        { 
            Login = "", 
            Password = "" 
        };
        var command = new LoginRequestCommand(loginModel);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.Contains("Введите логин", exception.Message);
        Assert.Contains("Введите пароль", exception.Message);
    }
} 
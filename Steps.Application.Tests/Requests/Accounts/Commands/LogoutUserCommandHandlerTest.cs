using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Steps.Application.Requests.Accounts.Commands;
using Steps.Application.Tests;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Xunit;

namespace Steps.Application.Tests.Requests.Accounts.Commands;

[TestSubject(typeof(LogoutUserCommandHandler))]
public class LogoutUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task Handle_ValidLogout_ReturnsSuccessResult()
    {
        // Arrange
        await SeedTestData();
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = new LogoutUserCommand();

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Выход выполнен успешно", result.Message);
    }

    [Fact]
    public async Task Handle_LogoutWhenNotAuthenticated_ReturnsSuccessResult()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = new LogoutUserCommand();

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        // Даже если пользователь не был аутентифицирован, выход должен быть успешным
    }
} 
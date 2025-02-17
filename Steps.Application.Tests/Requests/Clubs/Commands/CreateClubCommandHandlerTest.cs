using Calabonga.UnitOfWork;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Steps.Application.Interfaces;
using Steps.Application.Requests.Clubs.Commands;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;
using Steps.Shared.Contracts.Clubs.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Steps.Application.Exceptions;

namespace Steps.Application.Tests.Requests.Clubs.Commands;

[TestSubject(typeof(CreateClubCommandHandler))]
public class CreateClubCommandIntegrationTest
{
    private readonly IServiceScope _scope;

    public CreateClubCommandIntegrationTest()
    {
        var builder = WebApplication.CreateBuilder();
        builder.AddApplication();

        builder.Services.AddTransient<ISecurityService, SecurityServiceMock>();
        builder.Services.AddTransient<IUserManager<User>, UserManagerMock>();
        builder.Services.AddTransient<ISignInManager, SignInManagerMock>();

        var services = builder.Services;

        // Добавляем InMemory базу данных
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("TestDb"));

        services.AddUnitOfWork<ApplicationDbContext>();

        var app = builder.Build();
        app.UseApplication();

        _scope = app.Services.CreateScope();
    }

    [Fact]
    public async Task GetEmployees_WhenCalled_ReturnsEmployeeListAsync()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createClubModel = new CreateClubViewModel { Name = "Test Club", OwnerId = Guid.NewGuid() };
        var command = new CreateClubCommand(createClubModel);

        // Act
        try
        {
            var result = await mediator.Send(command);
        }
        catch (AppHandledException e)
        {
            // assert
            Assert.False(e.Result.Success);
        }
    }
}
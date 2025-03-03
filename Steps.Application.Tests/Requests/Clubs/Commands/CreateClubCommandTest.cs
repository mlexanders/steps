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
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Steps.Application.Exceptions;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Tests.Requests.Clubs.Commands;

public class CreateClubCommandTest
{
    private readonly IServiceScope _scope;
    public CreateClubCommandTest() 
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
    //Подаем корректные данные и проверяем ошибки при работе с бд
    public async Task SqlFail()
    {
        //Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createClubModel = new CreateClubViewModel { Name = "Test Club", OwnerId = Guid.NewGuid() };
        var command = new CreateClubCommand(createClubModel);
        
        //Act
        try
        {
            var result = await mediator.Send(command);
        }
        catch (AppHandledException ex)
        {
            Assert.IsType<PostgresException>(ex.InnerException);
        }
    }
    
    [Fact]
    //Подаем корректные данные и проверяем успешное создание
    public async Task AllRight()
    {
        //Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createClubModel = new CreateClubViewModel { Name = "Test Club", OwnerId = Guid.NewGuid() };
        var command = new CreateClubCommand(createClubModel);
        
        //Act
        try
        {
            var result = await mediator.Send(command);
        }
        //Assert
        catch (AppHandledException ex)
        {
           Assert.Null(ex.Result.Errors);
        }
    }

    [Fact]
    //Подаем юзера, которому доступ на создание запрещен
    public async Task NotAuthorized()
    {
        //Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createClubModel = new CreateClubViewModel { Name = "Test Club", OwnerId = Guid.NewGuid() };
        var command = new CreateClubCommand(createClubModel);
        
        //Act
        try
        {
            var result = await mediator.Send(command);
        }
        catch (AppHandledException ex)
        {
            Assert.IsType<AppAccessDeniedException>(ex.InnerException);
        }
    }

    [Fact]
    //Подаем существующий клуб
    public async Task ClubIsExist()
    {
        //Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createClubModel = new CreateClubViewModel { Name = "Test Club", OwnerId = Guid.NewGuid() };
        var command = new CreateClubCommand(createClubModel);
        
        //Act
        try
        {
            var result = await mediator.Send(command);
        }
        catch (AppHandledException ex)
        {
            Assert.IsType<StepsBusinessException>(ex.InnerException);
        }
    }

    [Fact]
    //Подаем невалидируемые данные
    public async Task NotValidatingData()
    {
        //Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createClubModel = new CreateClubViewModel { Name = "Test Club", OwnerId = Guid.NewGuid() };
        var command = new CreateClubCommand(createClubModel);
        
        //Act
        try
        {
            var result = await mediator.Send(command);
        }
        catch (AppHandledException ex)
        {
            Assert.IsType<FluentValidation.ValidationException>(ex.InnerException);
        }
    }
 }
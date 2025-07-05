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
using FluentValidation;

namespace Steps.Application.Tests.Requests.Clubs.Commands;

public class CreateClubCommandTest : TestBase
{
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
        
        //Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.IsType<AppAccessDeniedException>(exception.InnerException);
        Assert.Contains("Доступ запрещен", exception.Message);
    }

    [Fact]
    //Подаем существующий клуб
    public async Task ClubIsExist()
    {
        //Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createClubModel = new CreateClubViewModel { Name = "Test Club", OwnerId = Guid.NewGuid() };
        var command = new CreateClubCommand(createClubModel);
        
        //Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.IsType<StepsBusinessException>(exception.InnerException);
        Assert.Contains("Клуб с таким названием уже существует", exception.Message);
    }

    [Fact]
    //Подаем невалидируемые данные
    public async Task NotValidatingData()
    {
        //Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createClubModel = new CreateClubViewModel { Name = "", OwnerId = Guid.Empty };
        var command = new CreateClubCommand(createClubModel);
        
        //Act & Assert
        var exception = await Assert.ThrowsAsync<AppHandledException>(() => mediator.Send(command));
        Assert.IsType<FluentValidation.ValidationException>(exception.InnerException);
        Assert.Contains("Заполните форму", exception.Message);
    }
 }
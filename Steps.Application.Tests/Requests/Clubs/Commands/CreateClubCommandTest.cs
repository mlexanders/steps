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
    public async Task Create_Club_Test()
    {
        //Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var createClubModel = new CreateClubViewModel { Name = "Test Club", OwnerId = Guid.NewGuid() };
        var command = new CreateClubCommand(createClubModel);
        
        //Act
        try
        {
            var result = await mediator.Send(command);
            Assert.True(result.IsSuccess);
            Assert.Null(result.Value);
        }
        catch (AppHandledException ex)
        {
            switch (ex.InnerException)
            {
                case AppAccessDeniedException _:
                    Assert.Equal(typeof(AppAccessDeniedException), ex.InnerException.GetType());
                    return;

                case StepsBusinessException _:
                    Assert.Equal(typeof(StepsBusinessException), ex.InnerException.GetType());
                    return;

                case PostgresException _:
                    Assert.Equal(typeof(PostgresException), ex.InnerException.GetType());
                    return;
                
                case FluentValidation.ValidationException _:
                    Assert.Equal(typeof(FluentValidation.ValidationException), ex.InnerException.GetType());
                    return;

                default:
                    Assert.False(ex.Result.IsSuccess);
                    break;
            }
        }
    }
 }
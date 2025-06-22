using System;
using System.Threading.Tasks;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Steps.Application.Events.Base;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Application.Tests;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;
using Steps.Shared;

namespace Steps.Application.Tests;

/// <summary>
/// Базовый класс для тестов с InMemory базой данных
/// </summary>
public abstract class TestBase : IDisposable
{
    protected readonly IServiceScope _scope;
    protected readonly ApplicationDbContext _context;

    protected TestBase()
    {
        var builder = WebApplication.CreateBuilder();
        
        // Добавляем InMemory базу данных
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}"));

        // Добавляем UnitOfWork
        builder.Services.AddUnitOfWork<ApplicationDbContext>();

        // Добавляем Application слой
        builder.AddApplication();

        // Регистрируем моки для сервисов, которые не нужны в тестах
        builder.Services.AddTransient<ISecurityService, SecurityServiceMock>();
        builder.Services.AddTransient<IUserManager<User>, UserManagerMock>();
        builder.Services.AddTransient<ISignInManager, SignInManagerMock>();
        builder.Services.AddTransient<IApplicationEventPublisher, ApplicationEventPublisherMock>();
        builder.Services.AddTransient<INotificationService, NotificationServiceMock>();

        var app = builder.Build();
        app.UseApplication();

        _scope = app.Services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Создаем базу данных
        _context.Database.EnsureCreated();
    }

    /// <summary>
    /// Очищает базу данных после каждого теста
    /// </summary>
    protected async Task CleanupDatabase()
    {
        _context.Users.RemoveRange(_context.Users);
        _context.Contests.RemoveRange(_context.Contests);
        _context.Teams.RemoveRange(_context.Teams);
        _context.Athletes.RemoveRange(_context.Athletes);
        _context.Entries.RemoveRange(_context.Entries);
        _context.GroupBlocks.RemoveRange(_context.GroupBlocks);
        _context.PreScheduledCells.RemoveRange(_context.PreScheduledCells);
        _context.FinalScheduledCells.RemoveRange(_context.FinalScheduledCells);
        _context.ScheduleFiles.RemoveRange(_context.ScheduleFiles);
        
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Добавляет тестовые данные в базу
    /// </summary>
    protected async Task SeedTestData()
    {
        // Добавляем тестового пользователя
        var testUser = new User
        {
            Id = Guid.NewGuid(),
            Login = "test@example.com",
            Role = Role.Organizer,
            PasswordHash = "hashed_password"
        };
        
        _context.Users.Add(testUser);
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
        _scope?.Dispose();
    }
} 
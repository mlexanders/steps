# Тесты для Steps.Application

Этот проект содержит unit-тесты для команд и обработчиков аутентификации и управления пользователями.

## Структура проекта

```
Steps.Application.Tests/
├── README.md                           # Эта документация
├── Requests/                           # Тесты для запросов (команды и запросы)
│   ├── Accounts/                       # Тесты аутентификации
│   │   ├── Commands/                   # Команды аутентификации
│   │   │   ├── LoginRequestCommandHandlerTest.cs
│   │   │   ├── LogoutUserCommandHandlerTest.cs
│   │   │   └── RegisterUserCommandHandlerTest.cs
│   │   └── Queries/                    # Запросы аутентификации
│   │       └── CurrentUserQueryHandlerTest.cs
│   └── Users/                          # Тесты управления пользователями
│       └── Commands/                   # Команды управления пользователями
│           ├── CreateUserCommandHandlerTest.cs
│           └── UpdateUserCommandHandlerTest.cs
├── UserManagerMock.cs                  # Мок для IUserManager
├── SecurityServiceMock.cs              # Мок для ISecurityService
└── SignInManagerMock.cs                # Мок для ISignInManager
```

## Назначение тестов

### 🎯 Цели тестирования

1. **Проверка бизнес-логики** - убедиться, что команды и обработчики работают корректно
2. **Валидация данных** - проверить, что некорректные данные отклоняются
3. **Проверка прав доступа** - убедиться, что только авторизованные пользователи могут выполнять операции
4. **Обработка ошибок** - проверить корректную обработку исключительных ситуаций
5. **Регрессионное тестирование** - предотвратить появление ошибок при рефакторинге

### 📋 Покрываемые сценарии

#### Аутентификация
- ✅ Успешный вход в систему
- ❌ Неверные учетные данные
- ❌ Пользователь не найден
- ❌ Некорректные данные (валидация)
- ✅ Успешный выход из системы
- ✅ Получение данных текущего пользователя
- ✅ Регистрация нового пользователя

#### Управление пользователями
- ✅ Создание нового пользователя
- ❌ Попытка создать существующего пользователя
- ✅ Обновление пользователя
- ❌ Обновление несуществующего пользователя
- 🔒 Проверка прав доступа (только Organizer)

## Запуск тестов

### Требования
- .NET 8.0 или выше
- xUnit
- JetBrains Annotations

### Команды для запуска

```bash
# Запуск всех тестов
dotnet test

# Запуск тестов с подробным выводом
dotnet test --verbosity normal

# Запуск конкретного теста
dotnet test --filter "FullyQualifiedName~LoginRequestCommandHandlerTest"

# Запуск тестов с покрытием кода (если настроен)
dotnet test --collect:"XPlat Code Coverage"
```

## Моки и их назначение

### UserManagerMock
Симулирует работу с пользователями в базе данных:
- Создание пользователей
- Обновление пользователей
- Аутентификация
- Поиск пользователей

**Специальные сценарии:**
- `existinguser@example.com` - вызывает исключение при создании
- `nonexistent@example.com` - вызывает исключение при поиске/обновлении
- `invalid@example.com` - вызывает исключение при аутентификации

### SecurityServiceMock
Возвращает тестового пользователя с ролью `Organizer` для проверки прав доступа.

### SignInManagerMock
Симулирует операции входа и выхода из системы.

## Конвенции именования

### Тестовые методы
Формат: `[Метод]_[Сценарий]_[ОжидаемыйРезультат]`

Примеры:
- `Handle_ValidCredentials_ReturnsSuccessResult`
- `Handle_InvalidCredentials_ThrowsAppInvalidCredentialsException`
- `CanAccess_OrganizerRole_ReturnsTrue`

### Тестовые данные
- Валидные данные: `test@example.com`, `password123`
- Невалидные данные: `invalid@example.com`, `wrongpassword`
- Существующие пользователи: `existinguser@example.com`
- Несуществующие пользователи: `nonexistent@example.com`

## Добавление новых тестов

### Шаблон для нового теста

```csharp
[TestSubject(typeof(YourCommandHandler))]
public class YourCommandHandlerTest
{
    private readonly IServiceScope _scope;

    public YourCommandHandlerTest()
    {
        var builder = WebApplication.CreateBuilder();
        builder.AddApplication();

        builder.Services.AddTransient<ISecurityService, SecurityServiceMock>();
        builder.Services.AddTransient<IUserManager<User>, UserManagerMock>();
        builder.Services.AddTransient<ISignInManager, SignInManagerMock>();

        var app = builder.Build();
        app.UseApplication();

        _scope = app.Services.CreateScope();
    }

    [Fact]
    public async Task Handle_ValidData_ReturnsSuccessResult()
    {
        // Arrange
        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = new YourCommand(validData);

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        // Дополнительные проверки
    }
}
```

### Рекомендации

1. **Тестируйте успешные сценарии** - убедитесь, что основная функциональность работает
2. **Тестируйте граничные случаи** - пустые данные, null значения
3. **Тестируйте исключения** - проверьте корректную обработку ошибок
4. **Тестируйте права доступа** - убедитесь, что авторизация работает правильно
5. **Используйте описательные имена** - тесты должны быть самодокументируемыми

## Поддержка и обновление

При изменении бизнес-логики команд и обработчиков:
1. Обновите соответствующие тесты
2. Добавьте новые тесты для новой функциональности
3. Убедитесь, что все тесты проходят
4. Обновите эту документацию при необходимости 
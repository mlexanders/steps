# LoginRequestCommandHandlerTest

Тесты для обработчика команды входа в систему (`LoginRequestCommandHandler`).

## Назначение

Проверяет корректность аутентификации пользователей, включая успешные и неуспешные сценарии входа.

## Тестируемые сценарии

### ✅ Успешные сценарии

#### `Handle_ValidCredentials_ReturnsSuccessResult`
**Цель:** Проверить успешный вход с корректными учетными данными.

**Входные данные:**
- Login: `test@example.com`
- Password: `password123`

**Ожидаемый результат:**
- `Result.IsSuccess = true`
- `Result.Value.Login = "test@example.com"`
- `Result.Message = "Вход выполнен успешно"`

**Логика теста:**
1. Создается модель входа с валидными данными
2. Отправляется команда через MediatR
3. Проверяется успешность результата и корректность данных пользователя

### ❌ Сценарии с ошибками

#### `Handle_InvalidCredentials_ThrowsAppInvalidCredentialsException`
**Цель:** Проверить обработку неверных учетных данных.

**Входные данные:**
- Login: `invalid@example.com`
- Password: `wrongpassword`

**Ожидаемый результат:**
- Выбрасывается исключение `AppInvalidCredentialsException`

**Логика теста:**
1. Создается модель с неверными данными
2. Ожидается исключение при попытке входа
3. Проверяется тип исключения

#### `Handle_UserNotFound_ThrowsAppUserNotFoundException`
**Цель:** Проверить обработку попытки входа несуществующего пользователя.

**Входные данные:**
- Login: `nonexistent@example.com`
- Password: `password123`

**Ожидаемый результат:**
- Выбрасывается исключение `AppUserNotFoundException`

**Логика теста:**
1. Создается модель с несуществующим email
2. Ожидается исключение при попытке входа
3. Проверяется тип исключения

#### `Handle_EmptyCredentials_ThrowsValidationException`
**Цель:** Проверить валидацию пустых данных.

**Входные данные:**
- Login: `""` (пустая строка)
- Password: `""` (пустая строка)

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

**Логика теста:**
1. Создается модель с пустыми данными
2. Ожидается исключение валидации
3. Проверяется тип исключения

## Используемые моки

### UserManagerMock
- Симулирует метод `Login(string email, string password)`
- Возвращает пользователя для валидных данных
- Выбрасывает исключения для специальных email-адресов:
  - `invalid@example.com` → `AppInvalidCredentialsException`
  - `nonexistent@example.com` → `AppUserNotFoundException`

### SecurityServiceMock
- Возвращает тестового пользователя с ролью `Organizer`
- Используется для проверки прав доступа

## Зависимости

- **MediatR** - для отправки команд
- **FluentValidation** - для валидации данных
- **Steps.Shared.Exceptions** - для пользовательских исключений

## Пример использования

```csharp
[Fact]
public async Task Handle_ValidCredentials_ReturnsSuccessResult()
{
    // Arrange
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
```

## Важные моменты

1. **Изоляция тестов** - каждый тест использует отдельный экземпляр сервисов
2. **Мокирование зависимостей** - внешние зависимости заменены на моки
3. **Проверка исключений** - тестируется корректная обработка ошибок
4. **Валидация данных** - проверяется работа FluentValidation

## Связанные тесты

- `LogoutUserCommandHandlerTest` - тесты выхода из системы
- `RegisterUserCommandHandlerTest` - тесты регистрации
- `CurrentUserQueryHandlerTest` - тесты получения текущего пользователя 
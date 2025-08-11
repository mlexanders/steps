# RegisterUserCommandHandlerTest

Тесты для обработчика команды регистрации пользователя (`RegisterUserCommandHandler`).

## Назначение

Проверяет корректность регистрации новых пользователей, включая валидацию данных, проверку паролей и обработку ошибок.

## Тестируемые сценарии

### ✅ Успешные сценарии

#### `Handle_ValidRegistrationData_ReturnsSuccessResult`
**Цель:** Проверить успешную регистрацию пользователя с корректными данными.

**Входные данные:**
- Name: `"Test User"`
- Login: `"newuser@example.com"`
- Password: `"password123"`
- PasswordConfirm: `"password123"`
- Role: `Role.User`

**Ожидаемый результат:**
- `Result.IsSuccess = true`
- `Result.Value != Guid.Empty`
- `Result.Message = "Вы успешно зарегистрированы"`

#### `Handle_RegistrationWithOrganizerRole_ReturnsSuccessResult`
**Цель:** Проверить регистрацию пользователя с ролью Organizer.

**Входные данные:**
- Name: `"Organizer User"`
- Login: `"organizer@example.com"`
- Password: `"password123"`
- PasswordConfirm: `"password123"`
- Role: `Role.Organizer`

**Ожидаемый результат:**
- `Result.IsSuccess = true`
- `Result.Value != Guid.Empty`

### ❌ Сценарии с ошибками

#### `Handle_UserAlreadyExists_ThrowsStepsBusinessException`
**Цель:** Проверить обработку попытки регистрации существующего пользователя.

**Входные данные:**
- Name: `"Existing User"`
- Login: `"existinguser@example.com"`
- Password: `"password123"`
- PasswordConfirm: `"password123"`
- Role: `Role.User`

**Ожидаемый результат:**
- Выбрасывается исключение `StepsBusinessException`

#### `Handle_PasswordsDoNotMatch_ThrowsValidationException`
**Цель:** Проверить валидацию несовпадения паролей.

**Входные данные:**
- Name: `"Test User"`
- Login: `"test@example.com"`
- Password: `"password123"`
- PasswordConfirm: `"differentpassword"`
- Role: `Role.User`

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

#### `Handle_EmptyName_ThrowsValidationException`
**Цель:** Проверить валидацию пустого имени.

**Входные данные:**
- Name: `""` (пустая строка)
- Login: `"test@example.com"`
- Password: `"password123"`
- PasswordConfirm: `"password123"`
- Role: `Role.User`

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

#### `Handle_EmptyLogin_ThrowsValidationException`
**Цель:** Проверить валидацию пустого логина.

**Входные данные:**
- Name: `"Test User"`
- Login: `""` (пустая строка)
- Password: `"password123"`
- PasswordConfirm: `"password123"`
- Role: `Role.User`

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

#### `Handle_WeakPassword_ThrowsValidationException`
**Цель:** Проверить валидацию слабого пароля.

**Входные данные:**
- Name: `"Test User"`
- Login: `"test@example.com"`
- Password: `"123"`
- PasswordConfirm: `"123"`
- Role: `Role.User`

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

#### `Handle_InvalidEmailFormat_ThrowsValidationException`
**Цель:** Проверить валидацию некорректного формата email.

**Входные данные:**
- Name: `"Test User"`
- Login: `"invalid-email"`
- Password: `"password123"`
- PasswordConfirm: `"password123"`
- Role: `Role.User`

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

## Используемые моки

### UserManagerMock
- Симулирует метод `CreateAsync(User user, string password)`
- Выбрасывает исключение для `existinguser@example.com`
- Возвращает созданного пользователя для валидных данных

### SecurityServiceMock
- Возвращает тестового пользователя с ролью `Organizer`
- Используется для проверки прав доступа

## Зависимости

- **MediatR** - для отправки команд
- **FluentValidation** - для валидации данных
- **Steps.Shared.Exceptions** - для пользовательских исключений
- **Steps.Domain.Definitions** - для ролей пользователей
- **System** - для работы с Guid

## Пример использования

```csharp
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
```

## Бизнес-правила

1. **Уникальность логина** - не может существовать два пользователя с одинаковым логином
2. **Совпадение паролей** - пароль и подтверждение пароля должны совпадать
3. **Валидация данных** - все поля должны быть заполнены корректно
4. **Формат email** - логин должен быть в формате email
5. **Сложность пароля** - пароль должен соответствовать требованиям безопасности
6. **Возврат ID** - команда возвращает ID созданного пользователя

## Правила валидации

### Имя пользователя
- Не может быть пустым
- Не может быть null

### Логин (Email)
- Не может быть пустым
- Должен быть в формате email
- Не может быть null

### Пароль
- Не может быть пустым
- Должен соответствовать требованиям сложности
- Не может быть null

### Подтверждение пароля
- Должно совпадать с паролем
- Не может быть пустым
- Не может быть null

## Важные моменты

1. **Валидация на уровне команды** - проверяется работа FluentValidation
2. **Проверка паролей** - тестируется совпадение пароля и подтверждения
3. **Бизнес-логика** - тестируется создание пользователя через `UserManager`
4. **Проверка ролей** - тестируется регистрация с разными ролями
5. **Изоляция тестов** - каждый тест независим от других

## Связанные тесты

- `LoginRequestCommandHandlerTest` - тесты аутентификации
- `CreateUserCommandHandlerTest` - тесты создания пользователей
- `UpdateUserCommandHandlerTest` - тесты обновления пользователей

## Специальные email-адреса в моках

- `existinguser@example.com` - вызывает `StepsBusinessException` при регистрации
- `newuser@example.com` - успешно регистрируется
- `organizer@example.com` - успешно регистрируется с ролью Organizer
- `invalid-email` - вызывает исключение валидации

## Особенности тестирования

1. **Проверка паролей** - тестируется валидация совпадения паролей
2. **Проверка формата email** - тестируется валидация формата логина
3. **Проверка сложности пароля** - тестируется валидация требований к паролю
4. **Проверка ролей** - тестируется регистрация с разными ролями
5. **Проверка результата** - убеждаемся, что возвращается правильный ID пользователя 
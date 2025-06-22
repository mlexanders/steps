# CreateUserCommandHandlerTest

Тесты для обработчика команды создания пользователя (`CreateUserCommandHandler`).

## Назначение

Проверяет корректность создания новых пользователей, включая проверку прав доступа, валидацию данных и обработку ошибок.

## Тестируемые сценарии

### ✅ Успешные сценарии

#### `Handle_ValidUserData_ReturnsSuccessResult`
**Цель:** Проверить успешное создание пользователя с корректными данными.

**Входные данные:**
- Login: `newuser@example.com`

**Ожидаемый результат:**
- `Result.IsSuccess = true`
- `Result.Value.Login = "newuser@example.com"`
- `Result.Message = "Пользователь создан"`

**Логика теста:**
1. Создается модель пользователя с валидными данными
2. Отправляется команда через MediatR
3. Проверяется успешность результата и корректность данных

### ❌ Сценарии с ошибками

#### `Handle_UserAlreadyExists_ThrowsStepsBusinessException`
**Цель:** Проверить обработку попытки создания существующего пользователя.

**Входные данные:**
- Login: `existinguser@example.com`

**Ожидаемый результат:**
- Выбрасывается исключение `StepsBusinessException`

**Логика теста:**
1. Создается модель с существующим email
2. Ожидается исключение при попытке создания
3. Проверяется тип исключения

#### `Handle_EmptyLogin_ThrowsValidationException`
**Цель:** Проверить валидацию пустого логина.

**Входные данные:**
- Login: `""` (пустая строка)

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

#### `Handle_NullModel_ThrowsValidationException`
**Цель:** Проверить валидацию null модели.

**Входные данные:**
- Model: `null`

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

### 🔒 Проверка прав доступа

#### `CanAccess_OrganizerRole_ReturnsTrue`
**Цель:** Проверить, что пользователь с ролью Organizer может создавать пользователей.

**Входные данные:**
- User Role: `Role.Organizer`

**Ожидаемый результат:**
- `CanAccess = true`

#### `CanAccess_NonOrganizerRole_ReturnsFalse`
**Цель:** Проверить, что пользователи с другими ролями не могут создавать пользователей.

**Входные данные:**
- User Role: `Role.User`

**Ожидаемый результат:**
- `CanAccess = false`

## Используемые моки

### UserManagerMock
- Симулирует метод `CreateAsync(User user, string password)`
- Возвращает созданного пользователя для валидных данных
- Выбрасывает исключение для `existinguser@example.com`

### SecurityServiceMock
- Возвращает тестового пользователя с ролью `Organizer`
- Используется для проверки прав доступа

## Зависимости

- **MediatR** - для отправки команд
- **FluentValidation** - для валидации данных
- **Steps.Shared.Exceptions** - для пользовательских исключений
- **Steps.Domain.Definitions** - для ролей пользователей

## Пример использования

```csharp
[Fact]
public async Task Handle_ValidUserData_ReturnsSuccessResult()
{
    // Arrange
    var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
    var createUserModel = new CreateUserViewModel 
    { 
        Login = "newuser@example.com"
    };
    var command = new CreateUserCommand(createUserModel);

    // Act
    var result = await mediator.Send(command);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal("newuser@example.com", result.Value.Login);
    Assert.Equal("Пользователь создан", result.Message);
}
```

## Бизнес-правила

1. **Права доступа** - только пользователи с ролью `Organizer` могут создавать новых пользователей
2. **Уникальность логина** - не может существовать два пользователя с одинаковым логином
3. **Валидация данных** - логин не может быть пустым или null
4. **Автоматическая генерация пароля** - пароль генерируется автоматически на сервере

## Важные моменты

1. **Проверка прав доступа** - тестируется метод `CanAccess()` команды
2. **Валидация на уровне команды** - проверяется работа FluentValidation
3. **Бизнес-логика** - тестируется создание пользователя через `UserManager`
4. **Изоляция тестов** - каждый тест независим от других

## Связанные тесты

- `UpdateUserCommandHandlerTest` - тесты обновления пользователей
- `LoginRequestCommandHandlerTest` - тесты аутентификации
- `RegisterUserCommandHandlerTest` - тесты регистрации

## Специальные email-адреса в моках

- `existinguser@example.com` - вызывает `StepsBusinessException` при создании
- `newuser@example.com` - успешно создается
- Любые другие адреса - успешно создаются 
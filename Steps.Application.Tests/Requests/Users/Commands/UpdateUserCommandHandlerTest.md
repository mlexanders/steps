# UpdateUserCommandHandlerTest

Тесты для обработчика команды обновления пользователя (`UpdateUserCommandHandler`).

## Назначение

Проверяет корректность обновления существующих пользователей, включая проверку прав доступа, валидацию данных и обработку ошибок.

## Тестируемые сценарии

### ✅ Успешные сценарии

#### `Handle_ValidUserData_ReturnsSuccessResult`
**Цель:** Проверить успешное обновление пользователя с корректными данными.

**Входные данные:**
- Id: `Guid.NewGuid()`
- Login: `updateduser@example.com`
- Role: `Role.Judge`

**Ожидаемый результат:**
- `Result.IsSuccess = true`
- `Result.Value = updateUserModel.Id`
- `Result.Message = "Данные пользователя обновлены"`

#### `Handle_UpdateUserRole_ReturnsSuccessResult`
**Цель:** Проверить успешное обновление роли пользователя.

**Входные данные:**
- Id: `Guid.NewGuid()`
- Login: `user@example.com`
- Role: `Role.Counter`

**Ожидаемый результат:**
- `Result.IsSuccess = true`
- `Result.Value = updateUserModel.Id`

### ❌ Сценарии с ошибками

#### `Handle_UserNotFound_ThrowsStepsBusinessException`
**Цель:** Проверить обработку попытки обновления несуществующего пользователя.

**Входные данные:**
- Id: `Guid.NewGuid()`
- Login: `nonexistent@example.com`
- Role: `Role.User`

**Ожидаемый результат:**
- Выбрасывается исключение `StepsBusinessException`

#### `Handle_EmptyLogin_ThrowsValidationException`
**Цель:** Проверить валидацию пустого логина.

**Входные данные:**
- Id: `Guid.NewGuid()`
- Login: `""` (пустая строка)
- Role: `Role.User`

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

#### `Handle_NullModel_ThrowsValidationException`
**Цель:** Проверить валидацию null модели.

**Входные данные:**
- Model: `null`

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

#### `Handle_EmptyId_ThrowsValidationException`
**Цель:** Проверить валидацию пустого ID.

**Входные данные:**
- Id: `Guid.Empty`
- Login: `test@example.com`
- Role: `Role.User`

**Ожидаемый результат:**
- Выбрасывается исключение `FluentValidation.ValidationException`

### 🔒 Проверка прав доступа

#### `CanAccess_OrganizerRole_ReturnsTrue`
**Цель:** Проверить, что пользователь с ролью Organizer может обновлять пользователей.

**Входные данные:**
- User Role: `Role.Organizer`

**Ожидаемый результат:**
- `CanAccess = true`

#### `CanAccess_NonOrganizerRole_ReturnsFalse`
**Цель:** Проверить, что пользователи с другими ролями не могут обновлять пользователей.

**Входные данные:**
- User Role: `Role.User`

**Ожидаемый результат:**
- `CanAccess = false`

## Используемые моки

### UserManagerMock
- Симулирует метод `UpdateAsync(User user)`
- Выбрасывает исключение для `nonexistent@example.com`
- Успешно обновляет для других пользователей

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
public async Task Handle_ValidUserData_ReturnsSuccessResult()
{
    // Arrange
    var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
    var updateUserModel = new UpdateUserViewModel 
    { 
        Id = Guid.NewGuid(),
        Login = "updateduser@example.com",
        Role = Role.Judge
    };
    var command = new UpdateUserCommand(updateUserModel);

    // Act
    var result = await mediator.Send(command);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal(updateUserModel.Id, result.Value);
    Assert.Equal("Данные пользователя обновлены", result.Message);
}
```

## Бизнес-правила

1. **Права доступа** - только пользователи с ролью `Organizer` могут обновлять пользователей
2. **Существование пользователя** - нельзя обновить несуществующего пользователя
3. **Валидация данных** - ID и логин не могут быть пустыми или null
4. **Обновление роли** - можно изменить роль пользователя
5. **Возврат ID** - команда возвращает ID обновленного пользователя

## Важные моменты

1. **Проверка прав доступа** - тестируется метод `CanAccess()` команды
2. **Валидация на уровне команды** - проверяется работа FluentValidation
3. **Бизнес-логика** - тестируется обновление пользователя через `UserManager`
4. **Проверка ID** - валидируется, что ID не пустой
5. **Изоляция тестов** - каждый тест независим от других

## Связанные тесты

- `CreateUserCommandHandlerTest` - тесты создания пользователей
- `LoginRequestCommandHandlerTest` - тесты аутентификации
- `RegisterUserCommandHandlerTest` - тесты регистрации

## Специальные email-адреса в моках

- `nonexistent@example.com` - вызывает `StepsBusinessException` при обновлении
- `user@example.com` - успешно обновляется
- Любые другие адреса - успешно обновляются

## Особенности тестирования

1. **Генерация GUID** - каждый тест использует новый GUID для ID
2. **Проверка ролей** - тестируется обновление на разные роли (User, Judge, Counter)
3. **Валидация модели** - проверяется валидация всех полей модели
4. **Проверка результата** - убеждаемся, что возвращается правильный ID пользователя
</rewritten_file> 
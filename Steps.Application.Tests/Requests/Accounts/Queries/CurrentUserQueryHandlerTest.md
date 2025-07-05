# CurrentUserQueryHandlerTest

Тесты для обработчика запроса получения текущего пользователя (`CurrentUserQueryHandler`).

## Назначение

Проверяет корректность получения данных текущего аутентифицированного пользователя, включая маппинг в ViewModel.

## Тестируемые сценарии

### ✅ Успешные сценарии

#### `Handle_AuthenticatedUser_ReturnsUserData`
**Цель:** Проверить получение данных аутентифицированного пользователя.

**Входные данные:**
- Запрос без параметров (использует текущего пользователя)

**Ожидаемый результат:**
- `Result.IsSuccess = true`
- `Result.Value.Login = "test@example.com"`
- `Result.Value.Role = Role.Organizer`
- `Result.Value.Id != Guid.Empty`

**Логика теста:**
1. Создается запрос получения текущего пользователя
2. Отправляется запрос через MediatR
3. Проверяется успешность результата и корректность данных пользователя

#### `Handle_UserDataMapping_ReturnsCorrectViewModel`
**Цель:** Проверить корректность маппинга данных пользователя в ViewModel.

**Входные данные:**
- Запрос без параметров

**Ожидаемый результат:**
- `Result.IsSuccess = true`
- `Result.Value` является экземпляром `UserViewModel`
- `Result.Value.Login = "test@example.com"`
- `Result.Value.Role = Role.Organizer`

**Логика теста:**
1. Создается запрос получения текущего пользователя
2. Отправляется запрос через MediatR
3. Проверяется тип возвращаемого объекта и корректность маппинга

## Используемые моки

### SecurityServiceMock
- Симулирует метод `GetCurrentUser()`
- Возвращает тестового пользователя с данными:
  - Id: `Guid.NewGuid()`
  - Login: `"test@example.com"`
  - Role: `Role.Organizer`
  - PasswordHash: `"hashed_password"`

## Зависимости

- **MediatR** - для отправки запросов
- **Steps.Application.Interfaces.Base** - для интерфейсов сервисов
- **Steps.Shared.Contracts.Accounts.ViewModels** - для ViewModel пользователя
- **Steps.Domain.Definitions** - для ролей пользователей
- **System** - для работы с Guid

## Пример использования

```csharp
[Fact]
public async Task Handle_AuthenticatedUser_ReturnsUserData()
{
    // Arrange
    var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
    var query = new CurrentUserQuery();

    // Act
    var result = await mediator.Send(query);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal("test@example.com", result.Value.Login);
    Assert.Equal(Role.Organizer, result.Value.Role);
    Assert.NotEqual(Guid.Empty, result.Value.Id);
}
```

## Бизнес-правила

1. **Аутентификация** - запрос должен возвращать данные только аутентифицированного пользователя
2. **Маппинг данных** - данные должны корректно маппиться в `UserViewModel`
3. **Безопасность** - не должны возвращаться чувствительные данные (пароль, хеш пароля)
4. **Полнота данных** - должны возвращаться все необходимые поля (Id, Login, Role)

## Структура UserViewModel

```csharp
public class UserViewModel
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public Role Role { get; set; }
}
```

## Важные моменты

1. **Безопасность данных** - не возвращается хеш пароля
2. **Маппинг** - проверяется корректность преобразования из `User` в `UserViewModel`
3. **Изоляция тестов** - каждый тест независим от других
4. **Мокирование зависимостей** - внешние зависимости заменены на моки

## Связанные тесты

- `LoginRequestCommandHandlerTest` - тесты входа в систему
- `LogoutUserCommandHandlerTest` - тесты выхода из системы
- `RegisterUserCommandHandlerTest` - тесты регистрации

## Особенности тестирования

1. **Проверка маппинга** - тестируется корректность преобразования данных
2. **Проверка типов** - убеждаемся, что возвращается правильный тип объекта
3. **Проверка данных** - проверяются все поля ViewModel
4. **Безопасность** - убеждаемся, что не возвращаются чувствительные данные

## Логика работы

### Получение текущего пользователя
1. Вызов `SecurityService.GetCurrentUser()`
2. Маппинг `User` в `UserViewModel`
3. Возврат результата с данными пользователя

### Маппинг данных
```csharp
// Пример маппинга в обработчике
var user = await _securityService.GetCurrentUser();
var viewModel = new UserViewModel
{
    Id = user.Id,
    Login = user.Login,
    Role = user.Role
    // PasswordHash не включается в ViewModel
};
```

## Возможные сценарии ошибок

Хотя в текущих тестах не покрыты сценарии с ошибками, возможны следующие ситуации:

1. **Пользователь не аутентифицирован** - `SecurityService.GetCurrentUser()` может вернуть null
2. **Ошибка сервиса** - `SecurityService` может выбросить исключение
3. **Проблемы с маппингом** - некорректное преобразование данных

## Рекомендации по расширению тестов

1. **Добавить тест для неаутентифицированного пользователя**
2. **Добавить тест для обработки исключений**
3. **Добавить тест для различных ролей пользователей**
4. **Добавить тест для проверки всех полей ViewModel**

## Использование в приложении

Этот запрос обычно используется для:
- Отображения информации о текущем пользователе в UI
- Проверки прав доступа
- Отображения имени пользователя в навигации
- Определения доступных функций в зависимости от роли 
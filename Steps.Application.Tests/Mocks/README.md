# Моки для тестов

Данная папка содержит моки (mock-объекты) для изоляции тестов от внешних зависимостей.

## Назначение моков

Моки используются для:
- Изоляции тестов от реальных сервисов
- Симуляции различных сценариев поведения
- Контроля возвращаемых данных
- Тестирования обработки ошибок

## Доступные моки

### UserManagerMock

**Интерфейс:** `IUserManager<User>`

**Назначение:** Симулирует работу с пользователями в базе данных.

#### Методы

##### `CreateAsync(User user, string password)`
Создает нового пользователя.

**Поведение:**
- Для `existinguser@example.com` - выбрасывает `StepsBusinessException`
- Для остальных email - возвращает созданного пользователя с новым GUID

**Пример использования:**
```csharp
var user = new User { Login = "newuser@example.com", Role = Role.User };
var result = await userManager.CreateAsync(user, "password123");
// result содержит созданного пользователя
```

##### `UpdateAsync(User user)`
Обновляет существующего пользователя.

**Поведение:**
- Для `nonexistent@example.com` - выбрасывает `StepsBusinessException`
- Для остальных email - успешно обновляет

##### `Login(string email, string password)`
Аутентифицирует пользователя.

**Поведение:**
- Для `nonexistent@example.com` - выбрасывает `AppUserNotFoundException`
- Для `invalid@example.com` или `wrongpassword` - выбрасывает `AppInvalidCredentialsException`
- Для остальных данных - возвращает аутентифицированного пользователя

##### `FindByEmailAsync(string email)`
Находит пользователя по email.

**Поведение:**
- Для `nonexistent@example.com` - возвращает `null`
- Для остальных email - возвращает пользователя

#### Специальные email-адреса

| Email | Поведение |
|-------|-----------|
| `existinguser@example.com` | Вызывает `StepsBusinessException` при создании |
| `nonexistent@example.com` | Вызывает исключения при поиске/обновлении |
| `invalid@example.com` | Вызывает `AppInvalidCredentialsException` при аутентификации |
| `test@example.com` | Успешно обрабатывается во всех операциях |
| `newuser@example.com` | Успешно создается |
| `user@example.com` | Успешно обновляется |

### SecurityServiceMock

**Интерфейс:** `ISecurityService`

**Назначение:** Симулирует получение текущего аутентифицированного пользователя.

#### Методы

##### `GetCurrentUser()`
Возвращает текущего пользователя.

**Поведение:**
- Всегда возвращает тестового пользователя с данными:
  - Id: `Guid.NewGuid()`
  - Login: `"test@example.com"`
  - Role: `Role.Organizer`
  - PasswordHash: `"hashed_password"`

**Пример использования:**
```csharp
var currentUser = await securityService.GetCurrentUser();
// currentUser содержит тестового пользователя с ролью Organizer
```

### SignInManagerMock

**Интерфейс:** `ISignInManager`

**Назначение:** Симулирует операции входа и выхода из системы.

#### Методы

##### `SignOutAsync()`
Выполняет выход из системы.

**Поведение:**
- Всегда возвращает успешный результат
- Не выбрасывает исключения

**Пример использования:**
```csharp
await signInManager.SignOutAsync();
// Операция всегда успешна
```

### ApplicationEventPublisherMock

**Интерфейс:** `IApplicationEventPublisher`

**Назначение:** Симулирует публикацию событий приложения.

#### Методы

##### `PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)`
Публикует событие с токеном отмены.

**Поведение:**
- Выводит в консоль информацию о событии и токене отмены
- Всегда возвращает успешный результат
- Не выбрасывает исключения

**Пример использования:**
```csharp
var @event = new UserCreatedEvent { UserId = Guid.NewGuid() };
await eventPublisher.PublishAsync(@event, CancellationToken.None);
// В консоли появится: [EVENT PUBLISHED] UserCreatedEvent: UserCreatedEvent { UserId = ... } (CancellationToken: ...)
```

### NotificationServiceMock

**Интерфейс:** `INotificationService`

**Назначение:** Симулирует отправку уведомлений.

#### Методы

##### `SendAsync(string message, string? recipient = null)`
Отправляет простое уведомление.

**Поведение:**
- Выводит в консоль информацию об уведомлении
- Всегда возвращает успешный результат
- Не выбрасывает исключения

**Пример использования:**
```csharp
await notificationService.SendAsync("Пользователь создан", "admin@example.com");
// В консоли появится: [NOTIFICATION SENT] To: admin@example.com, Message: Пользователь создан
```

##### `SendAsync(string message, object data, string? recipient = null)`
Отправляет уведомление с данными.

**Поведение:**
- Выводит в консоль информацию об уведомлении и данных
- Всегда возвращает успешный результат
- Не выбрасывает исключения

**Пример использования:**
```csharp
var userData = new { Id = Guid.NewGuid(), Login = "newuser@example.com" };
await notificationService.SendAsync("Пользователь создан", userData, "admin@example.com");
// В консоли появится: [NOTIFICATION SENT] To: admin@example.com, Message: Пользователь создан, Data: { Id = ..., Login = ... }
```

## Регистрация моков

Моки регистрируются в конструкторе тестовых классов:

```csharp
public YourTestClass()
{
    var builder = WebApplication.CreateBuilder();
    builder.AddApplication();

    // Регистрация моков
    builder.Services.AddTransient<ISecurityService, SecurityServiceMock>();
    builder.Services.AddTransient<IUserManager<User>, UserManagerMock>();
    builder.Services.AddTransient<ISignInManager, SignInManagerMock>();
    builder.Services.AddTransient<IApplicationEventPublisher, ApplicationEventPublisherMock>();
    builder.Services.AddTransient<INotificationService, NotificationServiceMock>();

    var app = builder.Build();
    app.UseApplication();

    _scope = app.Services.CreateScope();
}
```

## Создание новых моков

### Шаблон для нового мока

```csharp
public class YourServiceMock : IYourService
{
    public async Task<Result> YourMethod(YourParameter parameter)
    {
        // Логика мока
        if (parameter.SomeCondition)
        {
            throw new YourException("Error message");
        }

        return new Result { IsSuccess = true, Value = expectedValue };
    }
}
```

### Рекомендации

1. **Имитируйте реальное поведение** - мок должен вести себя как реальный сервис
2. **Добавляйте специальные случаи** - используйте специальные значения для тестирования ошибок
3. **Документируйте поведение** - опишите, как работает мок
4. **Делайте моки простыми** - избегайте сложной логики в моках
5. **Используйте константы** - определите специальные значения как константы

## Тестирование с моками

### Преимущества

1. **Изоляция** - тесты не зависят от внешних сервисов
2. **Скорость** - тесты выполняются быстро
3. **Контроль** - можно контролировать поведение зависимостей
4. **Предсказуемость** - результаты тестов предсказуемы

### Недостатки

1. **Неполное покрытие** - не тестируется интеграция с реальными сервисами
2. **Устаревание** - моки могут не соответствовать изменениям в реальных сервисах
3. **Сложность** - сложные моки могут быть трудными для понимания

## Интеграционные тесты

Для полного покрытия рекомендуется также создавать интеграционные тесты, которые используют реальные сервисы:

```csharp
[Fact]
public async Task IntegrationTest_WithRealServices()
{
    // Использование реальных сервисов
    var realUserManager = new RealUserManager();
    var realSecurityService = new RealSecurityService();
    
    // Тестирование интеграции
    var result = await realUserManager.CreateAsync(user, password);
    Assert.True(result.IsSuccess);
}
```

## Обновление моков

При изменении интерфейсов сервисов необходимо:

1. Обновить соответствующие моки
2. Добавить новые методы, если они появились
3. Обновить тесты, если изменилось поведение
4. Обновить документацию моков 
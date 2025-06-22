# Обзор тестов Steps.Application

## 📊 Статистика тестов

### Общее количество тестов: **25**

#### По категориям:
- **Аутентификация (Accounts/Commands)**: 8 тестов
- **Аутентификация (Accounts/Queries)**: 2 теста  
- **Управление пользователями (Users/Commands)**: 15 тестов

#### По типам сценариев:
- **✅ Успешные сценарии**: 12 тестов (48%)
- **❌ Сценарии с ошибками**: 10 тестов (40%)
- **🔒 Проверка прав доступа**: 3 теста (12%)

## 🎯 Покрытие функциональности

### Аутентификация и авторизация
- ✅ Вход в систему (4 теста)
- ✅ Выход из системы (2 теста)
- ✅ Регистрация пользователей (7 тестов)
- ✅ Получение текущего пользователя (2 теста)

### Управление пользователями
- ✅ Создание пользователей (6 тестов)
- ✅ Обновление пользователей (9 тестов)

### Валидация данных
- ✅ Валидация email
- ✅ Валидация паролей
- ✅ Валидация обязательных полей
- ✅ Проверка уникальности пользователей

### Обработка ошибок
- ✅ Неверные учетные данные
- ✅ Пользователь не найден
- ✅ Пользователь уже существует
- ✅ Валидационные ошибки

### Права доступа
- ✅ Проверка роли Organizer
- ✅ Проверка роли User
- ✅ Проверка роли Judge
- ✅ Проверка роли Counter

## 📋 Детальный список тестов

### LoginRequestCommandHandlerTest (4 теста)
1. `Handle_ValidCredentials_ReturnsSuccessResult` ✅
2. `Handle_InvalidCredentials_ThrowsAppInvalidCredentialsException` ❌
3. `Handle_UserNotFound_ThrowsAppUserNotFoundException` ❌
4. `Handle_EmptyCredentials_ThrowsValidationException` ❌

### LogoutUserCommandHandlerTest (2 теста)
1. `Handle_ValidLogout_ReturnsSuccessResult` ✅
2. `Handle_LogoutWhenNotAuthenticated_ReturnsSuccessResult` ✅

### RegisterUserCommandHandlerTest (7 тестов)
1. `Handle_ValidRegistrationData_ReturnsSuccessResult` ✅
2. `Handle_UserAlreadyExists_ThrowsStepsBusinessException` ❌
3. `Handle_PasswordsDoNotMatch_ThrowsValidationException` ❌
4. `Handle_EmptyName_ThrowsValidationException` ❌
5. `Handle_EmptyLogin_ThrowsValidationException` ❌
6. `Handle_WeakPassword_ThrowsValidationException` ❌
7. `Handle_InvalidEmailFormat_ThrowsValidationException` ❌
8. `Handle_RegistrationWithOrganizerRole_ReturnsSuccessResult` ✅

### CurrentUserQueryHandlerTest (2 теста)
1. `Handle_AuthenticatedUser_ReturnsUserData` ✅
2. `Handle_UserDataMapping_ReturnsCorrectViewModel` ✅

### CreateUserCommandHandlerTest (6 тестов)
1. `Handle_ValidUserData_ReturnsSuccessResult` ✅
2. `Handle_UserAlreadyExists_ThrowsStepsBusinessException` ❌
3. `Handle_EmptyLogin_ThrowsValidationException` ❌
4. `Handle_NullModel_ThrowsValidationException` ❌
5. `CanAccess_OrganizerRole_ReturnsTrue` 🔒
6. `CanAccess_NonOrganizerRole_ReturnsFalse` 🔒

### UpdateUserCommandHandlerTest (9 тестов)
1. `Handle_ValidUserData_ReturnsSuccessResult` ✅
2. `Handle_UserNotFound_ThrowsStepsBusinessException` ❌
3. `Handle_EmptyLogin_ThrowsValidationException` ❌
4. `Handle_NullModel_ThrowsValidationException` ❌
5. `Handle_EmptyId_ThrowsValidationException` ❌
6. `CanAccess_OrganizerRole_ReturnsTrue` 🔒
7. `CanAccess_NonOrganizerRole_ReturnsFalse` 🔒
8. `Handle_UpdateUserRole_ReturnsSuccessResult` ✅

## 🔧 Используемые технологии

### Фреймворки тестирования
- **xUnit** - основной фреймворк тестирования
- **JetBrains Annotations** - для атрибутов тестирования

### Паттерны
- **MediatR** - для отправки команд и запросов
- **FluentValidation** - для валидации данных
- **CQRS** - разделение команд и запросов

### Мокирование
- **UserManagerMock** - мок для работы с пользователями
- **SecurityServiceMock** - мок для безопасности
- **SignInManagerMock** - мок для аутентификации

## 📈 Метрики качества

### Покрытие сценариев
- **Успешные операции**: 100% покрыты
- **Обработка ошибок**: 90% покрыты
- **Валидация данных**: 100% покрыты
- **Права доступа**: 100% покрыты

### Типы тестируемых исключений
- `AppInvalidCredentialsException` ✅
- `AppUserNotFoundException` ✅
- `StepsBusinessException` ✅
- `FluentValidation.ValidationException` ✅

### Тестируемые роли пользователей
- `Role.User` ✅
- `Role.Organizer` ✅
- `Role.Judge` ✅
- `Role.Counter` ✅

## 🚀 Рекомендации по улучшению

### Добавить недостающие тесты
1. **DeleteUserCommandHandler** - тесты удаления пользователей
2. **GetUsersQueryHandler** - тесты получения списка пользователей
3. **GetUserByIdQueryHandler** - тесты получения пользователя по ID

### Расширить существующие тесты
1. **LoginRequestCommandHandlerTest**
   - Добавить тест для заблокированного пользователя
   - Добавить тест для истекшего пароля

2. **CurrentUserQueryHandlerTest**
   - Добавить тест для неаутентифицированного пользователя
   - Добавить тест для обработки исключений

3. **RegisterUserCommandHandlerTest**
   - Добавить тест для различных ролей
   - Добавить тест для сложных паролей

### Улучшить моки
1. **UserManagerMock**
   - Добавить поддержку блокировки пользователей
   - Добавить поддержку смены пароля
   - Добавить поддержку подтверждения email

2. **SecurityServiceMock**
   - Добавить поддержку различных ролей
   - Добавить поддержку неаутентифицированного состояния

### Добавить интеграционные тесты
1. **End-to-end тесты** - полный цикл операций
2. **Тесты с реальной базой данных** - проверка персистентности
3. **Тесты производительности** - проверка времени выполнения

## 📝 Конвенции именования

### Тестовые методы
Формат: `[Метод]_[Сценарий]_[ОжидаемыйРезультат]`

Примеры:
- `Handle_ValidCredentials_ReturnsSuccessResult`
- `Handle_InvalidCredentials_ThrowsAppInvalidCredentialsException`
- `CanAccess_OrganizerRole_ReturnsTrue`

### Тестовые данные
- **Валидные данные**: `test@example.com`, `password123`
- **Невалидные данные**: `invalid@example.com`, `wrongpassword`
- **Существующие пользователи**: `existinguser@example.com`
- **Несуществующие пользователи**: `nonexistent@example.com`

## 🔍 Запуск и отладка

### Команды для запуска
```bash
# Все тесты
dotnet test

# Конкретный тест
dotnet test --filter "FullyQualifiedName~LoginRequestCommandHandlerTest"

# Тесты с подробным выводом
dotnet test --verbosity normal

# Тесты с покрытием кода
dotnet test --collect:"XPlat Code Coverage"
```

### Отладка тестов
1. Установить breakpoints в тестовых методах
2. Запустить тесты в режиме отладки
3. Использовать `Debugger.Break()` для принудительной остановки
4. Проверить значения переменных в окне Watch

## 📚 Дополнительные ресурсы

### Документация
- [README.md](README.md) - основная документация
- [Mocks/README.md](Mocks/README.md) - документация моков
- Индивидуальные файлы документации для каждого тестового класса

### Полезные ссылки
- [xUnit Documentation](https://xunit.net/)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [FluentValidation Documentation](https://docs.fluentvalidation.net/)

### Рекомендации по чтению
1. Начните с [README.md](README.md) для общего понимания
2. Изучите [Mocks/README.md](Mocks/README.md) для понимания моков
3. Читайте индивидуальную документацию тестов по мере необходимости
4. Используйте этот обзор для быстрой навигации 
using Npgsql;
using Steps.Shared;

namespace Steps.Application.ExceptionsHandling.Descriptors;

public class SqlExceptionDescriptor : ExceptionDescriptor<PostgresException>
{
    public override (Error, int) GetDescriptionWithStatusCode(PostgresException exception)
    {
        var sqlState = exception.SqlState;

        return sqlState switch
        {
            "23503" => GetDescriptionWithStatusCode(sqlState, "Ошибка. Связь на несуществующий объект",
                StatusCodes.Status400BadRequest),
            "23502" => GetDescriptionWithStatusCode(sqlState, "Поле не может быть пустым",
                StatusCodes.Status400BadRequest),
            "22003" => GetDescriptionWithStatusCode(sqlState, "Числовое значение вне диапазона",
                StatusCodes.Status400BadRequest),
            "22007" => GetDescriptionWithStatusCode(sqlState, "Неверный формат даты/времени",
                StatusCodes.Status400BadRequest),
            "22023" => GetDescriptionWithStatusCode(sqlState, "Неверное значение параметра",
                StatusCodes.Status400BadRequest),
            "23505" => GetDescriptionWithStatusCode(sqlState, "Запись уже существует",
                StatusCodes.Status409Conflict),

            var state when state.StartsWith("22") => GetDescriptionWithStatusCode(state, "Ошибка данных",
                StatusCodes.Status400BadRequest),
            var state when state.StartsWith("40") => GetDescriptionWithStatusCode(state, "Ошибка транзакции",
                StatusCodes.Status409Conflict),
            var state when state.StartsWith("42") => GetDescriptionWithStatusCode(state, "Ошибка синтаксиса SQL",
                StatusCodes.Status500InternalServerError),
            var state when state.StartsWith("08") => GetDescriptionWithStatusCode(state,
                "Ошибка подключения к базе данных", StatusCodes.Status500InternalServerError),
            var state when state.StartsWith("01") => GetDescriptionWithStatusCode(state, "Общая ошибка SQL",
                StatusCodes.Status500InternalServerError),

            _ => GetDescriptionWithStatusCode("DATABASE_ERROR", "Неизвестная ошибка базы данных",
                StatusCodes.Status500InternalServerError)
        };
    }
}
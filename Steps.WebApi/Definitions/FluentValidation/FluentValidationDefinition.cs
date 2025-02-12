using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Steps.Services.WebApi.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.FluentValidation;

/// <summary>
///     FluentValidation registration as Application definition
/// </summary>
public class FluentValidationDefinition : AppDefinition
{
    /// <summary>
    ///     Configure services for current application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        //По умолчанию ASP.NET Core автоматически проверяет состояние модели (ModelState)
        // перед выполнением действия контроллера. Если модель содержит ошибки валидации,
        // фреймворк возвращает HTTP-ответ с кодом 400 Bad Request, не вызывая сам метод контроллера.
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
}
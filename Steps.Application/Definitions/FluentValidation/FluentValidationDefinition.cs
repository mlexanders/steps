using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Steps.Utils.AppDefinition;

namespace Steps.Application.Definitions.FluentValidation;


public class FluentValidationDefinition : AppDefinition
{
 
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        //По умолчанию ASP.NET Core автоматически проверяет состояние модели (ModelState)
        // перед выполнением действия контроллера. Если модель содержит ошибки валидации,
        // фреймворк возвращает HTTP-ответ с кодом 400 Bad Request, не вызывая сам метод контроллера.
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddValidatorsFromAssembly(typeof(FluentValidationDefinition).Assembly);
    }
}
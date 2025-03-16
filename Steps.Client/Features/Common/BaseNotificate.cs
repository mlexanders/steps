using Microsoft.AspNetCore.Components;
using Radzen;
using Steps.Shared;

namespace Steps.Client.Features.Common;

public class BaseNotificate : ComponentBase
{
    [Inject] protected NotificationService NotificationService { get; set; } = null!;

    protected void ShowNotification(string message)
    {
        var msg = new NotificationMessage
        {
            Severity = NotificationSeverity.Info ,
            Summary = "Уведомление",
            Detail = message
        };
        NotificationService.Notify(msg);
    }

    protected void ShowResultMessage(Result? result)
    {
        if (result == null) return;

        var msg = new NotificationMessage
        {
            Severity = result.IsSuccess ? NotificationSeverity.Success : NotificationSeverity.Error,
            Summary = result.Message,
            Detail = result.Errors?.FirstOrDefault()?.Message,
        };
        NotificationService.Notify(msg);
    }
}
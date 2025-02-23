using Microsoft.AspNetCore.Components;
using Radzen;

namespace Steps.Client.Features;

public class BaseModal : BaseNotificate
{
    [Inject] protected DialogService DialogService { get; set; } = null!;

    protected void Close(bool isSuccess = false)
    {
        DialogService.Close(isSuccess);
    }
}
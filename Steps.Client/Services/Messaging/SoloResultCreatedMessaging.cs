using Microsoft.AspNetCore.SignalR.Client;
using Steps.Client.Services.Messaging.Base;
using Steps.Shared.Contracts.Messaging;

namespace Steps.Client.Services.Messaging;

public class SoloResultCreatedMessaging : BaseMessaging<SoloResultCreatedMessage>
{
    public SoloResultCreatedMessaging(string hubUrl) : base(hubUrl)
    {
    }
}

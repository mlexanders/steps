using Steps.Client.Services.Messaging.Base;
using Steps.Shared.Contracts.Messaging;

namespace Steps.Client.Services.Messaging;

public class TestResultCreatedMessaging : MessagingService<TestResultCreatedMessage>
{
    public TestResultCreatedMessaging(string url) : base(url)
    {
    }
}
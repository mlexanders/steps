using Steps.Shared.Contracts.Messaging.Notifications;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Shared.Contracts.Messaging;

public class TestResultCreatedMessage(TestResultViewModel model) : MessageContract<TestResultCreatedMessage>
{
    public TestResultViewModel Model { get; init; } = model;
}